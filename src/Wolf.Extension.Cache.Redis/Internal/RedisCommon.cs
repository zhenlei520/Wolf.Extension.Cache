// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using CSRedis;
using Wolf.Extension.Cache.Abstractions.Enum;

namespace Wolf.Extension.Cache.Redis.Internal
{
    /// <summary>
    /// 帮助类
    /// </summary>
    internal static class RedisCommon
    {
        /// <summary>
        /// 得到RedisExistence
        /// </summary>
        /// <param name="setStrategy">设置策略</param>
        /// <returns></returns>
        public static RedisExistence? GetRedisExistence(this SetStrategy? setStrategy)
        {
            if (setStrategy == null)
            {
                return null;
            }

            if (setStrategy == SetStrategy.NoFind)
            {
                return RedisExistence.Nx;
            }

            if (setStrategy == SetStrategy.Exist)
            {
                return RedisExistence.Xx;
            }

            throw new NotSupportedException(nameof(setStrategy));
        }
    }
}
