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











        /// <summary>移除指定ListId的内部List的值</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long ListRemove<T>(string key, T value);

        /// <summary>获取指定key的List</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<string> ListRange(string key, long count = 1000);

        /// <summary>获取指定key的List</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<T> ListRange<T>(string key, long count = 1000) where T : class, new();

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key">缓存key的值</param>
        /// <returns></returns>
        long ListLength(string key);

        /// <summary>移除指定ListId的内部List的值</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListRemoveAsync<T>(string key, T value);

        /// <summary>获取指定key的List</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<string>> ListRangeAsync(string key, long count = 1000);

        /// <summary>获取指定key的List</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<T>> ListRangeAsync<T>(string key, long count = 1000) where T : class, new();

        /// <summary>入队</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key, T value);

        /// <summary>出队</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> ListRightPopAsync(string key);

        /// <summary>出队</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string key) where T : class, new();

        /// <summary>入栈</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key, T value);

        /// <summary>出栈</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string key);

        /// <summary>出栈</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string key) where T : class, new();

        /// <summary>获取集合中的数量</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string key);
    }
}
