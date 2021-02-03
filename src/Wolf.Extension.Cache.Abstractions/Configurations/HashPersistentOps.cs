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
        public HashPersistentOps(bool isCanHashExpire=false)
        {
            IsCanHashExpire = isCanHashExpire;
            TimeSpan = null;
        }

        /// <summary>
        /// 是否可以过期，默认不设置过期
        /// </summary>
        public bool IsCanHashExpire { get; set; }

        /// <summary>
        /// 过期时间，默认永不过期，null：永不过期
        /// </summary>
        public TimeSpan? TimeSpan { get; set; }
    }
}
