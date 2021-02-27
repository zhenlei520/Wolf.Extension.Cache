// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Redis.Configurations;
using Wolf.Systems.Abstracts;
using Wolf.Systems.Core;
using Wolf.Systems.Core.Common.Unique;
using Wolf.Systems.Core.Provider.Random;
using Wolf.Systems.Enum;

namespace Wolf.Extension.Cache.Redis.Common
{
    /// <summary>
    ///
    /// </summary>
    internal partial class QuickHelperBase
    {
        private readonly ConnectionPool Instance;
        private readonly RedisOptions _redisOptions;

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="redisOptions"></param>
        public QuickHelperBase(string serviceId, RedisOptions redisOptions)
        {
            this.Instance =
                GlobalConfigurations.Instance.Where(x => x.ServiecId == serviceId).Select(x => x.Instance)
                    .FirstOrDefault() ?? throw new Exception("获取Redis连接异常");
            this._redisOptions = redisOptions;
        }

        private static IRandomNumberGeneratorProvider _randomNumberGenerator = new RandomNumberGeneratorProvider();

        private static DateTime dt1970 = new DateTime(1970, 1, 1);
        private static Random rnd = new Random();

        /// <summary>
        ///
        /// </summary>
        private static readonly int __staticMachine = ((0x00ffffff & Environment.MachineName.GetHashCode()) +
#if NETSTANDARD1_5 || NETSTANDARD1_6
			1
#else
                                                       AppDomain.CurrentDomain.Id
#endif
            ) & 0x00ffffff;

        /// <summary>
        ///
        /// </summary>
        private static readonly int __staticPid = Process.GetCurrentProcess().Id;

        private static int __staticIncrement = rnd.Next();

        #region 设置指定缓存键的值

        /// <summary>
        /// 设置指定缓存键的值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">字符串值</param>
        /// <param name="overdueStrategy">过期策略</param>
        /// <param name="timeSpan">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, OverdueStrategy overdueStrategy, TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
            {
                return false;
            }

