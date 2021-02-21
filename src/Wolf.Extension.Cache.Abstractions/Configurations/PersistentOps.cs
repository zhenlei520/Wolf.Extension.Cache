// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Wolf.Extension.Cache.Abstractions.Enum;

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// 缓存策略
    /// </summary>
    public class PersistentOps
    {
        /// <summary>
        /// 默认绝对过期，不确保原子性
        /// </summary>
        /// <param name="overdueTime"></param>
        public PersistentOps(int? overdueTime = null)
        {
            this.Strategy = OverdueStrategy.AbsoluteExpiration;
            this.IsAtomic = false;
            this.OverdueTime = null;
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

        /// <summary>
        /// 是否确保原子性，一个失败就回退
        /// </summary>
        public bool IsAtomic { get; set; }
    }
}
