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
        /// 是否开启滑动过期
        /// </summary>
        /// <param name="isOpenSlidingExpiration"></param>
        public RedisOptions(bool isOpenSlidingExpiration = false)
        {
            this.SlidingOverTimeCacheKeyFormat = new[] {"Sliding_{0}", "{0}"};
            this.SlidingOverTimeCacheCount = 50;
            this.HashOverTimeCacheKeyFormat = new[]
                {"Absolute_{0}", "Absolute_{0}~_~{1}", "Sliding_{0}", "Sliding_{0}~_~{1}"};
            this.HashOverTimeCacheCount = 50;
            this.IsOpenSlidingExpiration = isOpenSlidingExpiration;
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
        /// Redis 默认缓存键滑动缓存过期 格式：
        /// 第一个为滑动过期格式的键。第二个为滑动过期的值
        /// </summary>
        public string[] SlidingOverTimeCacheKeyFormat { get; set; }

        /// <summary>
        /// 缓存键滑动过期的缓存键个数
        /// </summary>
        public int SlidingOverTimeCacheCount { get; set; }

        /// <summary>
        /// Redis默认 Hashkey 过期值的格式：默认：
        /// 第一个为绝对过期的键，第二个为绝对过期的值
        /// 第三个为滑动过期的键，第四个为滑动过期的值
        /// </summary>
        public string[] HashOverTimeCacheKeyFormat { get; set; }

        /// <summary>
        /// Hash类型过期的缓存键有多少个
        /// </summary>
        public int HashOverTimeCacheCount { get; set; }

        //
        // /// <summary>
        // /// Hash缓存key 范围
        // /// </summary>
        // public List<string> OverTimeCacheKeys { get; set; }

        /// <summary>
        /// 定时清理Hash的时间 默认为500ms
        /// </summary>
        public int Timer { get; set; }

        /// <summary>
        /// 是否开启滑动过期
        /// </summary>
        public bool IsOpenSlidingExpiration { get; set; }
    }
}
