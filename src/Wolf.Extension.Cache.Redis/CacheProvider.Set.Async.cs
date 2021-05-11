// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolf.Systems.Core;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// set 存储异步
    /// </summary>
    public partial class CacheProvider
    {
        #region 向集合添加一个成员（异步）

        /// <summary>
        /// 向集合添加一个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public async Task<bool> SetAddAsync(string key, string value)
        {
            return await this._client.SAddAsync(key, value) > 0;
        }

        #endregion

        #region 向集合添加一个成员（异步）

        /// <summary>
        /// 向集合添加一个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> SetAddAsync<T>(string key, T value)
        {
            return await this._client.SAddAsync(key, value) > 0;
        }

        #endregion

        #region 向集合添加一个或多个成员（异步）

        /// <summary>
        /// 向集合添加一个或多个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <returns></returns>
        public async Task<bool> SetAddRangeAsync(string key, params string[] values)
        {
            return await this._client.SAddAsync(key, values) > 0;
        }

        #endregion

        #region 向集合添加一个或多个成员（异步）

        /// <summary>
        /// 向集合添加一个或多个成员（异步）
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> SetAddRangeAsync<T>(string key, params T[] values)
        {
            return await this._client.SAddAsync(key, values) > 0;
        }

        #endregion

        #region 得到指定缓存的长度（异步）

        /// <summary>
        /// 得到指定缓存的长度（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public async Task<long> SetLengthAsync(string key)
        {
            if (key.IsNullOrWhiteSpace())
            {
                return 0;
            }

            return await this._client.SCardAsync(key);
        }

        #endregion

        #region 得到给定缓存key集合的差集（异步）

        /// <summary>
        /// 得到给定缓存key集合的差集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        public async Task<string[]> SetDiffAsync(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return new string[0];
            }

            return await this._client.SDiffAsync(keys);
        }

        #endregion

        #region 得到给定缓存key集合的差集（异步）

        /// <summary>
        /// 得到给定缓存key集合的差集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        public async Task<T[]> SetDiffAsync<T>(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return new T[0];
            }

            return await this._client.SDiffAsync<T>(keys);
        }

        #endregion

        #region 将指定缓存key集合的差集保存到指定的缓存key中（异步）

        /// <summary>
        /// 将指定缓存key集合的差集保存到指定的缓存key中（异步）
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public async Task<bool> SetDiffStoreAsync(string destination, params string[] keys)
        {
            return await this._client.SDiffStoreAsync(destination, keys) > 0;
        }

        #endregion

        #region 得到给定集合的交集（异步）

        /// <summary>
        /// 得到给定集合的交集（异步）
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public async Task<List<string>> SetInterAsync(params string[] keys)
        {
            return (await this._client.SInterAsync(keys)).ToList();
        }

        #endregion

        #region 得到给定集合的交集（异步）

        /// <summary>
        /// 得到给定集合的交集（异步）
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public async Task<List<T>> SetInterAsync<T>(params string[] keys)
        {
            return (await this._client.SInterAsync<T>(keys)).ToList();
        }

        #endregion

        #region 将给定集合的交集存储到指定的缓存key中（异步）

        /// <summary>
        /// 将给定集合的交集存储到指定的缓存key中（异步）
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public async Task<bool> SetInterStoreAsync(string destination, params string[] keys)
        {
            return await this._client.SInterStoreAsync(destination, keys) > 0;
        }

        #endregion

        #region 判断指定的缓存key的value是否存在（异步）

        /// <summary>
        /// 判断指定的缓存key的value是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public Task<bool> SetExistsAsync(string key, string value)
        {
            return this._client.SIsMemberAsync(key, value);
        }

        #endregion

        #region 判断指定的缓存key的value是否存在（异步）

        /// <summary>
        /// 判断指定的缓存key的value是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SetExistsAsync<T>(string key, T value)
        {
            return this._client.SIsMemberAsync(key, value);
        }

        #endregion

        #region 判断指定的缓存key的value是否存在（异步）

        /// <summary>
        /// 根据缓存key得到集合中的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public async Task<List<string>> SetGetAsync(string key)
        {
            return (await this._client.SMembersAsync(key)).ToList();
        }

        #endregion

        #region 根据缓存key得到集合中的所有成员（异步）

        /// <summary>
        /// 根据缓存key得到集合中的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public async Task<List<T>> SetGetAsync<T>(string key)
        {
            return (await this._client.SMembersAsync<T>(key)).ToList();
        }

        #endregion

        #region 根据缓存key将源集合移动到destination集合（异步）

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合（异步）
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        public Task<bool> SetMoveAsync(string key, string optKey, string value)
        {
            return this._client.SMoveAsync(key, optKey, value);
        }

        #endregion

        #region 根据缓存key将源集合移动到destination集合（异步）

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合（异步）
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SetMoveAsync<T>(string key, string optKey, T value)
        {
            return this._client.SMoveAsync(key, optKey, value);
        }

        #endregion

        #region 根据缓存key获取一个随机元素并移除（异步）

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        public Task<string> SetPopAsync(string key)
        {
            return this._client.SPopAsync(key);
        }

        #endregion

        #region 根据缓存key获取一个随机元素并移除（异步）

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        public Task<T> SetPopAsync<T>(string key)
        {
            return this._client.SPopAsync<T>(key);
        }

        #endregion

        #region 根据缓存key获取count个随机元素并移除（异步）

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        public Task<string[]> SetPopAsync(string key, int count)
        {
            return this._client.SPopAsync(key, count);
        }

        #endregion

        #region 根据缓存key获取count个随机元素并移除（异步）

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        public Task<T[]> SetPopAsync<T>(string key, int count)
        {
            return this._client.SPopAsync<T>(key, count);
        }

        #endregion

        #region 根据缓存key获取一个随机元素但不移除（异步）

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        public Task<string> SetRandomGetAsync(string key)
        {
            return this._client.SRandMemberAsync(key);
        }

        #endregion

        #region 根据缓存key获取一个随机元素但不移除（异步）

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        public Task<T> SetRandomGetAsync<T>(string key)
        {
            return this._client.SRandMemberAsync<T>(key);
        }

        #endregion

        #region 根据缓存key获取count个随机元素但不移除（异步）

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        public Task<string[]> SetRandomGetAsync(string key, int count)
        {
            return this._client.SRandMembersAsync(key, count);
        }

        #endregion

        #region 根据缓存key获取count个随机元素但不移除（异步）

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        public Task<T[]> SetRandomGetAsync<T>(string key, int count)
        {
            return this._client.SRandMembersAsync<T>(key, count);
        }

        #endregion

        #region 根据缓存key以及缓存值移除指定元素（异步）

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public Task<long> SetRemAsync(string key, string value)
        {
            return this._client.SRemAsync(key, value);
        }

        #endregion

        #region 根据缓存key以及缓存值移除指定元素（异步）

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">多个缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public Task<long> SetRemAsync(string key, params string[] values)
        {
            return this._client.SRemAsync(key, values);
        }

        #endregion

        #region 根据缓存key以及缓存值移除指定元素（异步）

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public Task<long> SetRemAsync<T>(string key, T value)
        {
            return this._client.SRemAsync<T>(key, value);
        }

        #endregion

        #region 根据缓存key以及缓存值移除指定元素（异步）

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public Task<long> SetRemAsync<T>(string key, params T[] values)
        {
            return this._client.SRemAsync<T>(key, values);
        }

        #endregion

        #region 得到给定集合的并集（异步）

        /// <summary>
        /// 得到给定集合的并集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns>并集成员的列表</returns>
        public Task<string[]> SetUnionAsync(params string[] keys)
        {
            return this._client.SUnionAsync(keys);
        }

        #endregion

        #region 得到给定集合的并集（异步）

        /// <summary>
        /// 得到给定集合的并集（异步）
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>并集成员的列表</returns>
        public Task<T[]> SetUnionAsync<T>(params string[] keys)
        {
            return this._client.SUnionAsync<T>(keys);
        }

        #endregion

        #region 将指定的缓存key集合并到一起且保存到新的集合中并返回结果集中的元素数量（异步）

        /// <summary>
        /// 将指定的缓存key集合并到一起且保存到新的集合中并返回结果集中的元素数量（异步）
        /// </summary>
        /// <param name="optKey">新的缓存key</param>
        /// <param name="keys">指定缓存key集合</param>
        /// <returns>结果集中的元素数量</returns>
        public Task<long> SetUnionStoreAsync(string optKey, params string[] keys)
        {
            return this._client.SUnionStoreAsync(optKey, keys);
        }

        #endregion
    }
}
