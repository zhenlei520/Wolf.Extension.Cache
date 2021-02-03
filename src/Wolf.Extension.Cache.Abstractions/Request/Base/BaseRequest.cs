// Copyright (c) zhenlei520 All rights reserved.

namespace Wolf.Extension.Cache.Abstractions.Request.Base
{
    /// <summary>
    /// 缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRequest<T>
    {
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
