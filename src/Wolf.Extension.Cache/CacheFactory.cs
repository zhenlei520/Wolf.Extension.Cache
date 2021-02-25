// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Systems.Core;

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

        /// <summary>
        /// 创建多个缓存服务
        /// 服务名称为空时获取所有的缓存实现
        /// </summary>
        /// <param name="serviceName">服务名称(允许为空)</param>
        /// <returns></returns>
        public IEnumerable<ICacheProvider> CreateProviders(string serviceName = "")
        {
            return this._cacheBuilders.Where(x => x.Identify.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                .SelectMany(x => x.CreateProviders());
        }

        #region 创建缓存服务

        /// <summary>
        /// 创建缓存服务
        /// 如果注入多个服务则仅获取第一个配置
        /// </summary>
        /// <returns></returns>
        public ICacheProvider CreateProvider()
        {
            return this._cacheBuilders.FirstOrDefault()?.CreateProviders() ?? throw new NotImplementedException();
        }

        #endregion

        #region 创建缓存服务

        /// <summary>
        /// 创建缓存服务
        /// 如果注入同一个服务注入多次，则仅取第一个配置
        /// </summary>
        /// <param name="serviceName">服务名称,默认查询权重最高的</param>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        public ICacheProvider CreateProvider(string serviceName, string serviceId = "")
        {
            if (string.IsNullOrWhiteSpace(serviceId))
            {
                return this.CreateBuilder(serviceName).CreateProviders();
            }

            return this.CreateBuilder(serviceName).CreateProvider(serviceId);
        }

        #endregion

        #region private methods

        #region 得到Builder

        /// <summary>
        /// 得到Builder
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        private ICacheBuilder CreateBuilder(string serviceName)
        {
            if (serviceName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            return this._cacheBuilders.FirstOrDefault(x =>
                       x.Identify.Equals(serviceName, StringComparison.OrdinalIgnoreCase)) ??
                   throw new NotSupportedException($"服务名称为：{serviceName}");
        }

        #endregion

        #endregion
    }
}
