// Copyright (c) zhenlei520 All rights reserved.

namespace Wolf.Extension.Cache.Abstractions.Response.Hash
{
    /// <summary>
    /// Hash缓存响应信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HashResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        public HashResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key">hash键</param>
        /// <param name="value">值</param>
        public HashResponse(string key, T value) : this()
        {
            this.HashKey = key;
            this.HashValue = value;
        }

        /// <summary>
        /// hash缓存键
        /// </summary>
        public string HashKey { get; set; }

        /// <summary>
        /// 缓存值
        /// </summary>
        public T HashValue { get; set; }
    }
}
