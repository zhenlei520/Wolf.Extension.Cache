// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// List存储
    /// </summary>
    public partial interface ICacheProvider
    {
        #region 同步

        /// <summary>
        /// 入队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        long ListRightPush(string key, string value);

        /// <summary>
        /// 入队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        long ListRightPush<T>(string key, T value);

        /// <summary>
        /// 出队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回队列中总数</returns>
        string ListRightPop(string key);

        /// <summary>
        /// 出队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        T ListRightPop<T>(string key);

        /// <summary>
        /// 入栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        long ListLeftPush(string key, string value);

        /// <summary>
        /// 入栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        long ListLeftPush<T>(string key, T value);

        /// <summary>
        /// 出栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回队列中总数</returns>
        string ListLeftPop(string key);

        /// <summary>
        /// 出栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        T ListLeftPop<T>(string key);

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        List<string> ListRange(string key, long count = 1000);

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        List<T> ListRange<T>(string key, long count = 1000);

        /// <summary>
        /// 根据缓存键以及对应的list的值删除
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <returns></returns>
        long ListRemove(string key, string value);

        /// <summary>
        /// 根据缓存键以及对应的list的值删除
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long ListRemove<T>(string key, T value);

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key">缓存key的值</param>
        /// <returns></returns>
        long ListLength(string key);

        #endregion

        #region 异步

        /// <summary>
        /// 入队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        Task<long> ListRightPushAsync(string key, string value);

        /// <summary>
        /// 入队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        Task<long> ListRightPushAsync<T>(string key, T value);

        /// <summary>
        /// 出队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回队列中总数</returns>
        Task<string> ListRightPopAsync(string key);

        /// <summary>
        /// 出队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string key);

        /// <summary>
        /// 入栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        Task<long> ListLeftPushAsync(string key, string value);

        /// <summary>
        /// 入栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        Task<long> ListLeftPushAsync<T>(string key, T value);

        /// <summary>
        /// 出栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回队列中总数</returns>
        Task<string> ListLeftPopAsync(string key);

        /// <summary>
        /// 出栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string key);

        /// <summary>
        /// 获取指定key的List(异步)
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        Task<List<string>> ListRangeAsync(string key, long count = 1000);

        /// <summary>
        /// 获取指定key的List(异步)
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        Task<List<T>> ListRangeAsync<T>(string key, long count = 1000);

        /// <summary>
        /// 根据缓存键以及对应的list的值删除(异步)
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <returns></returns>
        Task<long> ListRemoveAsync(string key, string value);

        /// <summary>
        /// 根据缓存键以及对应的list的值删除(异步)
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<long> ListRemoveAsync<T>(string key, T value);

        /// <summary>
        /// 获取集合中的数量(异步)
        /// </summary>
        /// <param name="key">缓存key的值</param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string key);

        #endregion
    }
}
