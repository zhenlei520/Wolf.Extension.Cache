// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace Wolf.Extension.Cache.Redis.Internal
{
    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisConfigs
    {
        /// <summary>
        /// 哨兵
        /// </summary>
        public string[] Sentinels { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string[] ConnectionStrings { get; set; }

        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Func<string, string> NodeRuleExternal { get; set; }
    }
}
