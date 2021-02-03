// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// Sort Set
    /// </summary>
    public partial interface ICacheProvider
    {
         /// <summary>添加 (当score一样value一样时不插入)</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <param name="isOverlap"></param>
        /// <returns></returns>
        bool SortedSetAdd<T>(string key, T value, double score, bool isOverlap = false);

        /// <summary>删除</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SortedSetRemove<T>(string key, T value);

        /// <summary>获取全部</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<string> SortedSetRangeByRank(string key, long count = 1000);

        /// <summary>获取全部</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<T> SortedSetRangeByRank<T>(string key, long count = 1000) where T : class, new();

        /// <summary>获取已过期的hashKey</summary>
        /// <param name="count"></param>
        /// <returns></returns>
        List<(string, string, string, string)> SortedSetRangeByRankAndOverTime(long count = 1000);

        /// <summary>降序获取指定索引的集合</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        List<T> GetRangeFromSortedSetDesc<T>(string key, long fromRank, long toRank) where T : class, new();

        /// <summary>获取指定索引的集合</summary>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        List<string> GetRangeFromSortedSet(string key, long fromRank, long toRank);

        /// <summary>获取指定索引的集合</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        List<T> GetRangeFromSortedSet<T>(string key, long fromRank, long toRank) where T : class, new();

        /// <summary>判断是否存在项</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SortedSetExistItem<T>(string key, T value);

        /// <summary>获取集合中的数量</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long SortedSetLength(string key);

        /// <summary>添加</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<bool> SortedSetAddAsync<T>(string key, T value, double score);

        /// <summary>删除</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> SortedSetRemoveAsync<T>(string key, T value);

        /// <summary>获取全部</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<string>> SortedSetRangeByRankAsync(string key, long count = 1000);

        /// <summary>获取全部</summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<T>> SortedSetRangeByRankAsync<T>(string key, long count = 1000) where T : class, new();

        /// <summary>获取集合中的数量</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string key);


        // /// <summary>查找所有符合给定模式( pattern)的 key</summary>
        // /// <param name="pattern">如：runoob*</param>
        // /// <returns></returns>
        // List<string> Keys(string pattern);
    }
}
