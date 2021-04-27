// Copyright (c) zhenlei520 All rights reserved.

using System.Linq;
using Wolf.Extension.Cache.Abstractions.Request.SortedSet;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// Sort Set
    /// </summary>
    public partial class CacheProvider
    {
        #region 设置SortSet类型的缓存键值对

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <returns></returns>
        public bool SortedSet(string key, string value, decimal score)
        {
            return this._client.ZAdd(key, (score, value)) > 0;
        }

        #endregion

        #region 设置SortSet类型的缓存键值对

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <returns></returns>
        public bool SortedSet(string key, params SortedSetRequest<string>[] request)
        {
            var param = request.Select(x => (x.Score, x.Data as object)).ToArray();
            return this._client.ZAdd(key, param) > 0;
        }

        #endregion

        #region 设置SortSet类型的缓存键值对

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SortedSet<T>(string key, T value, decimal score)
        {
            return this._client.ZAdd(key, (score, value)) > 0;
        }

        #endregion

        #region 设置SortSet类型的缓存键值对

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">分值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SortedSet<T>(string key, params SortedSetRequest<T>[] request)
        {
            var param = request.Select(x => (x.Score, x.Data as object)).ToArray();
            return this._client.ZAdd(key, param) > 0;
        }

        #endregion

        #region 删除指定的缓存键的value

        /// <summary>
        /// 删除指定的缓存键的value
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SortedSetRemove(string key, string value)
        {
            return this._client.ZRem(key, value) > 0;
        }

        #endregion

        #region 删除指定的缓存键的value

        /// <summary>
        /// 删除指定的缓存键的value
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T value)
        {
            return this._client.ZRem(key, value) > 0;
        }

        #endregion

        #region 移除有序集合中给定的分数区间的所有成员

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="toRank">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public bool SortedSetRemoveByRank(string key, int fromRank, int toRank)
        {
            return this._client.ZRemRangeByRank(key, fromRank, toRank) > 0;
        }

        #endregion

        #region 移除有序集合中给定的分数区间的所有成员

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <returns></returns>
        public bool SortedSetRemoveByScore(string key, decimal min, decimal max)
        {
            return this._client.ZRemRangeByScore(key, min, max) > 0;
        }

        #endregion

        #region 根据排名查询指定缓存的count数量的值

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public string[] SortedSetRangeByRank(string key, int count = 1000, bool isDesc = true)
        {
            if (isDesc)
            {
                return this._client.ZRevRange(key, -1, count);
            }

            return this._client.ZRange(key, 0, count);
        }

        #endregion

        #region 根据排名查询指定缓存的count数量的值

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] SortedSetRangeByRank<T>(string key, int count = 1000, bool isDesc = true)
        {
            if (isDesc)
            {
                return this._client.ZRevRange<T>(key, -1, count);
            }

            return this._client.ZRange<T>(key, 0, count);
        }

        #endregion

        #region 根据缓存键获取从起始排名到终点排名的数据

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据（根据下标）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="toRank">终点排名下标，0表示第一个元素，-1表示最后一个元素（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public string[] SortedSetRangeFrom(string key, int fromRank, int toRank, bool isDesc = true)
        {
            if (isDesc)
            {
                return this._client.ZRevRange(key, fromRank, toRank);
            }

            return this._client.ZRange(key, fromRank, toRank);
        }

        #endregion

        #region 根据缓存键获取从起始排名到终点排名的数据

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标（包含）</param>
        /// <param name="toRank">终点排名下标（包含）</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] SortedSetRangeFrom<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            if (isDesc)
            {
                return this._client.ZRevRange<T>(key, fromRank, toRank);
            }

            return this._client.ZRange<T>(key, fromRank, toRank);
        }

        #endregion

        #region 查询指定缓存下的value是否存在

        /// <summary>
        /// 查询指定缓存下的value是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SortedSetExist<T>(string key, T value)
        {
            return this._client.ZScore(key, value).HasValue;
        }

        #endregion

        #region 得到指定缓存的SortSet长度

        /// <summary>
        /// 得到指定缓存的SortSet长度
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            return this._client.ZCard(key);
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
        public long SortedSetLength(string key, decimal min, decimal max)
        {
            return this._client.ZCount(key, min, max);
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
        public decimal SortedSetIncrement(string key, string value, long val = 1)
        {
            return this._client.ZIncrBy(key, value, val);
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
        public decimal SortedSetIncrement<T>(string key, T value, long val = 1)
        {
            return this._client.ZIncrBy(key, value, val);
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
        public decimal SortedSetDecrement(string key, string value, long val = 1)
        {
            return this._client.ZIncrBy(key, value, -1 * val);
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
        public decimal SortedSetDecrement<T>(string key, T value, long val = 1)
        {
            return this._client.ZIncrBy(key, value, -1 * val);
        }

        #endregion
    }
}
