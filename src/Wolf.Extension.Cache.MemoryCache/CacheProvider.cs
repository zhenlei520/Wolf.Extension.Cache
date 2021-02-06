// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Common;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Response.Base;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// 基本存储
    /// </summary>
    public partial class CacheProvider : ICacheProvider
    {
        /// <summary>
        ///
        /// </summary>
        private readonly object obj = new object();

        /// <summary>
        ///
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        ///
        /// </summary>
        /// <param name="memoryCache"></param>
        public CacheProvider(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        #region 基本

        #region 同步

        #region 设置缓存

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool Set(string key, string value, TimeSpan? expiry = null, PersistentOps persistentOps = null)
        {
            return this.Set<string>(key, value, expiry, persistentOps);
        }

        #endregion

        #region 设置缓存键值对集合

        /// <summary>
        /// 设置缓存键值对集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool StringSet(IEnumerable<BaseRequest<string>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null)
        {
            return this.Set<string>(list, expiry, persistentOps);
        }

        #endregion

        #region 保存一个对象

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(string key, T obj, TimeSpan? expiry = null, PersistentOps persistentOps = null)
        {
            CheckKey(key);
            if (expiry == null)
            {
                this._memoryCache.Set(key, obj);
            }
            else
            {
                persistentOps = persistentOps.Get();
                MemoryCacheEntryOptions memoryCacheEntryOptions;
                if (persistentOps.Strategy == OverdueStrategy.AbsoluteExpiration)
                {
                    memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiry.Value);
                }
                else if (persistentOps.Strategy == OverdueStrategy.SlidingExpiration)
                {
                    memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(expiry.Value);
                }
                else
                {
                    Console.WriteLine("不支持的过期策略");
                    return false;
                }

                this._memoryCache.Set(key, obj, memoryCacheEntryOptions);
            }

            return true;
        }

        #endregion

        #region 保存多个对象集合

        /// <summary>
        /// 保存多个对象集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(IEnumerable<BaseRequest<T>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null)
        {
            if (list == null || !list.Any())
            {
                return false;
            }

            list.Select(x => x.Key).ToList().ForEach(CheckKey);
            persistentOps = persistentOps.Get();
            List<string> roleBackList = new List<string>();
            foreach (var item in list)
            {
                if (!Set(item.Key, item.Value, expiry) && persistentOps.IsAtomic)
                {
                    roleBackList.Add(item.Key);
                    break;
                }
            }

            if (roleBackList.Count > 0)
            {
                RemoveRange(roleBackList);
                return false;
            }

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
        public List<BaseResponse<string>> Get(IEnumerable<string> keys)
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
            CheckKey(key);
            return _memoryCache.Get<T>(key);
        }

        #endregion

        #region 获取多组缓存键集合

        /// <summary>
        /// 获取多组缓存键集合
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        public List<BaseResponse<T>> Get<T>(IEnumerable<string> keys)
        {
            if (keys == null || !keys.Any())
            {
                return new List<BaseResponse<T>>();
            }

            keys.ToList().ForEach(CheckKey);
            return keys.Select(x => new BaseResponse<T>(x, Get<T>(x))).ToList();
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
            long value = val;
            if (this.Exist(key))
            {
                value = this.Get<long>(key) + val;
            }

            this.Set<long>(key, value);
            return value;
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
            long value = val * -1;
            if (this.Exist(key))
            {
                value = this.Get<long>(key) - val;
            }

            this.Set<long>(key, value);
            return value;
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
            CheckKey(key);
            return this._memoryCache.TryGetValue(key, out object value);
        }

        #endregion

        #region 设置过期时间

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool SetExpire(string key, TimeSpan expiry, PersistentOps persistentOps = null)
        {
            CheckKey(key);
            persistentOps = persistentOps.Get();
            this._memoryCache.GetOrCreate(key, cacheEntry =>
            {
                if (persistentOps.Strategy == OverdueStrategy.AbsoluteExpiration)
                {
                    cacheEntry.AbsoluteExpiration = DateTimeOffset.Now.Add(expiry);
                }
                else if (persistentOps.Strategy == OverdueStrategy.SlidingExpiration)
                {
                    cacheEntry.SetSlidingExpiration(expiry);
                }
                else
                {
                    Console.WriteLine("不支持的过期策略");
                }

                return DateTimeOffset.Now;
            });
            return true;
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
            CheckKey(key);
            this._memoryCache.Remove(key);
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
        public bool RemoveRange(IEnumerable<string> keys)
        {
            if (keys == null || !keys.Any())
            {
                return true;
            }

            keys.ToList().ForEach(CheckKey);
            foreach (var key in keys)
            {
                Remove(key);
            }

            return true;
        }

        #endregion

        #endregion

        #region 异步

        #region 设置缓存（异步）

        /// <summary>
        /// 设置缓存（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> SetAsync(
            string key,
            string value,
            TimeSpan? expiry = null,
            PersistentOps persistentOps = null)
        {
            return Task.FromResult(Set(key, value, expiry, persistentOps));
        }

        #endregion

        #region 设置缓存键值对集合(异步)

        /// <summary>
        /// 设置缓存键值对集合(异步)
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> SetAsync(List<BaseRequest<string>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null)
        {
            return Task.FromResult<bool>(StringSet(list, expiry, persistentOps));
        }

        #endregion

        #region 保存一个对象(异步)

        /// <summary>
        /// 保存一个对象(异步)
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SetAsync<T>(
            string key,
            T obj,
            TimeSpan? expiry = null,
            PersistentOps persistentOps = null)
        {
            return Task.FromResult(Set(key, obj, expiry, persistentOps));
        }

        #endregion

        #region 保存多个对象集合(异步)

        /// <summary>
        /// 保存多个对象集合(异步)
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SetAsync<T>(List<BaseRequest<T>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null)
        {
            return Task.FromResult(Set(list, expiry, persistentOps));
        }

        #endregion

        #region 获取单个key的值（异步）

        /// <summary>
        /// 获取单个key的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public Task<string> GetAsync(string key)
        {
            return Task.FromResult(Get(key));
        }

        #endregion

        #region 获取多组缓存键集合（异步）

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        public Task<List<BaseResponse<string>>> GetAsync(IEnumerable<string> keys)
        {
            return Task.FromResult(Get(keys));
        }

        #endregion

        #region 获取指定缓存的值（异步）

        /// <summary>
        /// 获取指定缓存的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key) where T : class, new()
        {
            return Task.FromResult(Get<T>(key));
        }

        #endregion

        #region 获取多组缓存键集合（异步）

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        public Task<List<BaseResponse<T>>> GetAsync<T>(IEnumerable<string> keys) where T : class, new()
        {
            return Task.FromResult(Get<T>(keys));
        }

        #endregion

        #region 为数字增长val（异步）

        /// <summary>
        /// 为数字增长val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        public Task<long> IncrementAsync(string key, long val = 1)
        {
            return Task.FromResult(Increment(key, val));
        }

        #endregion

        #region 为数字减少val（异步）

        /// <summary>
        /// 为数字减少val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">减少的值</param>
        /// <returns></returns>
        public Task<long> DecrementAsync(string key, long val = 1)
        {
            return Task.FromResult(Decrement(key, val));
        }

        #endregion

        #region 检查指定的缓存key是否存在（异步）

        /// <summary>
        /// 检查指定的缓存key是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<bool> ExistAsync(string key)
        {
            return Task.FromResult(Exist(key));
        }

        #endregion

        #region 设置过期时间（异步）

        /// <summary>
        /// 设置过期时间（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> SetExpireAsync(string key, TimeSpan expiry, PersistentOps persistentOps = null)
        {
            return Task.FromResult(SetExpire(key, expiry, persistentOps));
        }

        #endregion

        #region 删除指定Key的缓存（异步）

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        public Task<bool> RemoveAsync(string key)
        {
            return Task.FromResult(Remove(key));
        }

        #endregion

        #region 删除指定Key的缓存（异步）

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        public Task<bool> RemoveRangeAsync(List<string> keys)
        {
            return Task.FromResult(RemoveRange(keys));
        }

        #endregion

        #endregion

        #endregion 基本

        #region private methods

        #region 检查缓存key

        /// <summary>
        /// 检查缓存key
        /// </summary>
        /// <param name="key">缓存key</param>
        private void CheckKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
        }

        #endregion

        #endregion
    }
}
