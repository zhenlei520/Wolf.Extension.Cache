// Copyright (c) zhenlei520 All rights reserved.

using System;

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// Hash键策略
    /// </summary>
    public class HashPersistentOps : PersistentOps
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="isCanHashExpire">是否可以过期，默认不设置过期</param>
        /// <param name="overdueTime">过期时间</param>
        public HashPersistentOps(bool isCanHashExpire = false, int? overdueTime = null) : base(isCanHashExpire
            ? null
            : overdueTime)
        {
            if (isCanHashExpire)
            {
                this.HashOverdueTime = overdueTime;
                this.HashOverdueTimeSpan = this.HashOverdueTime != null ? TimeSpan.FromSeconds(this.HashOverdueTime.Value) : TimeSpan.Zero;
            }

            this.IsCanHashExpire = isCanHashExpire;
        }

        /// <summary>
        /// 是否单个Hash key过期
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
