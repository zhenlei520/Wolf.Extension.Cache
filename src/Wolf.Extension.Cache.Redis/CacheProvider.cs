// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Response.Base;
using Wolf.Extension.Cache.Redis.Common;
using Wolf.Extension.Cache.Abstractions.Common;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Redis.Configurations;
using Wolf.Extensions.Serialize.Json.Abstracts;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// 基本存储
    /// </summary>
    public partial class CacheProvider : BaseRedisCacheProvider, ICacheProvider
    {
        private readonly RedisOptions _redisOptions;
        private readonly QuickHelperBase _quickHelperBase;

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonProvider"></param>
        /// <param name="redisOptions"></param>
        /// <param name="serviceId"></param>
        public CacheProvider(IJsonProvider jsonProvider, RedisOptions redisOptions, string serviceId) : base(
            jsonProvider)
        {
            this._redisOptions = redisOptions;
            this._quickHelperBase = new QuickHelperBase(serviceId);
        }

        #region 设置缓存

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool Set(string key, string value, PersistentOps persistentOps = null)
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
        public bool Set<T>(string key, T obj, PersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            return base.Execute(persistentOps.Strategy,
                () => this._quickHelperBase.Set(key, obj, this._redisOptions.Pre, persistentOps.OverdueTimeSpan), () =>
                {
                    var res = this._quickHelperBase.Set(key, obj, this._redisOptions.Pre,
                        persistentOps.OverdueTimeSpan);
                    if (expiry != null)
                    {
                        //需要过期
                    }

                    return res;
                });
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
            return base.Execute(persistentOps.Strategy, () => QuickHelperBase.Set(
                list.Select(x => new KeyValuePair<string, T>(x.Key, x.Value)),
                expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1), () => false);
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
            var res = QuickHelperBase.Get(key);
            return base.ConvertObj<T>(res);
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
            var ret = QuickHelperBase.Get((keys ?? new List<string>()).ToArray());
            return ret.Select(x => new BaseResponse<T>(x.Key, base.ConvertObj<T>(x.Value))).ToList();
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
            return QuickHelperBase.Increment(key, val);
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
            return QuickHelperBase.Increment(key, -val);
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
            return QuickHelperBase.Exists(key);
        }

        #endregion

        #region 设置过期时间（需调整）

        /// <summary>
        /// 设置过期时间（需调整）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool SetExpire(string key, PersistentOps persistentOps = null)
        {
            return QuickHelperBase.Expire(key, persistentOps);
        }

        #endregion

        #region 删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        public bool Remove(string key)
        {
            QuickHelperBase.Remove(key);
            return true;
        }

        #endregion

        #region 删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        public bool RemoveRange(ICollection<string> keys)
        {
            QuickHelperBase.Remove((keys ?? new List<string>()).ToArray());
            return true;
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
            QuickHelperBase.Keys(this._redisOptions.Pre + pattern).ToList().ForEach(p =>
            {
                keys.Add(p.Substring(this._redisOptions.Pre.Length));
            });
            return keys;
        }

        #endregion
    }
}
