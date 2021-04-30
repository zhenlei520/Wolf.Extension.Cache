// Copyright (c) zhenlei520 All rights reserved.

using System.Linq;
using Wolf.Extension.Cache.Abstractions.Common;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// List存储
    /// </summary>
    public partial class CacheProvider
    {
        #region 入队（先进先出）

        /// <summary>
        /// 入队（先进先出）
        /// 如果仅当list的缓存key不存在时设置会消耗很大的性能，不建议使用
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns>返回队列中总数</returns>
        public long ListRightPush(string key, string value, ListPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();

            if (persistentOps.SetStrategy == SetStrategy.Exist)
            {
                return this._client.LPushX(key, value);
            }

            if (persistentOps.SetStrategy == SetStrategy.NoFind)
            {
                var list = this.ListLeftRange(key, -1);
                if (list.Any(x => x.Equals(value)))
                {
                    return list.Length;
                }
            }

            return this._client.LPush(key, value);
        }

        #endregion

        #region 入队（先进先出）

        /// <summary>
        /// 入队（先进先出）
        /// 如果仅当list的缓存key不存在时设置会消耗很大的性能，不建议使用
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public long ListRightPush<T>(string key, T value, ListPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();

            if (persistentOps.SetStrategy == SetStrategy.Exist)
            {
                return this._client.LPushX(key, value);
            }

            if (persistentOps.SetStrategy == SetStrategy.NoFind)
            {
                var list = this.ListLeftRange<T>(key, -1);
                if (list.Any(x => x.Equals(value)))
                {
                    return list.Length;
                }
            }

            return this._client.RPush(key, value);
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
            return this._client.RPop(key);
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
            return this._client.RPop<T>(key);
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
            return this._client.LPush(key, value);
        }

        #endregion

        #region 入栈（先进后出）

        /// <summary>
        /// 入栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public long ListLeftPush<T>(string key, T value)
        {
            return this._client.LPush(key, value);
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
            return this._client.LPop(key);
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
            return this._client.LPop<T>(key);
        }

        #endregion

        #region 获取指定key的队列List

        /// <summary>
        /// 获取指定key的队列List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public string[] ListLeftRange(string key, int count = 1000)
        {
            return this._client.LRange(key, 0, count);
        }

        #endregion

        #region 获取指定key的队列List

        /// <summary>
        /// 获取指定key的队列List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public T[] ListLeftRange<T>(string key, int count = 1000)
        {
            return this._client.LRange<T>(key, 0, count);
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public string[] ListRightRange(string key, int count = 1000)
        {
            return this._client.LRange(key, -1, count);
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public T[] ListRightRange<T>(string key, int count = 1000)
        {
            return this._client.LRange<T>(key, -1, count);
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
            return this._client.LRem(key, 0, value);
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
            return this._client.LRem(key, 0, value);
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
            return this._client.LLen(key);
        }

        #endregion
    }
}
