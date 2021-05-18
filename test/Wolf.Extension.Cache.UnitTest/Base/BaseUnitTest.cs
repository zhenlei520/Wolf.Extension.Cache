// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using Wolf.DependencyInjection;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Redis;
using Wolf.Extension.Cache.Redis.Configurations;
using Wolf.Extension.Cache.Redis.Internal;
using Wolf.Extensions.Serialize.Json.Abstracts;
using Wolf.Extensions.Serialize.Json.Newtonsoft;

namespace Wolf.Extension.Cache.UnitTest.Base
{
    public class BaseUnitTest
    {
        protected readonly ICacheProvider _cacheProvider;

        /// <summary>
        ///
        /// </summary>
        public BaseUnitTest()
        {
            var provider = GetServiceProvider();
            var jsonFactory = provider.GetService<IJsonFactory>();
            var redisOption = provider.GetService<CacheOptions>();
            var cacheBuilder = provider.GetService<ICacheBuilder>();

            var options=provider.GetService<IEnumerable<CacheOptions>>();
            RedisConfigs config = options.Select(x=>x.Configuration).FirstOrDefault() as RedisConfigs;
            var csRedisClient = new CSRedisClient(provider.GetService<IJsonFactory>(), config.NodeRuleExternal,
                config.Sentinels, config.ReadOnly, null, config.ConnectionStrings);

            var s=csRedisClient.SIsMember("tes", "asd");
            _cacheProvider=provider.GetService<ICacheFactory>().CreateProvider();
            // CSRedisClient client = new CSRedisClient(new JsonFactory(new List<IJsonBuilder>()
            // {
            //     new JsonBuilder()
            // }), "127.0.0.1:6379,password=,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240,prefix=wolf");
            // _cacheProvider = new CacheProvider(client);
        }

        protected IServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddNewtonsoftJson();
            serviceCollection.AddRedis(new RedisOptions("127.0.0.1", 6379, "", "", 0, 50, false), "");
            return serviceCollection.AddAutoInject("Wolf").BuildServiceProvider();
        }
    }
}
