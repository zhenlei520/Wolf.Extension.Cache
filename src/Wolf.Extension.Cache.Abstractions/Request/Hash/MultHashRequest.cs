// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;

namespace Wolf.Extension.Cache.Abstractions.Request.Hash
{
    /// <summary>
    /// 多个Hash缓存
    /// </summary>
    public class MultHashRequest<T>
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Hash缓存集合
        /// </summary>
        public IEnumerable<T> List { get; set; }
    }
}
