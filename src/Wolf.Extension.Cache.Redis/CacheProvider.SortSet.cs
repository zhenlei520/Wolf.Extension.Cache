// Copyright (c) zhenlei520 All rights reserved.

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
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SortedSet<T>(string key, T value, decimal score)
        {
            return this._client.ZAdd(key, (score, value)) > 0;
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
        /// 根据缓存键获取从起始排名到终点排名的数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">起始排名下标（包含）</param>
        /// <param name="toRank">终点排名下标（包含）</param>
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
    }
}
