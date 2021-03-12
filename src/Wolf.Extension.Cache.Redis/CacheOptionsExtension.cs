// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Wolf.DependencyInjection;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Redis.Configurations;
using Wolf.Extension.Cache.Redis.Internal;
using Wolf.Systems.Core;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    public static class CacheOptionsExtension
    {
        #region 添加redis配置

        /// <summary>
        /// 添加redis配置
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="redisOptions"></param>
        /// <param name="serviceId"></param>
        public static IServiceCollection AddRedis(this IServiceCollection serviceCollection, RedisOptions redisOptions,
            string serviceId)
        {
            return serviceCollection.AddRedis(GetCacheOptions(serviceId, new RedisConfigs(redisOptions.ToString())));
        }

        /// <summary>
        /// 添加redis配置
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="nodeRule">按key分区规则，返回值格式：127.0.0.1:6379/13，默认方案(null)：取key哈希与节点数取模</param>
        /// <param name="redisOptions">redis配置</param>
        /// <param name="serviceId">服务id</param>
        public static IServiceCollection AddRedis(this IServiceCollection serviceCollection,
            Func<string, string> nodeRule, RedisOptions[] redisOptions,
            string serviceId)
        {
            var connectionStrings = redisOptions.Select(x => x.ToString()).ToArray();
            return serviceCollection.AddRedis(GetCacheOptions(serviceId,
                new RedisConfigs(nodeRule, connectionStrings)));
        }

        /// <summary>
        /// 添加redis配置
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="connectionString">mymaster,password=123456,poolsize=50,connectTimeout=200,ssl=false</param>
        /// <param name="sentinels">哨兵节点</param>
        /// <param name="readOnly">是否只读</param>
        /// <param name="serviceId">服务id</param>
        public static IServiceCollection AddRedis(this IServiceCollection serviceCollection, string connectionString,
            string[] sentinels, bool readOnly, string serviceId)
        {
            return serviceCollection.AddRedis(GetCacheOptions(serviceId,
                new RedisConfigs(connectionString, sentinels, readOnly)));
        }

        /// <summary>
        /// 添加redis配置
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="connectionString">mymaster,password=123456,poolsize=50,connectTimeout=200,ssl=false</param>
        /// <param name="serviceId">服务id</param>
        public static IServiceCollection AddRedis(this IServiceCollection serviceCollection, string connectionString,
            string serviceId)
        {
            return serviceCollection.AddRedis(GetCacheOptions(serviceId,
                new RedisConfigs(connectionString)));
        }

        /// <summary>
        /// 添加redis配置
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="connectionStrings">mymaster,password=123456,poolsize=50,connectTimeout=200,ssl=false</param>
        /// <param name="serviceId">服务id</param>
        public static IServiceCollection AddRedis(this IServiceCollection serviceCollection,
            Func<string, string> nodeRule, string[] connectionStrings,
            string serviceId)
        {
            return serviceCollection.AddRedis(GetCacheOptions(serviceId,
                new RedisConfigs(nodeRule, connectionStrings)));
        }

        #endregion

        #region private methods

        #region 得到缓存配置

        /// <summary>
        /// 得到缓存配置
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <param name="configuration">配置信息</param>
        /// <returns></returns>
        private static CacheOptions GetCacheOptions(string serviceId, dynamic configuration)
        {
            return new CacheOptions()
            {
                ServiceName = GlobalConfigurations.ServiceName,
                ServiecId = serviceId.IsNullOrWhiteSpace() ? GlobalConfigurations.ServiceName : serviceId,
                Configuration = configuration
            };
        }

        #endregion

        #region 添加Redis配置

        /// <summary>
        /// 添加Redis配置
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="cacheOptions"></param>
        /// <returns></returns>
        private static IServiceCollection AddRedis(this IServiceCollection serviceCollection,
            CacheOptions cacheOptions)
        {
            serviceCollection.TryAddEnumerable(typeof(ICacheBuilder), typeof(CacheBuilder), ServiceLifetime.Singleton);
            serviceCollection.AddSingleton(cacheOptions);
            return serviceCollection;
        }

        #endregion

        #endregion
    }
}
