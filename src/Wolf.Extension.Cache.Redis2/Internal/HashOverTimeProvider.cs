// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Redis.Common;
using Wolf.Extension.Cache.Redis.Configurations;
using Wolf.Systems.Core.Common.Unique;

namespace Wolf.Extension.Cache.Redis.Internal
{
    /// <summary>
    /// hash键过期策略
    /// </summary>
    public partial class HashOverTimeProvider : IHashOverTimeProvider
    {
        private readonly RedisOptions _redisOptions;

        /// <summary>
        ///
        /// </summary>
        /// <param name="redisOptions"></param>
        public HashOverTimeProvider(RedisOptions redisOptions)
        {
            this._redisOptions = redisOptions;
        }

        #region 设置指定的hash过期（只考虑设置过期时间的情况）

        /// <summary>
        /// 设置指定的hash过期（只考虑设置过期时间的情况）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <param name="value">结果</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSetHashFileExpire<T>(string key, string hashKey, T value, HashPersistentOps persistentOps)
        {
            var cacheInfo = this.GetOverTimeExpireValue(key, hashKey,persistentOps.Strategy,this._redisOptions);
            QuickHelperBase.ZAdd(cacheInfo.Key,persistentOps.)
        }

        #endregion

        #region 设置多组hash集合过期（只考虑设置过期时间的情况）

        /// <summary>
        /// 设置多组hash集合过期（只考虑设置过期时间的情况）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashValue">hash键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSetHashFileExpire<T>(string key, List<HashRequest<T>> hashValue, HashPersistentOps persistentOps);

        #endregion

        #region 设置多组缓存键下的hash集合过期（只考虑设置过期时间的情况）

        /// <summary>
        /// 设置多组缓存键下的hash集合过期（只考虑设置过期时间的情况）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="persistentOps"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSetHashFileExpire<T>(List<MultHashRequest<HashRequest<T>>> request,
            HashPersistentOps persistentOps);

        #endregion
    }

    /// <summary>
    /// hash键过期策略
    /// </summary>
    public partial class HashOverTimeProvider
    {
        #region 得到自定义HashKey过期的缓存Key

        ///  <summary>
        /// 得到自定义HashKey过期的缓存Key（单键过期）
        ///  </summary>
        ///  <param name="key"></param>
        ///  <param name="hashKey"></param>
        ///  <param name="overdueStrategy">过期策略</param>
        ///  <param name="options">redis配置</param>
        ///  <returns></returns>
        internal KeyValuePair<string, string> GetOverTimeExpireValueBy(string key, string hashKey,
            OverdueStrategy overdueStrategy,
            RedisOptions options)
        {
            int hashCode = GuidGeneratorCommon.GetHashCode(key + hashKey);
            string cacheKey = overdueStrategy == OverdueStrategy.AbsoluteExpiration
                ? string.Format(options.HashOverTimeCacheKeyFormat[0], hashCode)
                : string.Format(options.HashOverTimeCacheKeyFormat[2], hashCode);
            string cacheValue = overdueStrategy == OverdueStrategy.AbsoluteExpiration
                ? string.Format(options.HashOverTimeCacheKeyFormat[1], key, hashKey)
                : string.Format(options.HashOverTimeCacheKeyFormat[3], key, hashKey);
            return new KeyValuePair<string, string>(cacheKey, cacheValue);
        }



        #endregion
    }
}
