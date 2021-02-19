// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.Extension.Cache.Abstractions.Configurations;

namespace Wolf.Extension.Cache.Abstractions.Common
{
    /// <summary>
    /// Hash键策略扩展
    /// </summary>
    public static class HashPersistentOpsExtensions
    {
        /// <summary>
        /// 得到策略
        /// </summary>
        /// <param name="persistentOps"></param>
        /// <returns></returns>
        public static HashPersistentOps Get(this HashPersistentOps persistentOps)
        {
            return persistentOps ?? new HashPersistentOps();
        }
    }
}
