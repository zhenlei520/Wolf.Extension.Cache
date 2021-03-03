// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wolf.DependencyInjection.Service;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extensions.Serialize.Json.Abstracts;
using Wolf.Systems.Core;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    ///
    /// </summary>
    public class CacheBuilder : DefaultWeight, ICacheBuilder
    {
        private readonly IEnumerable<CacheOptions> _cacheOptions;
        private readonly IJsonFactory _jsonFactory;

        public CacheBuilder(IEnumerable<CacheOptions> cacheOptions, IJsonFactory jsonFactory)
        {
            this._cacheOptions = cacheOptions;
            this._jsonFactory = jsonFactory;
        }

        /// <summary>
        ///
        /// </summary>
        public string Identify => "Redis";

        /// <summary>
        /// 创建多个缓存服务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICacheProvider> CreateProviders()
        {
            return this._cacheOptions
                .Where(x => x.ServiceName.Equals(GlobalConfigurations.ServiceName, StringComparison.OrdinalIgnoreCase))
                .Select(option => new CacheProvider(this._jsonFactory, option));
        }

        /// <summary>
        /// 创建缓存服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        public ICacheProvider CreateProvider(string serviceId = "")
        {
            if (serviceId.IsNullOrWhiteSpace())
            {
                return this.CreateProviders().FirstOrDefault();
            }

            var option = this._cacheOptions.FirstOrDefault(x =>
                             x.ServiceName.Equals(GlobalConfigurations.ServiceName,
                                 StringComparison.OrdinalIgnoreCase) &&
                             x.ServiecId.Equals(serviceId, StringComparison.OrdinalIgnoreCase)) ??
                         throw new ArgumentNullException("未发现" + nameof(serviceId) + $"为{serviceId}配置信息");
            return new CacheProvider(this._jsonFactory, option);
        }
    }
}
