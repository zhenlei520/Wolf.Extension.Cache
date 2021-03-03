// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace Wolf.Extension.Cache.Redis.Internal
{
    /// <summary>
    /// 滑动过期策略
    /// </summary>
    public interface ISlidingOverTimeProvider
    {
        /// <summary>
        /// 设置指定 key 的值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">字符串值</param>
        /// <param name="timeSpan">过期时间</param>
        /// <returns></returns>
        bool Set<T>(string key, T value, TimeSpan timeSpan);
    }
}
