﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Wolf.Extension.Cache.Abstractions.Configurations;

namespace Wolf.Extension.Cache.Abstractions.Common
{
    /// <summary>
    /// 策略
    /// </summary>
    public static class PersistentOpsExtensions
    {
        /// <summary>
        /// 得到策略
        /// </summary>
        /// <param name="persistentOps"></param>
        /// <returns></returns>
        public static PersistentOps Get(this PersistentOps persistentOps)
        {
            return persistentOps ?? new PersistentOps();
        }

        /// <summary>
        /// 得到策略
        /// </summary>
        /// <param name="persistentOps"></param>
        /// <returns></returns>
        public static BasePersistentOps Get(this BasePersistentOps persistentOps)
        {
            return persistentOps ?? new BasePersistentOps(TimeSpan.Zero);
        }

        /// <summary>
        /// 得到策略
        /// </summary>
        /// <param name="persistentOps"></param>
        /// <returns></returns>
        public static ListPersistentOps Get(this ListPersistentOps persistentOps)
        {
            return persistentOps ?? new ListPersistentOps();
        }
    }
}
