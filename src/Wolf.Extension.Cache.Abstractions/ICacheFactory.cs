// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// 存储服务
    /// </summary>
    public interface ICacheFactory
    {
        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceName">服务名称,默认查询权重最高的</param>
        /// <returns></returns>
        ICacheBuilder Create(string serviceName="");
    }
}
