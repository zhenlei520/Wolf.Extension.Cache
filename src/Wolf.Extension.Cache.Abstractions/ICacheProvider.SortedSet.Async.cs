// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions.Request.SortedSet;
using Wolf.Extension.Cache.Abstractions.Response.SortedSet;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// Sort Set 异步
    /// </summary>
    public partial interface ICacheProvider
    {
        /// <summary>
        /// 设置SortSet类型的缓存键值对（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <returns></returns>
        Task<bool> SortedSetAsync(string key, string value, decimal score);

        /// <summary>
        /// 设置SortSet类型的缓存键值对（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SortedSetAsync(string key, params SortedSetRequest<string>[] request);

        /// <summary>
        /// 设置SortSet类型的缓存键值对（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SortedSetAsync<T>(string key, T value, decimal score);

        /// <summary>
        /// 设置SortSet类型的缓存键值对（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="request"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SortedSetAsync<T>(string key, params SortedSetRequest<T>[] request);

        /// <summary>
        /// 删除指定的缓存键的value（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> SortedSetRemoveAsync(string key, string value);

        /// <summary>
        /// 删除指定的缓存键的value（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SortedSetRemoveAsync<T>(string key, T value);

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="toRank">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<bool> SortedSetRemoveByRankAsync(string key, int fromRank, int toRank);

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <returns></returns>
        Task<bool> SortedSetRemoveByScoreAsync(string key, decimal min, decimal max);

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        Task<string[]> SortedSetRangeByRankAsync(string key, int count = 1000, bool isDesc = true);

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T[]> SortedSetRangeByRankAsync<T>(string key, int count = 1000, bool isDesc = true);

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="toRank">终点排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        Task<string[]> SortedSetRangeFromAsync(string key, int fromRank, int toRank, bool isDesc = true);

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="toRank">终点排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T[]> SortedSetRangeFromAsync<T>(string key, int fromRank, int toRank, bool isDesc = true);

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据以及分值（根据下标）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="toRank">终点排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        Task<List<SortedSetResponse<string>>> SortedSetRangeWithScoresFromAsync(string key, int fromRank, int toRank,
            bool isDesc = true);

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据以及分值（根据下标）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="toRank">终点排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        Task<List<SortedSetResponse<T>>> SortedSetRangeWithScoresFromAsync<T>(string key, int fromRank, int toRank,
            bool isDesc = true);

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
        Task<string[]> SortedSetRangeByScoreAsync(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true);

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
        Task<T[]> SortedSetRangeByScoreAsync<T>(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true);

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
        Task<List<SortedSetResponse<string>>> SortedSetRangeByScoreWithScoresAsync(string key, decimal min, decimal max,
            int skip = 0, int count = -1,
            bool isDesc = true);

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
        Task<List<SortedSetResponse<T>>> SortedSetRangeByScoreWithScoresAsync<T>(string key, decimal min, decimal max,
            int skip = 0, int count = -1,
            bool isDesc = true);

        /// <summary>
        /// 返回有序集合中指定成员的索引（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        Task<long?> SortedSetIndexAsync(string key, string value, bool isDesc = true);

        /// <summary>
        /// 返回有序集合中指定成员的索引（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<long?> SortedSetIndexAsync<T>(string key, T value, bool isDesc = true);

        /// <summary>
        /// 查询指定缓存下的value是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> SortedSetExistAsync(string key, string value);

        /// <summary>
        /// 查询指定缓存下的value是否存在（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SortedSetExistAsync<T>(string key, T value);

        /// <summary>
        /// 得到指定缓存的SortSet长度（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string key);

        /// <summary>
        /// 返回有序集KEY中，score值在min和max之间(默认包括score值等于min或max)的成员的数量（异步）
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="min">score的最小值（包含）</param>
        /// <param name="max">score的最大值（包含）</param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string key, decimal min, decimal max);

        /// <summary>
        /// 有序集合增长val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        Task<decimal> SortedSetIncrementAsync(string key, string value, long val = 1);

        /// <summary>
        /// 有序集合增长val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        Task<decimal> SortedSetIncrementAsync<T>(string key, T value, long val = 1);

        /// <summary>
        /// 有序集合减少val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        Task<decimal> SortedSetDecrementAsync(string key, string value, long val = 1);

        /// <summary>
        /// 有序集合减少val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        Task<decimal> SortedSetDecrementAsync<T>(string key, T value, long val = 1);
    }
}
