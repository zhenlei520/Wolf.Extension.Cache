// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Response.Base;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// 基本存储 异步
    /// </summary>
    public partial class CacheProvider
    {

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
            return Task.FromResult(Set(list, expiry, persistentOps));
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

        #region 查找所有符合给定模式( pattern)的 key（异步）

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key（异步）
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public Task<List<string>> KeysAsync(string pattern="*")
        {
            return Task.FromResult(Keys(pattern));
        }

        #endregion
    }
}
