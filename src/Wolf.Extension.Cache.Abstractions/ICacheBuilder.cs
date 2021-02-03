// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.DependencyInjection.Abstracts.Service;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// 存储Builder
    /// </summary>
    public interface ICacheBuilder : IIdentify
    {
        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceId">服务id，默认为空</param>
        /// <returns></returns>
        ICacheProvider CreateProvider(string serviceId = "");
    }
}
