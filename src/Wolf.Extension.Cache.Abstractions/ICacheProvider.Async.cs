// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Response.Base;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// 基本存储 异步
    /// </summary>
    public partial interface ICacheProvider
    {
        /// <summary>
        /// 设置缓存（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        Task<bool> SetAsync(
            string key,
            string value,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 设置缓存键值对集合(异步)
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        Task<bool> SetAsync(List<BaseRequest<string>> list,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 保存一个对象(异步)
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(
            string key,
            T obj,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 保存多个对象集合(异步)
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(List<BaseRequest<T>> list,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 获取单个key的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        Task<List<BaseResponse<string>>> GetAsync(ICollection<string> keys);

        /// <summary>
        /// 获取指定缓存的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        Task<List<BaseResponse<T>>> GetAsync<T>(ICollection<string> keys);

        /// <summary>
        /// 为数字增长val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        Task<long> IncrementAsync(string key, long val = 1);

        /// <summary>
        /// 为数字减少val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">减少的值</param>
        /// <returns></returns>
        Task<long> DecrementAsync(string key, long val = 1);

        /// <summary>
        /// 检查指定的缓存key是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<bool> ExistAsync(string key);

        /// <summary>
        /// 设置过期时间（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="timeSpan">过期时间，永久保存：TimeSpan.Zero</param>
        /// <param name="strategy">过期策略,默认绝对过期</param>
        /// <returns></returns>
        Task<bool> SetExpireAsync(string key, TimeSpan timeSpan, OverdueStrategy strategy = OverdueStrategy.AbsoluteExpiration);

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        Task<long> RemoveAsync(string key);

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        Task<long> RemoveRangeAsync(List<string> keys);

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key（异步）
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        Task<List<string>> KeysAsync(string pattern = "*");
    }
}
