// Copyright (c) zhenlei520 All rights reserved.

using System;
using Wolf.Extension.Cache.Abstractions.Enum;

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// 基本缓存策略
    /// </summary>
    public class BasePersistentOps
    {
        /// <summary>
        /// 默认绝对过期，不确保原子性
        /// </summary>
        /// <param name="overdueTime"></param>
        public BasePersistentOps(int? overdueTime = null)
        {
            this.Strategy = OverdueStrategy.AbsoluteExpiration;
            this.OverdueTime = overdueTime;
            this.OverdueTimeSpan = this.OverdueTime != null ? TimeSpan.FromSeconds(this.OverdueTime.Value) : TimeSpan.Zero;
        }

        /// <summary>
        /// 过期策略
        /// </summary>
        public OverdueStrategy Strategy { get; set; }

        /// <summary>
        /// 过期时间，单位：s
        /// 永不过期：null
        /// </summary>
        public int? OverdueTime { get; private set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan OverdueTimeSpan { get; private set; }
    }
}
