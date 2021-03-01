// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// 缓存策略
    /// </summary>
    public class PersistentOps : BasePersistentOps
    {
        /// <summary>
        /// 默认绝对过期，不确保原子性
        /// </summary>
        /// <param name="overdueTime"></param>
        public PersistentOps(int? overdueTime = null) : base(overdueTime)
        {
            this.IsAtomic = false;
        }

        /// <summary>
        /// 是否确保原子性，一个失败就回退
        /// </summary>
        public bool IsAtomic { get; set; }
    }
}
