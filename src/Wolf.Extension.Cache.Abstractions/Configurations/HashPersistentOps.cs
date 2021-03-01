// Copyright (c) zhenlei520 All rights reserved.

using System;
using Wolf.Extension.Cache.Abstractions.Enum;

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// Hash键策略
    /// </summary>
    public class HashPersistentOps
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="isCanHashExpire">是否指定的hash键可以过期，默认整键过期</param>
        /// <param name="overdueTime">过期时间</param>
        public HashPersistentOps(bool isCanHashExpire = false, int? overdueTime = null)
        {
            if (isCanHashExpire)
            {
                this.HashOverdueTime = overdueTime;
                this.HashOverdueTimeSpan = this.HashOverdueTime != null ? TimeSpan.FromSeconds(this.HashOverdueTime.Value) : TimeSpan.Zero;
            }

            this.IsCanHashExpire = isCanHashExpire;
        }

        /// <summary>
        /// 过期策略
        /// </summary>
        public OverdueStrategy Strategy { get; set; }

        /// <summary>
        /// 是否单个Hash key过期
        /// 如果不是单键过期，即为整键过期
        /// </summary>
        public bool IsCanHashExpire { get; private set; }

        /// <summary>
        /// 过期时间，单位：s
        /// 永不过期：null
        /// </summary>
        public int? HashOverdueTime { get; private set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan HashOverdueTimeSpan { get; private set; }
    }
}
