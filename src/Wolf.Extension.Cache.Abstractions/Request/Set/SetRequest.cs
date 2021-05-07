// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Abstractions.Request.Set
{
    /// <summary>
    /// set
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SetRequest<T>
    {
        /// <summary>
        ///
        /// </summary>
        public SetRequest()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="data">值</param>
        public SetRequest(string key, T data) : this()
        {
            Key = key;
            Data = data;
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
