// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.DependencyInjection.Service;
using Wolf.Extension.Cache.Abstractions;

namespace Wolf.Extension.Cache.MemCache
{
    /// <summary>
    /// Memory缓存
    /// </summary>
    public class CacheBuilder : DefaultWeight, ICacheBuilder
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Identify => "MemCache";

        /// <summary>
        /// 权重
        /// </summary>
        public override int Weights => 98;

        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        public ICacheProvider CreateProvider(string serviceId = "")
        {
            return new CacheProvider();
        }
    }
}
