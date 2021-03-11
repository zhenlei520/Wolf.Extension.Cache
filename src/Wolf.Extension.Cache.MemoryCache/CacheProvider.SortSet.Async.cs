// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// Sort Set 异步
    /// </summary>
    public partial class CacheProvider
    {
        #region 设置SortSet类型的缓存键值对（异步）

        /// <summary>
        /// 设置SortSet类型的缓存键值对（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <returns></returns>
        public Task<bool> SortedSetAsync(string key, string value, decimal score)
        {
            return Task.FromResult<bool>(SortedSet(key, value, (decimal) score));
        }

        #endregion

        #region 设置SortSet类型的缓存键值对（异步）

        /// <summary>
        /// 设置SortSet类型的缓存键值对（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SortedSetAsync<T>(string key, T value, decimal score)
        {
            return Task.FromResult(SortedSet(key, value, (decimal) score));
        }

        #endregion

        #region 删除指定的缓存键的value（异步）

        /// <summary>
        /// 删除指定的缓存键的value（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public Task<bool> SortedSetRemoveAsync(string key, string value)
        {
            return Task.FromResult<bool>(this.SortedSetRemove(key, value));
        }

        #endregion

        #region 删除指定的缓存键的value（异步）

        /// <summary>
        /// 删除指定的缓存键的value（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            return Task.FromResult(this.SortedSetRemove(key, value));
        }

        #endregion

        #region 根据排名查询指定缓存的count数量的值（异步）

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public Task<string[]> SortedSetRangeByRankAsync(string key, int count = 1000, bool isDesc = true)
        {
            return Task.FromResult(this.SortedSetRangeByRank(key, count, isDesc));
        }

        #endregion

        #region 根据排名查询指定缓存的count数量的值（异步）

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T[]> SortedSetRangeByRankAsync<T>(string key, int count = 1000, bool isDesc = true)
        {
            return Task.FromResult(this.SortedSetRangeByRank<T>(key, count, isDesc));
        }

        #endregion

        #region 根据缓存键获取从起始排名到终点排名的数据（异步）

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标（包含）</param>
        /// <param name="toRank">终点排名下标（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public Task<string[]> SortedSetRangeFromAsync(string key, int fromRank, int toRank, bool isDesc = true)
        {
            return Task.FromResult(this.SortedSetRangeFrom(key, fromRank, toRank, isDesc));
        }

        #endregion

        #region 根据缓存键获取从起始排名到终点排名的数据（异步）

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标（包含）</param>
        /// <param name="toRank">终点排名下标（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T[]> SortedSetRangeFromAsync<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            return Task.FromResult(this.SortedSetRangeFrom<T>(key, fromRank, toRank, isDesc));
        }

        #endregion

        #region 查询指定缓存下的value是否存在（异步）

        /// <summary>
        /// 查询指定缓存下的value是否存在（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SortedSetExistAsync<T>(string key, T value)
        {
            return Task.FromResult(this.SortedSetExist(key, value));
        }

        #endregion

        #region 得到指定缓存的SortSet长度（异步）

        /// <summary>
        /// 得到指定缓存的SortSet长度（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public Task<long> SortedSetLengthAsync(string key)
        {
            return Task.FromResult(this.SortedSetLength(key));
        }

        #endregion
    }
}
