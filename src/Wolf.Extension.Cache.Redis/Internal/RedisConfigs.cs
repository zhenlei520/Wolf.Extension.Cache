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
        ///
        /// </summary>
        /// <param name="connectionString">mymaster,password=123456,poolsize=50,connectTimeout=200,ssl=false</param>
        /// <param name="sentinels">哨兵</param>
        /// <param name="readOnly">只读</param>
        public RedisConfigs(string connectionString, string[] sentinels, bool readOnly)
        {
            this.ConnectionStrings = new[] {connectionString};
            this.Sentinels = sentinels;
            this.ReadOnly = readOnly;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString">mymaster,password=123456,poolsize=50,connectTimeout=200,ssl=false</param>
        /// <param name="sentinels">哨兵</param>
        /// <param name="readOnly">只读</param>
        public RedisConfigs(string connectionString)
        {
            this.ConnectionStrings = new[] {connectionString};
            this.Sentinels = new string[0];
            this.ReadOnly = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nodeRule"></param>
        /// <param name="connectionStrings"></param>
        /// <param name="connectionString">mymaster,password=123456,poolsize=50,connectTimeout=200,ssl=false</param>
        /// <param name="sentinels">哨兵</param>
        /// <param name="readOnly">只读</param>
        public RedisConfigs(Func<string, string> nodeRule,string[] connectionStrings)
        {
            this.ConnectionStrings = connectionStrings;
            this.Sentinels = new string[0];
            this.ReadOnly = false;
            this.NodeRuleExternal = nodeRule;
        }

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
