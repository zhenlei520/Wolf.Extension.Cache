// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// List
    /// </summary>
    public partial class CacheProvider
    {
        #region 入队（先进先出）

        /// <summary>
        /// 入队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        public long ListRightPush(string key, string value)
        {
            return this.ListRightPush<string>(key, value);
        }

        #endregion

        #region 入队（先进先出）

        /// <summary>
        /// 入队（先进先出）
        /// 0代表入队未成功
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public long ListRightPush<T>(string key, T value)
        {
            lock (obj)
            {
                var list = this.Get<List<T>>(key);
                if (list == null)
                {
                    list = new List<T>();
                }

                list.Insert(0, value);
                if (this.Set(key, list))
                {
                    return list.Count();
                }

                return 0;
            }
        }

        #endregion

        #region 出队（先进先出）

        /// <summary>
        /// 出队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public string ListRightPop(string key)
        {
            return this.ListRightPop<string>(key);
        }

        #endregion

        #region 出队（先进先出）

        /// <summary>
        /// 出队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            lock (obj)
            {
                var list = this.Get<List<T>>(key);
                T obj = default(T);
                if (list != null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                }

                if (this.Set(key, list))
                {
                    return obj;
                }

                return default(T);
            }
        }

        #endregion

        #region 入栈（先进后出）

        /// <summary>
        /// 入栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        public long ListLeftPush(string key, string value)
        {
            return this.ListLeftPush<string>(key, value);
        }

        #endregion

        #region 入栈（先进后出）

        /// <summary>
        /// 入栈（先进后出）
        /// 0代表入栈未成功
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public long ListLeftPush<T>(string key, T value)
        {
            lock (obj)
            {
                var list = this.Get<List<T>>(key) ?? new List<T>();
                list.Insert(list.Count, value);

                if (this.Set(key, list))
                {
                    return list.Count();
                }

                return 0;
            }
        }

        #endregion

        #region 出栈（先进后出）

        /// <summary>
        /// 出栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public string ListLeftPop(string key)
        {
            return this.ListLeftPop<string>(key);
        }

        #endregion

        #region 出栈（先进后出）

        /// <summary>
        /// 出栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            lock (obj)
            {
                var list = this.Get<List<T>>(key);
                T obj = default(T);
                if (list != null && list.Count > 0)
                {
                    obj = list[list.Count - 1];
                    list.RemoveAt(0);
                }

                if (this.Set(key, list))
                {
                    return obj;
                }

                return default(T);
            }
        }

        #endregion

        #region 获取指定key的List

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public List<string> ListLeftRange(string key, int count = 1000)
        {
            return this.ListLeftRange<string>(key, count);
        }

        #endregion

        #region 获取指定key的List

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public List<T> ListLeftRange<T>(string key, int count = 1000)
        {
            var allList = this.Get<List<T>>(key);
            return allList.Take(count).ToList();
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public List<string> ListRightRange(string key, int count = 1000)
        {
            return this.ListRightRange<string>(key, count);
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public List<T> ListRightRange<T>(string key, int count = 1000)
        {
            var allList = this.Get<List<T>>(key);
            allList.Reverse();
            return allList.Take(count).ToList();
        }

        #endregion

        #region 根据缓存键以及对应的list的值删除

        /// <summary>
        /// 根据缓存键以及对应的list的值删除
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <returns>返回队列中总数</returns>
        public long ListRemove(string key, string value)
        {
            return this.ListRemove<string>(key, value);
        }

        #endregion

        #region 根据缓存键以及对应的list的值删除

        /// <summary>
        /// 根据缓存键以及对应的list的值删除
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public long ListRemove<T>(string key, T value)
        {
            lock (obj)
            {
                var list = this.Get<List<T>>(key);
                var count = 0;
                if (list != null)
                {
                    list.RemoveAll(x => x.Equals(value));
                    count = list.Count;
                    this.Set(key, list);
                }

                return count;
            }
        }

        #endregion

        #region 获取集合中的数量

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key">缓存key的值</param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            var allList = this.Get<List<object>>(key);
            return allList?.Count ?? 0;
        }

        #endregion
    }
}
