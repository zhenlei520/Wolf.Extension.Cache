// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions;

namespace Wolf.Extension.Cache
{
    /// <summary>
    /// 缓存工厂
    /// </summary>
    public class CacheFactory : ICacheFactory
    {
        private readonly IEnumerable<ICacheBuilder> _cacheBuilders;

        /// <summary>
        ///
        /// </summary>
        /// <param name="cacheBuilders"></param>
        public CacheFactory(IEnumerable<ICacheBuilder> cacheBuilders)
        {
            this._cacheBuilders = cacheBuilders;
        }

        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceName">服务名称,默认查询权重最高的</param>
        /// <returns></returns>
        public ICacheBuilder Create(string serviceName = "")
        {
            return this._cacheBuilders.FirstOrDefault(x =>
                x.Identify.Equals(serviceName, StringComparison.OrdinalIgnoreCase)) ?? new NullCacheBuilder();
        }
    }
}
