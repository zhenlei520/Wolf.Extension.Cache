// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.DependencyInjection.Service;
using Wolf.Extension.Cache.Abstractions;

namespace Wolf.Extension.Cache
{
    /// <summary>
    ///
    /// </summary>
    public class NullCacheBuilder : DefaultWeight, ICacheBuilder
    {
        /// <summary>
        ///
        /// </summary>
        public string Identify => "Null";

        #region 创建缓存服务

        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceId">服务id，默认为空</param>
        /// <returns></returns>
        public ICacheProvider CreateProvider(string serviceId = "")
        {
            return new NullCacheProvider();
        }

        #endregion
    }
}
