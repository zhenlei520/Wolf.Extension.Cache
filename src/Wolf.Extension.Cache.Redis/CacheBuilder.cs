﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.DependencyInjection.Service;
using Wolf.Extension.Cache.Abstractions;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    ///
    /// </summary>
    public class CacheBuilder : DefaultWeight, ICacheBuilder
    {
        public int Weights { get; }
        public string Identify { get; }

        public ICacheProvider CreateProvider(string serviceId = "")
        {
            throw new System.NotImplementedException();
        }
    }
}