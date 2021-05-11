// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// Sort Set（异步）
    /// </summary>
    public partial interface ICacheProvider
    {
        /// <summary>
        /// 向集合添加一个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        Task<bool> SetAddAsync(string key, string value);

        /// <summary>
        /// 向集合添加一个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAddAsync<T>(string key, T value);

        /// <summary>
        /// 向集合添加一个或多个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <returns></returns>
        Task<bool> SetAddRangeAsync(string key, params string[] values);

        /// <summary>
        /// 向集合添加一个或多个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAddRangeAsync<T>(string key, params T[] values);

        /// <summary>
        /// 得到指定缓存的长度（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<long> SetLengthAsync(string key);

        /// <summary>
        /// 得到给定缓存key集合的差集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        Task<string[]> SetDiffAsync(params string[] keys);

        /// <summary>
        /// 得到给定缓存key集合的差集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        Task<T[]> SetDiffAsync<T>(params string[] keys);

        /// <summary>
        /// 将指定缓存key集合的差集保存到指定的缓存key中（异步）
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        Task<bool> SetDiffStoreAsync(string destination, params string[] keys);

        /// <summary>
        /// 得到给定集合的交集（异步）
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        Task<List<string>> SetInterAsync(params string[] keys);

        /// <summary>
        /// 得到给定集合的交集（异步）
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        Task<List<T>> SetInterAsync<T>(params string[] keys);

        /// <summary>
        /// 将给定集合的交集存储到指定的缓存key中（异步）
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        Task<bool> SetInterStoreAsync(string destination, params string[] keys);

        /// <summary>
        /// 判断指定的缓存key的value是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        Task<bool> SetExistsAsync(string key, string value);

        /// <summary>
        /// 判断指定的缓存key的value是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetExistsAsync<T>(string key, T value);

        /// <summary>
        /// 根据缓存key得到集合中的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<List<string>> SetGetAsync(string key);

        /// <summary>
        /// 根据缓存key得到集合中的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<List<T>> SetGetAsync<T>(string key);

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合（异步）
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        Task<bool> SetMoveAsync(string key, string optKey, string value);

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合（异步）
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetMoveAsync<T>(string key, string optKey, T value);

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        Task<string> SetPopAsync(string key);

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        Task<T> SetPopAsync<T>(string key);

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        Task<string[]> SetPopAsync(string key, int count);

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        Task<T[]> SetPopAsync<T>(string key, int count);

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        Task<string> SetRandomGetAsync(string key);

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        Task<T> SetRandomGetAsync<T>(string key);

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        Task<string[]> SetRandomGetAsync(string key, int count);

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        Task<T[]> SetRandomGetAsync<T>(string key, int count);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        Task<long> SetRemAsync(string key, string value);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">多个缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        Task<long> SetRemAsync(string key, params string[] values);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        Task<long> SetRemAsync<T>(string key, T value);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        Task<long> SetRemAsync<T>(string key, params T[] values);

        /// <summary>
        /// 得到给定集合的并集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns>并集成员的列表</returns>
        Task<string[]> SetUnionAsync(params string[] keys);

        /// <summary>
        /// 得到给定集合的并集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>并集成员的列表</returns>
        Task<T[]> SetUnionAsync<T>(params string[] keys);

        /// <summary>
        /// 将指定的缓存key集合并到一起且保存到新的集合中并返回结果集中的元素数量（异步）
        /// </summary>
        /// <param name="optKey">新的缓存key</param>
        /// <param name="keys">指定缓存key集合</param>
        /// <returns>结果集中的元素数量</returns>
        Task<long> SetUnionStoreAsync(string optKey, params string[] keys);
    }
}
