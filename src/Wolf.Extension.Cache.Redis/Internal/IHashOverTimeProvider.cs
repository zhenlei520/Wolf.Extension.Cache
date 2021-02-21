// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Hash;

namespace Wolf.Extension.Cache.Redis.Internal
{
    /// <summary>
    /// Hash过期策略
    /// </summary>
    public interface IHashOverTimeProvider
    {
        /// <summary>
        /// 设置指定的hash过期
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <param name="value">结果</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSetHashFileExpire<T>(string key, string hashKey, T value, HashPersistentOps persistentOps);

        /// <summary>
        /// 设置多组hash集合过期
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashValue">hash键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSetHashFileExpire<T>(string key, List<HashRequest<T>> hashValue, HashPersistentOps persistentOps);

        /// <summary>
        /// 设置多组缓存键下的hash集合过期
        /// </summary>
        /// <param name="request"></param>
        /// <param name="persistentOps"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSetHashFileExpire<T>(List<MultHashRequest<HashRequest<T>>> request,
            HashPersistentOps persistentOps);
    }
}
