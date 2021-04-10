// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
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
        public string ServiceName => "";

        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICacheProvider> CreateProviders()
        {
            return new List<ICacheProvider>();
        }

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
