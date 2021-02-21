// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.Extension.Cache.Redis.Common;

namespace Wolf.Extension.Cache.Redis.Configurations
{
    /// <summary>
    /// Redis连接配置
    /// </summary>
    internal class RedisConnectionPoolConfigs
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public string ServiecId { get; set; }

        /// <summary>
        /// 连接池
        /// </summary>
        public ConnectionPool Instance { get; set; }
    }
}
