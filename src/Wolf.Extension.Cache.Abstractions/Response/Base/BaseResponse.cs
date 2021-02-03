// Copyright (c) zhenlei520 All rights reserved.

namespace Wolf.Extension.Cache.Abstractions.Response.Base
{
    /// <summary>
    /// 基础响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        public BaseResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public BaseResponse(string key, T value) : this()
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// 缓存键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 缓存值
        /// </summary>
        public T Value { get; set; }
    }
}
