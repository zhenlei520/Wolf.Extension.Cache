// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Wolf.Extension.Cache.Abstractions.Enum
{
    /// <summary>
    /// 过期策略
    /// </summary>
    public enum OverdueStrategy
    {
        /// <summary>
        /// 绝对过期
        /// </summary>
        [Description("绝对过期")]AbsoluteExpiration = 1,

        /// <summary>
        /// 滑动过期
        /// </summary>
        [Description("滑动过期")]SlidingExpiration = 2
    }
}
