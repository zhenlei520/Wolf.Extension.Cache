// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using CSRedis;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Redis;
using Wolf.Extensions.Serialize.Json;
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
            CSRedisClient client = new CSRedisClient(new JsonFactory(new List<IJsonBuilder>()
            {
                new JsonBuilder()
            }), "127.0.0.1:6379,password=,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240,prefix=wolf");
            _cacheProvider = new CacheProvider(client);
        }
    }
}
