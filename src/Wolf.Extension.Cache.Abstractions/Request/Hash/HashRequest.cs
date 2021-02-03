// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Abstractions.Request.Hash
{
    /// <summary>
    ///
    /// </summary>
    public class HashRequest<T>
    {
        /// <summary>
        /// hash键
        /// </summary>
        public string HashKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }
    }
}
