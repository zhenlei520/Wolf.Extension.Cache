// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;

namespace Wolf.Extension.Cache.Abstractions.Response.Hash
{
    /// <summary>
    /// Hash Get响应值
    /// </summary>
    public class HashMultResponse<T>
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Hash信息
        /// </summary>
        public IEnumerable<HashResponse<T>> List { get; set; }
    }
}
