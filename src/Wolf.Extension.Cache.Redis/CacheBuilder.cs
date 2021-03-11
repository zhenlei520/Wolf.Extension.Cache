// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using CSRedis;
using Wolf.DependencyInjection.Service;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Redis.Internal;
using Wolf.Extensions.Serialize.Json.Abstracts;
using Wolf.Systems.Core;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    ///
    /// </summary>
    public class CacheBuilder : DefaultWeight, ICacheBuilder
    {
        private readonly IEnumerable<CacheOptions> _cacheOptions;
        private readonly IJsonFactory _jsonFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="cacheOptions"></param>
        /// <param name="jsonFactory"></param>
        public CacheBuilder(IEnumerable<CacheOptions> cacheOptions, IJsonFactory jsonFactory)
        {
            this._cacheOptions = cacheOptions;
            this._jsonFactory = jsonFactory;
        }

        /// <summary>
        ///
        /// </summary>
        public string Identify => "Redis";

        /// <summary>
        /// 创建多个缓存服务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICacheProvider> CreateProviders()
        {
            var configs = GetConfigs();
            return configs.Select(config => new CacheProvider(new CSRedisClient(this._jsonFactory,
                config.NodeRuleExternal,
                config.Sentinels, config.ReadOnly, null, config.ConnectionStrings))).ToList();
        }

        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        public ICacheProvider CreateProvider(string serviceId = "")
        {
            var config = GetConfigs(serviceId);
            CSRedisClient csRedisClient = new CSRedisClient(this._jsonFactory, config.NodeRuleExternal,
                config.Sentinels, config.ReadOnly, null, config.ConnectionStrings);
            return new CacheProvider(csRedisClient);
        }

        #region 得到配置信息列表

        /// <summary>
        /// 得到配置信息列表
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CacheOptions> GetOptions()
        {
            return this._cacheOptions.Where(x => x.ServiceName.Equals(Identify, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region 得到配置信息

        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <returns></returns>
        private List<RedisConfigs> GetConfigs()
        {
            return GetOptions().Select(x => (RedisConfigs) x.Configuration).ToList();
        }

        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        private RedisConfigs GetConfigs(string serviceId)
        {
            if (serviceId.IsNullOrWhiteSpace())
            {
                return GetOptions().Select(x => (RedisConfigs) x.Configuration).FirstOrDefault();
            }

            return GetOptions().Where(x => x.ServiecId.Equals(serviceId, StringComparison.OrdinalIgnoreCase))
                .Select(x => (RedisConfigs) x.Configuration).FirstOrDefault();
        }

        #endregion
    }
}
