// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// 存储服务配置信息
    /// 同一个服务id只有有一个配置信息，但一个服务名称可以有多个服务id
    /// </summary>
    public class CacheOptions
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务id
        /// </summary>
        public string ServiecId { get; set; }

        /// <summary>
        /// 配置信息
        /// </summary>
        public object Configuration { get; set; }
    }
}
