// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// Sort Set（异步）
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
        public async Task<bool> SortedSetAsync(string key, string value, decimal score)
        {
            return await this._client.ZAddAsync(key, (score, value)) > 0;
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
        public async Task<bool> SortedSetAsync<T>(string key, T value, decimal score)
        {
            return await this._client.ZAddAsync(key, (score, value)) > 0;
        }

        #endregion

        #region 删除指定的缓存键的value（异步）

        /// <summary>
        /// 删除指定的缓存键的value（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync(string key, string value)
        {
            return await this._client.ZRemAsync(key, value) > 0;
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
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            return await this._client.ZRemAsync(key, value) > 0;
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
            if (isDesc)
            {
                return this._client.ZRevRangeAsync(key, -1, count);
            }

            return this._client.ZRangeAsync(key, 0, count);
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
            if (isDesc)
            {
                return this._client.ZRevRangeAsync<T>(key, -1, count);
            }

            return this._client.ZRangeAsync<T>(key, 0, count);
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
            if (isDesc)
            {
                return this._client.ZRevRangeAsync(key, fromRank, toRank);
            }

            return this._client.ZRangeAsync(key, fromRank, toRank);
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
            if (isDesc)
            {
                return this._client.ZRevRangeAsync<T>(key, fromRank, toRank);
            }

            return this._client.ZRangeAsync<T>(key, fromRank, toRank);
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
        public async Task<bool> SortedSetExistAsync<T>(string key, T value)
        {
            return (await this._client.ZScoreAsync(key, value)).HasValue;
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
            return this._client.ZCardAsync(key);
        }

        #endregion
    }
}
