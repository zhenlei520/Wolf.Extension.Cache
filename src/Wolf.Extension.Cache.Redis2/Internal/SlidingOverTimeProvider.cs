// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Wolf.Extension.Cache.Redis.Common;

namespace Wolf.Extension.Cache.Redis.Internal
{
    /// <summary>
    /// 滑动过期策略
    /// </summary>
    public class SlidingOverTimeProvider : ISlidingOverTimeProvider
    {
        private readonly ConnectionPool Instance;

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceId"></param>
        public SlidingOverTimeProvider(string serviceId)
        {
            this.Instance =
                GlobalConfigurations.Instance.Where(x => x.ServiecId == serviceId).Select(x => x.Instance)
                    .FirstOrDefault() ?? throw new Exception("获取Redis连接异常");
        }

        #region 设置指定 key 的值

        /// <summary>
        /// 设置指定 key 的值
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="value">字符串值</param>
        /// <param name="timeSpan">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan timeSpan)
        {
            using (var conn = QuickHelperBase.Instance.GetConnection())
            {
                key = string.Concat(QuickHelperBase.Prefix, key);
                if (timeSpan > TimeSpan.Zero)
                    return conn.Client.Set(key, value, timeSpan) == "OK";
                if (timeSpan < TimeSpan.Zero)
                    return false;
                return conn.Client.Set(key, value) == "OK";
            }
        }

        #endregion
    }
}
