// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Wolf.DependencyInjection.Service;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extensions.Serialize.Json.Abstracts;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    ///
    /// </summary>
    public class CacheBuilder : DefaultWeight, ICacheBuilder
    {
        private readonly IEnumerable<CacheOptions> _options;
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="jsonFactory"></param>
        public CacheBuilder(IEnumerable<CacheOptions> options, IJsonFactory jsonFactory)
        {
            this._options = options;
            this._jsonProvider = jsonFactory.Create();
        }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Identify => "MemoryCache";

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
            var memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

            return new CacheProvider(memoryCache, GetOptions(serviceId));
        }

        #region 得到配置文件

        /// <summary>
        /// 得到配置文件
        /// </summary>
        /// <param name="serviceId">服务id（无作用）</param>
        /// <returns></returns>
        private Wolf.Extension.Cache.MemoryCache.Configurations.MemoryCacheOptions GetOptions(string serviceId)
        {
            var options =
                _options.FirstOrDefault(x => x.ServiceName.Equals(Identify, StringComparison.OrdinalIgnoreCase));
            if (options == null)
            {
                return new Configurations.MemoryCacheOptions();
            }

            return
                this._jsonProvider.Deserialize<Configurations.MemoryCacheOptions>(
                    this._jsonProvider.Serializer(options.Configuration));
        }

        #endregion
    }
}
