// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions.Request.SortedSet;
using Wolf.Extension.Cache.Abstractions.Response.SortedSet;
using Wolf.Extension.Cache.MemoryCache.Internal;
using Wolf.Systems.Core;

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
        public bool SortedSet(string key, string value, decimal score)
        {
            return this.SortedSet<string>(key, value, score);
        }

        #endregion

        #region 设置SortSet类型的缓存键值对

        /// <summary>
        /// 设置SortSet类型的缓存键值对
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SortedSet(string key, params SortedSetRequest<string>[] request)
        {
            return this.SortedSet<string>(key, request);
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
            return this.SortedSet(key, new SortedSetRequest<T>(score, value));
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
        public bool SortedSet<T>(string key, params SortedSetRequest<T>[] request)
        {
            lock (_obj)
            {
                var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
                if (sortSetList == null)
                {
                    sortSetList = new SortedSet<SortSetRequest<T>>(new List<SortSetRequest<T>>(),
                        new SortSetRequestComparer<T>());
                }

                request.ForEach(item =>
                {
                    var info = sortSetList.FirstOrDefault(x => x.Data.Equals(item.Data));
                    if (info == null)
                    {
                        info = new SortSetRequest<T>()
                        {
                            Data = item.Data,
                            Score = item.Score
                        };
                        sortSetList.Add(info);
                    }
                    else
                    {
                        info.Score = item.Score;
                    }
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
            lock (_obj)
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
            lock (_obj)
            {
                var sortSetList = this.Get<SortedSet<SortSetRequest<object>>>(key);
                if (sortSetList == null)
                {
                    return true;
                }

                //正序
                var list = sortSetList.ToList();
                if (fromRank >= 0)
                {
                    var lastIndex = toRank == -1 ? sortSetList.Count - 1 : toRank;
                    // ReSharper disable once InvokeAsExtensionMethod
                    Systems.Core.Extensions.ChangeResult(ref fromRank, ref lastIndex);
                    if (lastIndex > sortSetList.Count - 1)
                    {
                        lastIndex = sortSetList.Count - 1;
                    }

                    var delList = list.Skip(fromRank).Take(lastIndex - fromRank + 1);
                    sortSetList.RemoveWhere(x => delList.All(y => y.Data.Equals(x.Data)));
                }
                else if (fromRank == -1)
                {
                    int lastIndex = sortSetList.Count() - 1;
                    if (toRank > lastIndex)
                    {
                        return false;
                    }

                    var delList = list.Skip(toRank).Take(lastIndex - toRank + 1);
                    sortSetList.RemoveWhere(x => delList.All(y => y.Data.Equals(x.Data)));
                }
                else
                {
                    return false;
                }

                return this.Set(key, sortSetList);
            }
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
            lock (_obj)
            {
                var sortSetList = this.Get<SortedSet<SortSetRequest<object>>>(key);
                if (sortSetList == null)
                {
                    return true;
                }

                sortSetList.RemoveWhere(x => x.Score >= min && x.Score <= max);
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
        public string[] SortedSetRangeByRank(string key, int count = 1000, bool isDesc = true)
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
        public T[] SortedSetRangeByRank<T>(string key, int count = 1000, bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return new List<T>().ToArray();
            }

            if (isDesc)
            {
                return sortSetList.OrderByDescending(x => x.Score).Take(count).Select(x => x.Data).ToArray();
            }

            return sortSetList.OrderBy(x => x.Score).Take(count).Select(x => x.Data).ToArray();
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
            return this.SortedSetRangeFrom<string>(key, fromRank, toRank, isDesc);
        }

        #endregion

        #region 根据缓存键获取从起始排名到终点排名的数据

        /// <summary>
        /// 根据缓存键获取从起始排名到终点排名的数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="fromRank">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="toRank">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="isDesc">是否降序，默认降序</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] SortedSetRangeFrom<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return new List<T>().ToArray();
            }

            //正序
            var list = sortSetList.ToList();
            if (fromRank >= 0)
            {
                var lastIndex = toRank == -1 ? sortSetList.Count - 1 : toRank;
                // ReSharper disable once InvokeAsExtensionMethod
                Systems.Core.Extensions.ChangeResult(ref fromRank, ref lastIndex);
                if (lastIndex > sortSetList.Count - 1)
                {
                    lastIndex = sortSetList.Count - 1;
                }

                var newList = list.Skip(fromRank).Take(lastIndex - fromRank + 1).Select(x => x.Data).ToArray();
                if (fromRank > toRank)
                {
                    //倒序
                    newList.Reverse();
                }

                return newList;
            }

            if (fromRank == -1)
            {
                int lastIndex = sortSetList.Count() - 1;
                if (toRank > lastIndex)
                {
                    return new T[0];
                }

                var newList = list.Skip(toRank).Take(lastIndex - toRank + 1).Select(x => x.Data).ToArray();
                newList.Reverse();
                return newList;
            }

            return new T[0];
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
        public List<SortedSetResponse<string>> SortedSetRangeWithScoresFrom(string key, int fromRank, int toRank,
            bool isDesc = true)
        {
            return this.SortedSetRangeWithScoresFrom<string>(key, fromRank, toRank, isDesc);
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
        public List<SortedSetResponse<T>> SortedSetRangeWithScoresFrom<T>(string key, int fromRank, int toRank,
            bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return new List<SortedSetResponse<T>>();
            }

            var count = toRank - fromRank + 1;
            if (isDesc)
            {
                return sortSetList.OrderByDescending(x => x.Score).Take(fromRank).Take(count)
                    .Select(x => new SortedSetResponse<T>(x.Score, x.Data)).ToList();
            }

            return sortSetList.OrderBy(x => x.Score).Take(count).Select(x => new SortedSetResponse<T>(x.Score, x.Data))
                .ToList();
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
        public string[] SortedSetRangeByScore(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true)
        {
            return this.SortedSetRangeByScore<string>(key, min, max, skip, count, isDesc);
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
        public T[] SortedSetRangeByScore<T>(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return new List<T>().ToArray();
            }

            if (count < -1 || count == 0)
            {
                throw new Exception("count is negative 1 or greater than 0");
            }

            if (skip < 0)
            {
                throw new Exception("skip is greater than or equal to 0");
            }

            var list = sortSetList.ToList();
            if (isDesc)
            {
                list.Reverse();
            }

            return sortSetList.Where(x => x.Score >= min && x.Score <= max).Skip(skip).Take(count)
                .Select(x => x.Data).ToArray();
        }

        #endregion

        #region 根据缓存key以及最小分值以及最大分值得到区间的成员以及分值（根据分值）

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
        public List<SortedSetResponse<string>> SortedSetRangeByScoreWithScores(string key, decimal min, decimal max,
            int skip = 0, int count = -1,
            bool isDesc = true)
        {
            return this.SortedSetRangeByScoreWithScores<string>(key, min, max, skip, count, isDesc);
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
        public List<SortedSetResponse<T>> SortedSetRangeByScoreWithScores<T>(string key, decimal min, decimal max,
            int skip = 0, int count = -1,
            bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return new List<SortedSetResponse<T>>().ToList();
            }

            if (count < -1 || count == 0)
            {
                throw new Exception("count is negative 1 or greater than 0");
            }

            if (skip < 0)
            {
                throw new Exception("skip is greater than or equal to 0");
            }

            var list = sortSetList.ToList();
            if (isDesc)
            {
                list.Reverse();
            }

            return list.Where(x => x.Score >= min && x.Score <= max).Skip(skip).Take(count)
                .Select(x => new SortedSetResponse<T>(x.Score, x.Data)).ToList();
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
        public long? SortedSetIndex(string key, string value, bool isDesc)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<string>>>(key);
            if (sortSetList == null)
            {
                return null;
            }

            var list = sortSetList.ToList();
            if (isDesc)
            {
                list = list.OrderByDescending(x => x.Score).ToList();
            }
            else
            {
                list.OrderBy(x => x.Score).ToList();
            }

            for (var index = 0; index < list.Count; index++)
            {
                if (list[index].Data.Equals(value))
                {
                    return index;
                }
            }

            return null;
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
        public long? SortedSetIndex<T>(string key, T value, bool isDesc = true)
        {
            var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key);
            if (sortSetList == null)
            {
                return null;
            }

            var list = sortSetList.ToList();
            if (isDesc)
            {
                list = list.OrderByDescending(x => x.Score).ToList();
            }
            else
            {
                list.OrderBy(x => x.Score).ToList();
            }

            for (var index = 0; index < list.Count; index++)
            {
                if (list[index].Data.Equals(value))
                {
                    return index;
                }
            }

            return null;
        }

        #endregion

        #region 查询指定缓存下的value是否存在

        /// <summary>
        /// 查询指定缓存下的value是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SortedSetExist(string key, string value)
        {
            return this.SortedSetExist<string>(key, value);
        }

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
            var sortSetList = this.Get<SortedSet<SortSetRequest<object>>>(key);
            return sortSetList.Count(x => x.Score >= min && x.Score <= max);
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
            return SortedSetIncrement<string>(key, value, val);
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
            lock (_obj)
            {
                var sortSetList = this.Get<SortedSet<SortSetRequest<T>>>(key) ?? new SortedSet<SortSetRequest<T>>(
                    new List<SortSetRequest<T>>(),
                    new SortSetRequestComparer<T>());

                var info = sortSetList.FirstOrDefault(x => x.Data.Equals(value));
                if (info == null)
                {
                    info = new SortSetRequest<T>()
                    {
                        Data = value,
                        Score = val
                    };
                    sortSetList.Add(info);
                }
                else
                {
                    info.Score += val;
                }

                this.Set(key, sortSetList);
                return info.Score;
            }
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
            return this.SortedSetDecrement<string>(key, value, val);
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
            return this.SortedSetIncrement(key, value, -1 * val);
        }

        #endregion
    }
}
