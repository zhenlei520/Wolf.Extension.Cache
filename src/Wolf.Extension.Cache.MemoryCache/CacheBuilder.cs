// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Extensions.Caching.Memory;
using Wolf.DependencyInjection.Service;
using Wolf.Extension.Cache.Abstractions;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    ///
    /// </summary>
    public class CacheBuilder : DefaultWeight, ICacheBuilder
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Identify => "MemoryCache";

        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        public ICacheProvider CreateProvider(string serviceId = "")
        {
            var memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
            return new CacheProvider(memoryCache);
        }
    }
}
