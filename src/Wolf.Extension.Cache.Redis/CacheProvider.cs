// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using CSRedis;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Common;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Response.Base;
using Wolf.Extension.Cache.Redis.Internal;
using Wolf.Extensions.Serialize.Json.Abstracts;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public partial class CacheProvider : ICacheProvider
    {
        private readonly CSRedisClient _client;
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="redisClient"></param>
        public CacheProvider(CSRedisClient redisClient)
        {
            this._client = redisClient;
        }

        #region 设置缓存

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool Set(string key, string value, BasePersistentOps persistentOps = null)
        {
            return this.Set<string>(key, value, persistentOps);
        }

        #endregion

        #region 设置缓存键值对集合

        /// <summary>
        /// 设置缓存键值对集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool Set(IEnumerable<BaseRequest<string>> list,
            PersistentOps persistentOps = null)
        {
            return this.Set<string>(list, persistentOps);
        }

        #endregion

        #region 保存一个对象

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(string key, T obj, BasePersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            return this._client.Set(key, obj, persistentOps.OverdueTimeSpan,
                persistentOps.SetStrategy.GetRedisExistence());
        }

        #endregion

        #region 保存多个对象集合

        /// <summary>
        /// 保存多个对象集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(IEnumerable<BaseRequest<T>> list,
            PersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            RedisExistence? exists = persistentOps.SetStrategy.GetRedisExistence();
            var ret = this._client.StartPipe(client =>
            {
                CSRedisClientPipe<bool> clientPipe = null;
                foreach (var item in list)
                {
                    if (clientPipe == null)
                    {
                        clientPipe = client.Set(item.Key, item.Value, persistentOps.OverdueTimeSpan, exists);
                    }
                    else
                    {
                        clientPipe.Set(item.Key, item.Value, persistentOps.OverdueTimeSpan, exists);
                    }
                }
            });
            return true;
        }

        #endregion

        #region 获取单个key的值

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public string Get(string key)
        {
            return this.Get<string>(key);
        }

        #endregion

        #region 获取多组缓存键集合

        /// <summary>
        /// 获取多组缓存键集合
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        public List<BaseResponse<string>> Get(ICollection<string> keys)
        {
            return this.Get<string>(keys);
        }

        #endregion

        #region 获取指定缓存的值

        /// <summary>
        /// 获取指定缓存的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return this._client.Get<T>(key);
        }

        #endregion

        #region 获取多组缓存键集合

        /// <summary>
        /// 获取多组缓存键集合
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        public List<BaseResponse<T>> Get<T>(ICollection<string> keys)
        {
            return null;
        }

        #endregion

        #region 为数字增长val

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">待增加的值</param>
        /// <returns>原value+val</returns>
        public long Increment(string key, long val = 1)
        {
            return this._client.IncrBy(key, val);
        }

        #endregion

        #region 为数字减少val

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">待减的值</param>
        /// <returns>原value-val</returns>
        public long Decrement(string key, long val = 1)
        {
            return this._client.IncrBy(key, -val);
        }

        #endregion

        #region 检查指定的缓存key是否存在

        /// <summary>
        /// 检查指定的缓存key是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            return this._client.Exists(key);
        }

        #endregion

        #region 设置过期时间（需调整）

        /// <summary>
        /// 设置过期时间（需调整）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="timeSpan">过期时间，永久保存：TimeSpan.Zero</param>
        /// <param name="strategy">过期策略,默认绝对过期</param>
        /// <returns></returns>
        public bool SetExpire(string key, TimeSpan timeSpan,
            OverdueStrategy strategy = OverdueStrategy.AbsoluteExpiration)
        {
            if (timeSpan < TimeSpan.Zero)
            {
                throw new NotSupportedException(nameof(timeSpan));
            }

            return this._client.Expire(key, timeSpan);
        }

        #endregion

        #region 删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        public long Remove(string key)
        {
            return this._client.Del(key);
        }

        #endregion

        #region 删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        public long RemoveRange(ICollection<string> keys)
        {
            if (keys == null || !keys.Any())
            {
                return 0;
            }

            return this._client.Del(keys.ToArray());
        }

        #endregion

        #region 查找所有符合给定模式( pattern)的 key

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key
        /// 默认得到全部的键
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public List<string> Keys(string pattern = "*")
        {
            var keys = new List<string>();
            return this._client.Keys(pattern).ToList();
        }

        #endregion
    }
}
