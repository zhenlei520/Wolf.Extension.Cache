// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Wolf.DependencyInjection.Abstracts;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// 存储服务
    /// </summary>
    public interface ICacheFactory : ISingleInstance
    {
        /// <summary>
        /// 创建多个缓存服务
        /// 服务名称为空时获取所有的缓存实现
        /// </summary>
        /// <param name="serviceName">服务名称(允许为空)</param>
        /// <returns></returns>
        IEnumerable<ICacheProvider> CreateProviders(string serviceName = "");

        /// <summary>
        /// 创建缓存服务
        /// 如果注入多个服务则仅获取第一个配置
        /// </summary>
        /// <returns></returns>
        ICacheProvider CreateProvider();

        /// <summary>
        /// 创建缓存服务
        /// 如果注入同一个服务注入多次，则仅取第一个配置
        /// </summary>
        /// <param name="serviceName">服务名称,默认查询权重最高的</param>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        ICacheProvider CreateProvider(string serviceName, string serviceId = "");
    }
}