            string cacheKey = GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                if (timeSpan == TimeSpan.Zero || overdueStrategy == OverdueStrategy.SlidingExpiration)
                {
                    var ret = conn.Client.Set(cacheKey, value) == "OK";
                    if (ret && overdueStrategy == OverdueStrategy.SlidingExpiration)
                    {
                        this.SetSlidingExpiration(cacheKey, timeSpan);
                    }

                    return ret;
                }
                else
                {
                    return conn.Client.Set(cacheKey, value, timeSpan) == "OK";
                }
            }
        }

        #endregion

        #region 设置多组缓存键值对的集合

        /// <summary>
        /// 设置多组缓存键值对的集合
        /// </summary>
        /// <param name="list"></param>
        /// <param name="overdueStrategy">过期策略</param>
        /// <param name="IsAtomic">是否确保原子性，一个失败就回退</param>
        /// <param name="timeSpan">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(IEnumerable<KeyValuePair<string, T>> list, OverdueStrategy overdueStrategy,
            bool IsAtomic,
            TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
            {
                return false;
            }

            bool isRollback = false; //是否回滚
            using (var conn = Instance.GetConnection())
            {
                List<string> successList = new List<string>();
                if (timeSpan == TimeSpan.Zero || overdueStrategy == OverdueStrategy.SlidingExpiration)
                {
                    foreach (var item in list)
                    {
                        var cacheKey = GetCacheKey(item.Key);
                        if (conn.Client.Set(cacheKey, item.Value) != "OK" && IsAtomic)
                        {
                            isRollback = true;
                            break;
                        }

                        if (overdueStrategy == OverdueStrategy.SlidingExpiration)
                        {
                            this.SetSlidingExpiration(cacheKey, timeSpan);
                        }

                        successList.Add(item.Key);
                    }
                }
                else
                {
                    foreach (var item in list)
                    {
                        var cacheKey = GetCacheKey(item.Key);
                        if (conn.Client.Set(cacheKey, item.Value, timeSpan) != "OK" && IsAtomic)
                        {
                            isRollback = true;
                            break;
                        }

                        successList.Add(cacheKey);
                    }
                }

                if (isRollback)
                {
                    successList.ForEach(key => { conn.Client.Del(key); });
                    return false;
                }

                return true;
            }
        }

        #endregion

        /// <summary>
        /// 设置指定 key 的值(字节流)
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">字节流</param>
        /// <param name="expireSeconds">过期(秒单位)</param>
        /// <returns></returns>
        public bool SetBytes(string key, byte[] value, int expireSeconds = -1)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                if (expireSeconds > 0)
                    return conn.Client.Set(key, value, TimeSpan.FromSeconds(expireSeconds)) == "OK";
                else
                    return conn.Client.Set(key, value) == "OK";
            }
        }

        #region 获取指定 key 的值

        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public string Get(string key)
        {
            var cacheKey = this.GetCacheKey(key);
            return this.GetResultAndRenewalTime(cacheKey, () =>
            {
                using (var conn = Instance.GetConnection())
                {
                    return conn.Client.Get(cacheKey);
                }
            });
        }

        #endregion

        #region 获取指定 keys集合的值

        /// <summary>
        /// 获取指定 keys集合的值
        /// </summary>
        /// <param name="keys">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> Get(params string[] keys)
        {
            using (var conn = Instance.GetConnection())
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                foreach (var key in keys)
                {
                    var cacheKey = this.GetCacheKey(key);
                    var cacheValue = this.GetResultAndRenewalTime(cacheKey, () => conn.Client.Get(cacheKey));
                    list.Add(new KeyValuePair<string, string>(cacheKey, cacheValue));
                }

                return list;
            }
        }

        #endregion

        #region 针对非延迟过期的缓存可以用（暂时注释）

        // /// <summary>
        // /// 获取多个指定 key 的值(数组)
        // /// </summary>
        // /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        // /// <returns></returns>
        // public string[] GetStrings(params string[] key)
        // {
        //     if (key == null || key.Length == 0) return new string[0];
        //     string[] rkeys = new string[key.Length];
        //     for (int a = 0; a < key.Length; a++) rkeys[a] = this.GetCacheKey(key[a]);
        //     using (var conn = Instance.GetConnection())
        //     {
        //         return conn.Client.MGet(rkeys);
        //     }
        // }

        #endregion

        /// <summary>
        /// 获取指定 key 的值(字节流)
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public byte[] GetBytes(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.GetBytes(key);
            }
        }

        /// <summary>
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long Remove(params string[] key)
        {
            if (key == null || key.Length == 0) return 0;
            string[] rkeys = new string[key.Length];
            for (int a = 0; a < key.Length; a++) rkeys[a] = this.GetCacheKey(key[a]);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.Del(rkeys);
            }
        }

        /// <summary>
        /// 检查给定 key 是否存在
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.Exists(key);
            }
        }

        /// <summary>
        /// 将 key 所储存的值加上给定的增量值（increment）
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">增量值(默认=1)</param>
        /// <returns></returns>
        public long Increment(string key, long value = 1)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.IncrBy(key, value);
            }
        }

        /// <summary>
        /// 为给定 key 设置过期时间
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expire)
        {
            if (expire <= TimeSpan.Zero) return false;
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.Expire(key, expire);
            }
        }

        /// <summary>
        /// 以秒为单位，返回给定 key 的剩余生存时间
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long Ttl(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.Ttl(key);
            }
        }

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key
        /// </summary>
        /// <param name="pattern">如：runoob*</param>
        /// <returns></returns>
        public string[] Keys(string pattern)
        {
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.Keys(pattern);
            }
        }

        /// <summary>
        /// Redis Publish 命令用于将信息发送到指定的频道
        /// </summary>
        /// <param name="channel">频道名</param>
        /// <param name="data">消息文本</param>
        /// <returns></returns>
        public long Publish(string channel, string data)
        {
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.Publish(channel, data);
            }
        }

        #region Hash 操作

        #region 同时将多个 field-value (域-值)对设置到哈希表 key 中

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中
        /// </summary>
        /// <param name="expire">过期时间</param>
        /// <param name="keyValues">key dataKey,value</param>
        /// <returns></returns>
        public string HashSetExpire(Dictionary<string, string[]> keyValues, TimeSpan expire)
        {
            using (var conn = Instance.GetConnection())
            {
                string result = "Empty Data";
                if (keyValues != null && keyValues.Count > 0)
                {
                    foreach (var item in keyValues)
                    {
                        string key = this.GetCacheKey(item.Key);
                        if (expire.LessThan(TimeSpan.Zero))
                        {
                            return "Empty Data";
                        }

                        var ret = conn.Client.HMSet(key, item.Value.Select(a => string.Concat(a)).ToArray());
                        if (expire > TimeSpan.Zero) conn.Client.Expire(key, expire);
                        if (ret == "OK")
                        {
                            result = "OK";
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中
        /// </summary>
        /// <param name="expire">过期时间</param>
        /// <param name="keyValues">key dataKey,value</param>
        /// <returns></returns>
        public string HashSetHashFileExpire(Dictionary<string, string[]> keyValues, TimeSpan expire)
        {
            if (expire.GreaterThan(TimeSpan.Zero))
            {
                Dictionary<string, List<(double, string)>> dics = new Dictionary<string, List<(double, string)>>();
                double expireTime = (DateTime.Now.AddSeconds(expire.TotalSeconds)
                    .ToUnixTimestamp(TimestampType.MilliSecond).ConvertToDouble(0));
                foreach (var item in keyValues)
                {
                    var cacheKey = GetCacheFileKey();
                    if (!dics.ContainsKey(cacheKey))
                    {
                        dics.Add(cacheKey, new List<(double, string)>());
                    }

                    var memberScores = dics[cacheKey];
                    for (int i = 0; i < item.Value.Length; i += 2)
                    {
                        memberScores.Add(new ValueTuple<double, string>(expireTime,
                            GetSlidingOverTimeExpireValue(item.Key, item.Value[i].ToString(), TODO, TODO)));
                    }

                    dics[cacheKey] = memberScores;
                }

                ZAdd(dics);
            }
            else if (expire.Equals(TimeSpan.Zero))
            {
                return HashSetExpire(keyValues, TimeSpan.Zero);
            }

            return "Empty Data";
        }

        #endregion

        #region 同时将多个 field-value (域-值)对设置到哈希表 key 中

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="expire">过期时间</param>
        /// <param name="hashkeyValues">field1 value1 [field2 value2]</param>
        /// <returns></returns>
        public string HashSet(string key,
            params string[] hashkeyValues)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HMSet(key, hashkeyValues.Select(a => string.Concat(a)).ToArray());
            }
        }

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="expire">过期时间</param>
        /// <param name="hashkeyValues">field1 value1 [field2 value2]</param>
        /// <returns></returns>
        public string HashSetExpire(string key, TimeSpan expire,
            params object[] hashkeyValues)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                var ret = conn.Client.HMSet(key, hashkeyValues.Select(a => string.Concat(a)).ToArray());
                if (expire > TimeSpan.Zero)
                    conn.Client.Expire(key, expire);
                return ret;
            }
        }

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中(设置hashkey的过期时间)
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="hashKey"></param>
        /// <param name="expire">过期时间</param>
        /// <param name="value">结果</param>
        /// <returns></returns>
        public string HashSetHashFileExpire<T>(string key, string hashKey, TimeSpan expire, T value)
        {
            if (expire.GreaterThan(TimeSpan.Zero))
            {
                ZAdd(GetCacheFileKey(),
                    (DateTime.Now.AddSeconds(expire.TotalSeconds).ToUnixTimestamp(TimestampType.MilliSecond).ConvertToDouble(0),
                        GetSlidingOverTimeExpireValue(key, hashKey, TODO, TODO)));
                return "OK";
            }

            if (expire.Equals(TimeSpan.Zero))
            {
                return HashSetExpire(key, TimeSpan.Zero, hashKey, value);
            }

            return "No Data";
        }


        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中(设置hashkey的过期时间)
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="expire">过期时间</param>
        /// <param name="kvalues">结果</param>
        /// <returns></returns>
        public string HashSetHashFileExpire(string key, TimeSpan expire, params object[] kvalues)
        {
            if (expire > TimeSpan.Zero)
            {
                List<ValueTuple<double, string>> memberScores = new List<ValueTuple<double, string>>();
                double expireTime = (DateTime.Now.AddSeconds(expire.TotalSeconds)
                    .ToUnixTimestamp(TimestampType.MilliSecond).ConvertToDouble(0));
                for (int i = 0; i < kvalues.Length; i += 2)
                {
                    if (kvalues[i] != null && kvalues[i + 1] != null)
                        memberScores.Add((expireTime,
                            GetSlidingOverTimeExpireValue(key, kvalues[i].ToString(), TODO, TODO)));
                }

                ZAdd(GetCacheFileKey(), memberScores.ToArray());
            }

            return HashSetExpire(key, TimeSpan.Zero, kvalues);
        }

        #endregion

        /// <summary>
        /// 获取存储在哈希表中指定字段的值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public string HashGet(string key, string field)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HGet(key, field);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public string[] HashGet(string key, params string[] fields)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HMGet(key, fields);
            }
        }

        /// <summary>
        /// 获取存储在哈希表中指定字段的值
        /// </summary>
        /// <param name="keyDic"></param>
        /// <returns></returns>
        public Dictionary<string, string[]> HashGet(Dictionary<string, string[]> keyDic)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();
            using (var conn = Instance.GetConnection())
            {
                foreach (var item in keyDic)
                {
                    string key = this.GetCacheKey(item.Key);
                    result.Add(key, conn.Client.HMGet(key, item.Value));
                }
            }

            return result;
        }

        /// <summary>
        /// 为哈希表 key 中的指定字段的整数值加上增量 increment
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="field">字段</param>
        /// <param name="value">增量值(默认=1)</param>
        /// <returns></returns>
        public long HashIncrement(string key, string field, long value = 1)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HIncrBy(key, field, value);
            }
        }

        /// <summary>
        /// 删除一个或多个哈希表字段
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public long HashDelete(string key, params string[] fields)
        {
            if (fields == null || fields.Length == 0) return 0;
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HDel(key, fields);
            }
        }

        /// <summary>
        /// 查看哈希表 key 中，指定的字段是否存在
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public bool HashExists(string key, string field)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HExists(key, field);
            }
        }

        /// <summary>
        /// 获取哈希表中字段的数量
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long HashLength(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HLen(key);
            }
        }

        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public Dictionary<string, string> HashGetAll(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HGetAll(key);
            }
        }

        /// <summary>
        /// 获取所有哈希表中的字段
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public string[] HashKeys(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HKeys(key);
            }
        }

        /// <summary>
        /// 获取哈希表中所有值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public string[] HashVals(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.HVals(key);
            }
        }

        #endregion

        #region List 操作

        /// <summary>
        /// 通过索引获取列表中的元素
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public string LIndex(string key, long index)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LIndex(key, index);
            }
        }

        /// <summary>
        /// 在列表的元素前面插入元素
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="pivot">列表的元素</param>
        /// <param name="value">新元素</param>
        /// <returns></returns>
        public long LInsertBefore(string key, string pivot, string value)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LInsert(key, RedisInsert.Before, pivot, value);
            }
        }

        /// <summary>
        /// 在列表的元素后面插入元素
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="pivot">列表的元素</param>
        /// <param name="value">新元素</param>
        /// <returns></returns>
        public long LInsertAfter(string key, string pivot, string value)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LInsert(key, RedisInsert.After, pivot, value);
            }
        }

        /// <summary>
        /// 获取列表长度
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long LLen(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LLen(key);
            }
        }

        /// <summary>
        /// 移出并获取列表的第一个元素
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public string LPop(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LPop(key);
            }
        }

        /// <summary>
        /// 移除并获取列表最后一个元素
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public string RPop(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.RPop(key);
            }
        }

        /// <summary>
        /// 将一个或多个值插入到列表头部
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">一个或多个值</param>
        /// <returns></returns>
        public long LPush(string key, string[] value)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LPush(key, value);
            }
        }

        /// <summary>
        /// 在列表中添加一个或多个值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">一个或多个值</param>
        /// <returns></returns>
        public long RPush(string key, string[] value)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.RPush(key, value);
            }
        }

        /// <summary>
        /// 获取列表指定范围内的元素
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public string[] LRang(string key, long start, long stop)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LRange(key, start, stop);
            }
        }

        /// <summary>
        /// 根据参数 count 的值，移除列表中与参数 value 相等的元素
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">元素</param>
        /// <param name="count">移除的数量，大于0时从表头删除数量count，小于0时从表尾删除数量-count，等于0移除所有</param>
        /// <returns></returns>
        public long LRem(string key, string value, long count)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LRem(key, count, value);
            }
        }

        /// <summary>
        /// 通过索引设置列表元素的值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool LSet(string key, long index, string value)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LSet(key, index, value) == "OK";
            }
        }

        /// <summary>
        /// 对一个列表进行修剪，让列表只保留指定区间内的元素，不在指定区间之内的元素都将被删除
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public bool LTrim(string key, long start, long stop)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.LTrim(key, start, stop) == "OK";
            }
        }

        #endregion

        #region Sorted Set 操作

        /// <summary>
        /// 向有序集合添加一个或多个成员，或者更新已存在成员的分数
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="memberScores">一个或多个成员分数</param>
        /// <returns></returns>
        public long ZAdd(string key, params (string, double)[] memberScores)
        {
            using (var conn = Instance.GetConnection())
            {
                string cacheKey = this.GetCacheKey(key);
                return conn.Client.ZAdd(cacheKey,
                    memberScores.Select(a => new Tuple<double, string>(a.Item2, a.Item1)).ToArray());
            }
        }

        /// <summary>
        /// 向有序集合添加一个或多个成员，或者更新已存在成员的分数
        /// </summary>
        /// <param name="memberScores">一个或多个成员分数</param>
        /// <returns></returns>
        public long ZAdd(Dictionary<string, List<(double, string)>> memberScores)
        {
            using (var conn = Instance.GetConnection())
            {
                long result = 0;
                foreach (var item in memberScores)
                {
                    string key = this.GetCacheKey(item.Key);
                    result += conn.Client.ZAdd<double, string>(key,
                        item.Value.Select(a => new Tuple<double, string>(a.Item1, a.Item2)).ToArray());
                }

                return result;
            }
        }

        /// <summary>
        /// 获取有序集合的成员数量
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long ZCard(string key)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZCard(key);
            }
        }

        /// <summary>
        /// 计算在有序集合中指定区间分数的成员数量
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="min">分数最小值</param>
        /// <param name="max">分数最大值</param>
        /// <returns></returns>
        public long ZCount(string key, double min, double max)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZCount(key, min, max);
            }
        }

        /// <summary>
        /// 有序集合中对指定成员的分数加上增量 increment
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="memeber">成员</param>
        /// <param name="increment">增量值(默认=1)</param>
        /// <returns></returns>
        public double ZIncrBy(string key, string memeber, double increment = 1)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZIncrBy(key, increment, memeber);
            }
        }

        #region 多个有序集合 交集

        /// <summary>
        /// 计算给定的一个或多个有序集的最大值交集，将结果集存储在新的有序集合 destinationKey 中
        /// </summary>
        /// <param name="destinationKey">新的有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long ZInterStoreMax(string destinationKey, params string[] keys)
        {
            return ZInterStore(destinationKey, RedisAggregate.Max, keys);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的最小值交集，将结果集存储在新的有序集合 destinationKey 中
        /// </summary>
        /// <param name="destinationKey">新的有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long ZInterStoreMin(string destinationKey, params string[] keys)
        {
            return ZInterStore(destinationKey, RedisAggregate.Min, keys);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的合值交集，将结果集存储在新的有序集合 destinationKey 中
        /// </summary>
        /// <param name="destinationKey">新的有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long ZInterStoreSum(string destinationKey, params string[] keys)
        {
            return ZInterStore(destinationKey, RedisAggregate.Sum, keys);
        }

        private long ZInterStore(string destinationKey, RedisAggregate aggregate, params string[] keys)
        {
            destinationKey = this.GetCacheKey(destinationKey);
            string[] rkeys = new string[keys.Length];
            for (int a = 0; a < keys.Length; a++) rkeys[a] = this.GetCacheKey(keys[a]);
            if (rkeys.Length == 0) return 0;
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZInterStore(destinationKey, null, aggregate, rkeys);
            }
        }

        #endregion

        #region 多个有序集合 并集

        /// <summary>
        /// 计算给定的一个或多个有序集的最大值并集，将该并集(结果集)储存到 destination
        /// </summary>
        /// <param name="destinationKey">新的有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long ZUnionStoreMax(string destinationKey, params string[] keys)
        {
            return ZUnionStore(destinationKey, RedisAggregate.Max, keys);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的最小值并集，将该并集(结果集)储存到 destination
        /// </summary>
        /// <param name="destinationKey">新的有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long ZUnionStoreMin(string destinationKey, params string[] keys)
        {
            return ZUnionStore(destinationKey, RedisAggregate.Min, keys);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的合值并集，将该并集(结果集)储存到 destination
        /// </summary>
        /// <param name="destinationKey">新的有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public long ZUnionStoreSum(string destinationKey, params string[] keys)
        {
            return ZUnionStore(destinationKey, RedisAggregate.Sum, keys);
        }

        private long ZUnionStore(string destinationKey, RedisAggregate aggregate, params string[] keys)
        {
            destinationKey = this.GetCacheKey(destinationKey);
            string[] rkeys = new string[keys.Length];
            for (int a = 0; a < keys.Length; a++) rkeys[a] = this.GetCacheKey(keys[a]);
            if (rkeys.Length == 0) return 0;
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZUnionStore(destinationKey, null, aggregate, rkeys);
            }
        }

        #endregion

        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public string[] ZRange(string key, long start, long stop)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRange(key, start, stop, false);
            }
        }

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="minScore">最小分数</param>
        /// <param name="maxScore">最大分数</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        public string[] ZRangeByScore(string key, double minScore, double maxScore, long? limit = null,
            long offset = 0)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRangeByScore(key, minScore, maxScore, false, false, false, offset, limit);
            }
        }

        /// <summary>
        /// 返回有序集合中指定成员的索引
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public long? ZRank(string key, string member)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRank(key, member);
            }
        }

        /// <summary>
        /// 移除有序集合中的一个或多个成员
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="member">一个或多个成员</param>
        /// <returns></returns>
        public long ZRem(string key, params string[] member)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRem(key, member);
            }
        }

        /// <summary>
        /// 移除有序集合中给定的排名区间的所有成员
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public long ZRemRangeByRank(string key, long start, long stop)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRemRangeByRank(key, start, stop);
            }
        }

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="minScore">最小分数</param>
        /// <param name="maxScore">最大分数</param>
        /// <returns></returns>
        public long ZRemRangeByScore(string key, double minScore, double maxScore)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRemRangeByScore(key, minScore, maxScore);
            }
        }

        /// <summary>
        /// 返回有序集中指定区间内的成员，通过索引，分数从高到底
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public string[] ZRevRange(string key, long start, long stop)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRevRange(key, start, stop, false);
            }
        }

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="minScore">最小分数</param>
        /// <param name="maxScore">最大分数</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        public string[] ZRevRangeByScore(string key, double maxScore, double minScore, long? limit = null,
            long? offset = null)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRevRangeByScore(key, maxScore, minScore, false, false, false, offset, limit);
            }
        }

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <param name="keys">不含prefix前辍RedisHelper.Name</param>
        /// <param name="minScore">最小分数</param>
        /// <param name="maxScore">最大分数</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        public List<ValueTuple<string, string[]>> ZRevRangeByScore(List<string> keys, double maxScore,
            double minScore, long? limit = null,
            long? offset = null)
        {
            List<ValueTuple<string, string[]>> list = new List<(string, string[])>();
            using (var conn = Instance.GetConnection())
            {
                keys.ForEach(key =>
                {
                    key = this.GetCacheKey(key);
                    list.Add((key,
                        conn.Client.ZRevRangeByScore(key, maxScore, minScore, false, false, false, offset, limit)));
                });
                return list;
            }
        }

        /// <summary>
        /// 返回有序集合中指定成员的排名，有序集成员按分数值递减(从大到小)排序
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public long? ZRevRank(string key, string member)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZRevRank(key, member);
            }
        }

        /// <summary>
        /// 返回有序集中，成员的分数值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public double? ZScore(string key, string member)
        {
            key = this.GetCacheKey(key);
            using (var conn = Instance.GetConnection())
            {
                return conn.Client.ZScore(key, member);
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// SortSet类型的缓存key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetExpireKeyBySortSet(string key)
        {
            return $"sortset_{key}";
        }

        /// <summary>
        /// HashSet类型的缓存key
        /// 存储滑动过期的时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetExpireKeyByHashSet(string key)
        {
            return $"hashset_{key}";
        }

        #region 设置滑动过期

        /// <summary>
        /// 设置滑动过期
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="timeSpan">时间戳</param>
        private void SetSlidingExpiration(string key, TimeSpan timeSpan)
        {
            var slidingInfo = this.GetSlidingOverTimeExpireValue(key);

            var sortSetCacheKey = GetExpireKeyBySortSet(slidingInfo.Key);
            this.LRem(sortSetCacheKey, slidingInfo.Value, 0);
            this.ZAdd(sortSetCacheKey, (slidingInfo.Value, GetExpireTime(timeSpan)));

            var hashSetCacheKey = GetExpireKeyByHashSet(slidingInfo.Key);
            this.HashDelete(hashSetCacheKey, slidingInfo.Value);
            this.HashSet(hashSetCacheKey, slidingInfo.Value, timeSpan.TotalSeconds + ""); //设置HashSet过期时间，存储过期时长
        }

        #endregion

        #region 判断是否滑动过期并延长过期时间

        /// <summary>
        /// 判断是否滑动过期并延长过期时间
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="func">回调函数</param>
        /// <returns></returns>
        private T GetResultAndRenewalTime<T>(string key, Func<T> func)
        {
            var slidingInfo = this.GetSlidingOverTimeExpireValue(key);
            var hashSetCacheKey = GetExpireKeyByHashSet(slidingInfo.Key);
            var hashValue = HashGet(hashSetCacheKey, slidingInfo.Value);
            if (!string.IsNullOrWhiteSpace(hashValue))
            {
                var totalSecond = hashValue.ConvertToInt(0); //得到滑动过期的时间
                this.SetSlidingExpiration(key, TimeSpan.FromSeconds(totalSecond)); //延长时间
            }

            return func.Invoke();
        }

        #endregion

        #region 得到缓存键

        /// <summary>
        /// 得到缓存键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        private string GetCacheKey(string key)
        {
            return string.Concat(this._redisOptions.Pre, key);
        }

        #endregion

        #region 得到滑动过期的缓存Key与值

        ///  <summary>
        /// 得到滑动过期的缓存Key与值
        ///  </summary>
        ///  <param name="key"></param>
        ///  <returns></returns>
        private KeyValuePair<string, string> GetSlidingOverTimeExpireValue(string key)
        {
            int hashCode = GuidGeneratorCommon.GetHashCode(key);
            string cacheKey = string.Format(this._redisOptions.SlidingOverTimeCacheKeyFormat[0],
                hashCode % this._redisOptions.SlidingOverTimeCacheCount);
            string cacheValue = string.Format(this._redisOptions.SlidingOverTimeCacheKeyFormat[1], key);
            return new KeyValuePair<string, string>(cacheKey, cacheValue);
        }

        #endregion

        #region 得到过期时间 13位时间戳

        /// <summary>
        /// 得到过期时间 13位时间戳
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private long GetExpireTime(TimeSpan timeSpan)
        {
            return (DateTime.Now + timeSpan).ToUnixTimestamp(TimestampType.MilliSecond);
        }

        #endregion

        #endregion
    }
}
