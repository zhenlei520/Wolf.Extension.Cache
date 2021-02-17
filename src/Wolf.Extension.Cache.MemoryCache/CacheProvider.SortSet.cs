// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolf.Extension.Cache.MemoryCache.Internal;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// SortSet
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
        public bool SortedSet(string key, string value, double score)
        {
            return this.SortedSet<string>(key, value, score);
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
        public bool SortedSet<T>(string key, T value, double score)
        {
            lock (obj)
            {
                var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
                if (sortSetList == null)
                {
                    sortSetList = new SortedSet<SortSetRequest<T>>(new List<SortSetRequest<T>>(),
                        new SortSetRequestComparer<T>());
                }

                sortSetList.Add(new SortSetRequest<T>()
                {
                    Data = value,
                    Score = score
                });
                return this.Set(key, sortSetList);
            }
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
            return this.SortedSetRemove<string>(key, value);
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
            lock (obj)
            {
                var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
                if (sortSetList == null)
                {
                    return true;
                }

                sortSetList.RemoveWhere(x => x.Data.Equals(value));
                return this.Set(key, sortSetList);
            }
        }

        #endregion

        #region 根据排名查询指定缓存的count数量的值

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <returns></returns>
        public List<string> SortedSetRangeByRank(string key, int count = 1000, bool isDesc = true)
        {
            return this.SortedSetRangeByRank<string>(key, count, isDesc);
        }

        #endregion

        #region 根据排名查询指定缓存的count数量的值

        /// <summary>
        /// 根据排名查询指定缓存的count数量的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="count">数量</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key, int count = 1000, bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return new List<T>();
            }

            if (isDesc)
            {
                return sortSetList.OrderByDescending(x => x.Score).Take(count).Select(x => x.Data).ToList();
            }

            return sortSetList.OrderBy(x => x.Score).Take(count).Select(x => x.Data).ToList();
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
        public List<string> SortedSetRangeFrom(string key, int fromRank, int toRank, bool isDesc = true)
        {
            return this.SortedSetRangeFrom<string>(key, fromRank, toRank, isDesc);
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
        public List<T> SortedSetRangeFrom<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return new List<T>();
            }

            var count = toRank - fromRank + 1;
            if (isDesc)
            {
                return sortSetList.OrderByDescending(x => x.Score).Take(fromRank).Take(count)
                    .Select(x => x.Data).ToList();
            }

            return sortSetList.OrderBy(x => x.Score).Take(count).Select(x => x.Data).ToList();
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
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return false;
            }

            return sortSetList.Any(x => x.Data.Equals(value));
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
            var sortSetList = this.Get<SortedSet<SortSetRequest<object>>>(key);
            return sortSetList?.Count ?? 0;
        }

        #endregion
    }
}
