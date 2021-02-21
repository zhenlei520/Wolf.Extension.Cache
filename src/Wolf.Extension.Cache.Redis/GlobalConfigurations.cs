// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Wolf.Extension.Cache.Redis.Configurations;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// 全局配置
    /// </summary>
    internal class GlobalConfigurations
    {
        /// <summary>
        ///
        /// </summary>
        public static List<RedisConnectionPoolConfigs> Instance { get; internal set; }
    }
}
