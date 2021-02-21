// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// Sort Set
    /// </summary>
    public partial interface ICacheProvider
    {
        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <returns></returns>
        bool SortedSet(string key, string value, double score);

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool SortedSet<T>(string key, T value, double score);

        /// <summary>
        /// 删除指定的缓存键的value
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SortedSetRemove(string key, string value);

        /// <summary>
        /// 删除指定的缓存键的value
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool SortedSetRemove<T>(string key, T value);

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        List<string> SortedSetRangeByRank(string key, int count = 1000, bool isDesc = true);

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> SortedSetRangeByRank<T>(string key, int count = 1000, bool isDesc = true);

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标（包含）</param>
        /// <param name="toRank">终点排名下标（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        List<string> SortedSetRangeFrom(string key, int fromRank, int toRank, bool isDesc = true);

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标（包含）</param>
        /// <param name="toRank">终点排名下标（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> SortedSetRangeFrom<T>(string key, int fromRank, int toRank, bool isDesc = true);

        /// <summary>
        /// 查询指定缓存下的value是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool SortedSetExist<T>(string key, T value);

        /// <summary>
        /// 得到指定缓存的SortSet长度
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        long SortedSetLength(string key);
    }
}
