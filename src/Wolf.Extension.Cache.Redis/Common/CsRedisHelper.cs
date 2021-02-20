﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.Extension.Cache.Redis.Config;

namespace Wolf.Extension.Cache.Redis.Common
{
    /// <summary>
    /// 基于CsRedisCore的实现
    /// </summary>
    internal partial class CsRedisHelper: QuickHelperBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="redisConfig"></param>
        public static void InitializeConfiguration(RedisConfig redisConfig)
        {
            int port, poolsize, database;
            string ip, pass;
            port = redisConfig.Port;
            poolsize = redisConfig.PoolSize;
            database = redisConfig.DataBase;
            ip = redisConfig.Ip;
            pass = redisConfig.Password;
            Namefix = redisConfig.Prefix;
            Instance = new ConnectionPool(ip, port, poolsize);
            Instance.Connected += (s, o) => {
                RedisClient rc = s as RedisClient;
                if (!string.IsNullOrEmpty(pass)) rc.Auth(pass);
                if (database > 0) rc.Select(database);
            };
            SetCacheFileKeyPre(redisConfig.OverTimeCacheKeyPre);
            SetCacheFileKeys(redisConfig.OverTimeCacheKeys);
        }
    }
}
