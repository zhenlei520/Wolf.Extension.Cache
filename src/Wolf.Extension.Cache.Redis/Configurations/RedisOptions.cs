// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Redis.Configurations
{
    /// <summary>
    /// Redis缓存配置
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        ///
        /// </summary>
        public RedisOptions()
        {
            this.OverTimeCacheKeyFormat = new[] {"Absolute_{0}~_~{1}", "Sliding_{0}~_~{1}"};
        }
        /// <summary>
        /// 前缀
        /// </summary>
        public string Pre { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 储存的数据库索引
        /// </summary>
        public int DataBase { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Redis连接池连接数
        /// </summary>
        public int PoolSize { get; set; }

        /// <summary>
        /// Redis默认 Hashkey 过期格式：默认：
        /// 第一个为绝对过期格式
        /// 第二个为滑动过期格式
        /// </summary>
        public string[] OverTimeCacheKeyFormat { get; set; }

        //
        // /// <summary>
        // /// Hash缓存key 范围
        // /// </summary>
        // public List<string> OverTimeCacheKeys { get; set; }

        /// <summary>
        /// 定时清理Hash的时间 默认为500ms
        /// </summary>
        public int Timer { get; set; }
    }
}
