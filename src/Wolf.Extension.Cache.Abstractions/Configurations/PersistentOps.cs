// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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
        public PersistentOps()
        {
            Strategy = OverdueStrategy.AbsoluteExpiration;
            IsAtomic = false;
        }

        /// <summary>
        /// 过期策略
        /// </summary>
        public OverdueStrategy Strategy { get; set; }

        /// <summary>
        /// 是否确保原子性，一个失败就回退
        /// </summary>
        public bool IsAtomic { get; set; }
    }
}
