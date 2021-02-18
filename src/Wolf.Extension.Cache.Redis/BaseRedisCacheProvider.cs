// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extensions.Serialize.Json.Abstracts;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// 滑动验证
    /// </summary>
    public class BaseRedisCacheProvider
    {
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonProvider"></param>
        public BaseRedisCacheProvider(IJsonProvider jsonProvider)
        {
            this._jsonProvider = jsonProvider;
        }

        #region 执行


        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="overdueStrategy">缓存过期策略</param>
        /// <param name="absoluteFunc">绝对过期方法</param>
        /// <param name="slidingFunc">滑动过期方法</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual T Execute<T>(OverdueStrategy overdueStrategy, Func<T> absoluteFunc, Func<T> slidingFunc)
        {
            if (overdueStrategy == OverdueStrategy.AbsoluteExpiration)
            {
                return absoluteFunc.Invoke();
            }

            return slidingFunc.Invoke();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="overdueStrategy">缓存过期策略</param>
        /// <param name="absoluteFunc">绝对过期方法</param>
        /// <param name="slidingFunc">滑动过期方法</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual Task<T> Execute<T>(OverdueStrategy overdueStrategy, Func<Task<T>> absoluteFunc,
            Func<Task<T>> slidingFunc)
        {
            if (overdueStrategy == OverdueStrategy.AbsoluteExpiration)
            {
                return absoluteFunc.Invoke();
            }

            return slidingFunc.Invoke();
        }

        #endregion

        #region 辅助方法

        #region 将对象序列化成JSON

        /// <summary>
        /// 将对象序列化成JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : _jsonProvider.Serializer(value);
            return result;
        }

        #endregion

        #region 将JSON反序列化成对象

        /// <summary>
        /// 序列化列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        protected List<T> ConvertListObj<T>(List<string> values)
        {
            List<T> list = new List<T>();
            values.ForEach(p => { list.Add(ConvertObj<T>(p)); });
            return list;
        }

        /// <summary>
        /// 将JSON反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected T ConvertObj<T>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }

            Type t = typeof(T);
            if (string.Equals(t.Name, "string", StringComparison.OrdinalIgnoreCase))
            {
                return (T) Convert.ChangeType(value, typeof(T));
            }

            return (T)_jsonProvider.Deserialize(value,typeof(T));
        }

        #endregion

        #endregion
    }
}
