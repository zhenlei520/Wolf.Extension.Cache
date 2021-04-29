// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions.Request.SortedSet;
using Wolf.Extension.Cache.Abstractions.Response.SortedSet;

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

        #region 设置SortSet类型的缓存键值对

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAsync(string key, params SortedSetRequest<string>[] request)
        {
            var param = request.Select(x => (x.Score, x.Data as object)).ToArray();
            return await this._client.ZAddAsync(key, param) > 0;
        }

        #endregion

        #region 设置SortSet类型的缓存键值对

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="request"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> SortedSetAsync<T>(string key, params SortedSetRequest<T>[] request)
        {
            var param = request.Select(x => (x.Score, x.Data as object)).ToArray();
            return await this._client.ZAddAsync(key, param) > 0;
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

        #region 移除有序集合中给定的分数区间的所有成员（异步）

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="toRank">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveByRankAsync(string key, int fromRank, int toRank)
        {
            return await this._client.ZRemRangeByRankAsync(key, fromRank, toRank) > 0;
        }

        #endregion

        #region 移除有序集合中给定的分数区间的所有成员（异步）

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveByScoreAsync(string key, decimal min, decimal max)
        {
            return await this._client.ZRemRangeByScoreAsync(key, min, max) > 0;
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

        #region 根据缓存键获取从起始排名到终点排名的数据以及分值（根据下标）

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据以及分值（根据下标）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="toRank">终点排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public async Task<List<SortedSetResponse<string>>> SortedSetRangeWithScoresFromAsync(string key, int fromRank,
            int toRank,
            bool isDesc = true)
        {
            if (isDesc)
            {
                var ret = await this._client.ZRevRangeWithScoresAsync(key, fromRank, toRank);
                return ret.Select(x => new SortedSetResponse<string>(x.score, x.member)).ToList();
            }
            else
            {
                var ret = await this._client.ZRevRangeWithScoresAsync(key, fromRank, toRank);
                return ret.Select(x => new SortedSetResponse<string>(x.score, x.member)).ToList();
            }
        }

        #endregion

        #region 根据缓存键获取从起始排名到终点排名的数据以及分值（根据下标）

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据以及分值（根据下标）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="toRank">终点排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public async Task<List<SortedSetResponse<T>>> SortedSetRangeWithScoresFromAsync<T>(string key, int fromRank,
            int toRank,
            bool isDesc = true)
        {
            if (isDesc)
            {
                var ret = await this._client.ZRevRangeWithScoresAsync<T>(key, fromRank, toRank);
                return ret.Select(x => new SortedSetResponse<T>(x.score, x.member)).ToList();
            }
            else
            {
                var ret = await this._client.ZRevRangeWithScoresAsync<T>(key, fromRank, toRank);
                return ret.Select(x => new SortedSetResponse<T>(x.score, x.member)).ToList();
            }
        }

        #endregion

        #region 根据缓存key以及最小分值以及最大分值得到区间的成员（根据分值）

        /// <summary>
        /// 根据缓存key以及最小分值以及最大分值得到区间的成员（根据分值）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="skip">跳过多少条</param>
        /// <param name="count">查询多少条，默认-1 查询全部</param>
        /// <param name="isDesc">是否按分值降序，默认降序</param>
        /// <returns></returns>
        public Task<string[]> SortedSetRangeByScoreAsync(string key, decimal min, decimal max, int skip = 0,
            int count = -1,
            bool isDesc = true)
        {
            if (count < -1 || count == 0)
            {
                throw new Exception("count is negative 1 or greater than 0");
            }

            if (skip < 0)
            {
                throw new Exception("skip is greater than or equal to 0");
            }

            if (isDesc)
            {
                return this._client.ZRevRangeByScoreAsync(key, min, max, count, skip);
            }

            return this._client.ZRangeByScoreAsync(key, min, max, count, skip);
        }

        #endregion

        #region 根据缓存key以及最小分值以及最大分值得到区间的成员（根据分值）

        /// <summary>
        /// 根据缓存key以及最小分值以及最大分值得到区间的成员（根据分值）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="skip">跳过多少条</param>
        /// <param name="count">查询多少条，默认-1 查询全部</param>
        /// <param name="isDesc">是否按分值降序，默认降序</param>
        /// <returns></returns>
        public Task<T[]> SortedSetRangeByScoreAsync<T>(string key, decimal min, decimal max, int skip = 0,
            int count = -1,
            bool isDesc = true)
        {
            if (count < -1 || count == 0)
            {
                throw new Exception("count is negative 1 or greater than 0");
            }

            if (skip < 0)
            {
                throw new Exception("skip is greater than or equal to 0");
            }

            if (isDesc)
            {
                return this._client.ZRevRangeByScoreAsync<T>(key, min, max, count, skip);
            }

            return this._client.ZRangeByScoreAsync<T>(key, min, max, count, skip);
        }

        #endregion

        #region 根据缓存key以及最小分值以及最大分值得到区间的成员（根据分值）

        /// <summary>
        /// 根据缓存key以及最小分值以及最大分值得到区间的成员以及分值（根据分值）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="skip">跳过多少条</param>
        /// <param name="count">查询多少条，默认-1 查询全部</param>
        /// <param name="isDesc">是否按分值降序，默认降序</param>
        /// <returns></returns>
        public async Task<List<SortedSetResponse<string>>> SortedSetRangeByScoreWithScoresAsync(string key, decimal min,
            decimal max,
            int skip = 0, int count = -1,
            bool isDesc = true)
        {
            if (count < -1 || count == 0)
            {
                throw new Exception("count is negative 1 or greater than 0");
            }

            if (skip < 0)
            {
                throw new Exception("skip is greater than or equal to 0");
            }

            if (isDesc)
            {
                return (await this._client.ZRevRangeByScoreWithScoresAsync(key, min, max, count, skip))
                    .Select(x => new SortedSetResponse<string>(x.score, x.member)).ToList();
            }

            return (await this._client.ZRangeByScoreWithScoresAsync(key, min, max, count, skip))
                .Select(x => new SortedSetResponse<string>(x.score, x.member)).ToList();
        }

        #endregion

        #region 根据缓存key以及最小分值以及最大分值得到区间的成员（根据分值）

        /// <summary>
        /// 根据缓存key以及最小分值以及最大分值得到区间的成员（根据分值）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="skip">跳过多少条</param>
        /// <param name="count">查询多少条，默认-1 查询全部</param>
        /// <param name="isDesc">是否按分值降序，默认降序</param>
        /// <returns></returns>
        public async Task<List<SortedSetResponse<T>>> SortedSetRangeByScoreWithScoresAsync<T>(string key, decimal min,
            decimal max,
            int skip = 0, int count = -1,
            bool isDesc = true)
        {
            if (count < -1 || count == 0)
            {
                throw new Exception("count is negative 1 or greater than 0");
            }

            if (skip < 0)
            {
                throw new Exception("skip is greater than or equal to 0");
            }

            if (isDesc)
            {
                return (await this._client.ZRevRangeByScoreWithScoresAsync<T>(key, min, max, count, skip))
                    .Select(x => new SortedSetResponse<T>(x.score, x.member)).ToList();
            }

            return (await this._client.ZRangeByScoreWithScoresAsync<T>(key, min, max, count, skip))
                .Select(x => new SortedSetResponse<T>(x.score, x.member)).ToList();
        }

        #endregion

        #region 返回有序集合中指定成员的索引

        /// <summary>
        /// 返回有序集合中指定成员的索引
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public Task<long?> SortedSetIndexAsync(string key, string value, bool isDesc = true)
        {
            if (isDesc)
            {
                return this._client.ZRevRankAsync(key, value);
            }

            return this._client.ZRankAsync(key, value);
        }

        #endregion

        #region 返回有序集合中指定成员的索引

        /// <summary>
        /// 返回有序集合中指定成员的索引
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public Task<long?> SortedSetIndexAsync<T>(string key, T value, bool isDesc = true)
        {
            if (isDesc)
            {
                return this._client.ZRevRankAsync(key, value);
            }

            return this._client.ZRankAsync(key, value);
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

        #region 返回有序集KEY中，score值在min和max之间(默认包括score值等于min或max)的成员的数量

        /// <summary>
        /// 返回有序集KEY中，score值在min和max之间(默认包括score值等于min或max)的成员的数量
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="min">score的最小值（包含）</param>
        /// <param name="max">score的最大值（包含）</param>
        /// <returns></returns>
        public Task<long> SortedSetLengthAsync(string key, decimal min, decimal max)
        {
            return this._client.ZCountAsync(key, min, max);
        }

        #endregion

        #region 有序集合增长val

        /// <summary>
        /// 有序集合增长val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        public Task<decimal> SortedSetIncrementAsync(string key, string value, long val = 1)
        {
            return this._client.ZIncrByAsync(key, value, val);
        }

        #endregion

        #region 有序集合增长val

        /// <summary>
        /// 有序集合增长val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        public Task<decimal> SortedSetIncrementAsync<T>(string key, T value, long val = 1)
        {
            return this._client.ZIncrByAsync(key, value, val);
        }

        #endregion

        #region 有序集合减少val

        /// <summary>
        /// 有序集合减少val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        public Task<decimal> SortedSetDecrementAsync(string key, string value, long val = 1)
        {
            return this._client.ZIncrByAsync(key, value, -1 * val);
        }

        #endregion

        #region 有序集合减少val

        /// <summary>
        /// 有序集合减少val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        public Task<decimal> SortedSetDecrementAsync<T>(string key, T value, long val = 1)
        {
            return this._client.ZIncrByAsync(key, value, -1 * val);
        }

        #endregion
    }
}
