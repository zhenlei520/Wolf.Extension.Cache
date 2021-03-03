// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Redis.Configurations;

namespace Wolf.Extension.Cache.Redis.Common
{
    /// <summary>
    /// 基于CsRedisCore的实现
    /// </summary>
    internal partial class CsRedisHelper
    {
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="redisOptions">redis配置</param>
        /// <param name="serviceId">服务id</param>
        public static void InitializeConfiguration(RedisOptions redisOptions, string serviceId)
        {
            if (GlobalConfigurations.Instance == null)
            {
                GlobalConfigurations.Instance = new List<RedisConnectionPoolConfigs>();
            }

            if (GlobalConfigurations.Instance.All(x => x.ServiecId != serviceId))
            {
                var connectionPool = new RedisConnectionPoolConfigs()
                {
                    Instance = new ConnectionPool(redisOptions.Ip, redisOptions.Port, redisOptions.PoolSize),
                    ServiecId = serviceId
                };
                GlobalConfigurations.Instance.Add(connectionPool);
                connectionPool.Instance.Connected += (s, o) =>
                {
                    RedisClient rc = s as RedisClient;
                    if (!string.IsNullOrEmpty(redisOptions.Password)) rc.Auth(redisOptions.Password);
                    if (redisOptions.DataBase > 0) rc.Select(redisOptions.DataBase);
                };
            }
        }
    }
}
