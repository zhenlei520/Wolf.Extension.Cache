// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// List异步
    /// </summary>
    public partial class CacheProvider
    {
        #region 入队（先进先出）(异步)

        /// <summary>
        /// 入队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        public Task<long> ListRightPushAsync(string key, string value)
        {
            return Task.FromResult(this.ListRightPush(key, value));
        }

        #endregion

        #region 入队（先进先出）(异步)

        /// <summary>
        /// 入队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public Task<long> ListRightPushAsync<T>(string key, T value)
        {
            return Task.FromResult(this.ListRightPush(key, value));
        }

        #endregion

        #region 出队（先进先出）(异步)

        /// <summary>
        /// 出队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回队列中总数</returns>
        public Task<string> ListRightPopAsync(string key)
        {
            return Task.FromResult(this.ListRightPop(key));
        }

        #endregion

        #region 出队（先进先出）(异步)

        /// <summary>
        /// 出队（先进先出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<T> ListRightPopAsync<T>(string key)
        {
            return Task.FromResult(this.ListRightPop<T>(key));
        }

        #endregion

        #region 入栈（先进后出）(异步)

        /// <summary>
        /// 入栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        public Task<long> ListLeftPushAsync(string key, string value)
        {
            return Task.FromResult(this.ListLeftPush(key, value));
        }

        #endregion

        #region 入栈（先进后出）(异步)

        /// <summary>
        /// 入栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            return Task.FromResult<long>(this.ListLeftPush(key, value));
        }

        #endregion

        #region 出栈（先进后出）(异步)

        /// <summary>
        /// 出栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回队列中总数</returns>
        public Task<string> ListLeftPopAsync(string key)
        {
            return Task.FromResult(this.ListLeftPop(key));
        }

        #endregion

        #region 出栈（先进后出）(异步)

        /// <summary>
        /// 出栈（先进后出）(异步)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<T> ListLeftPopAsync<T>(string key)
        {
            return Task.FromResult<T>(this.ListLeftPop<T>(key));
        }

        #endregion

        #region 获取指定key的List(异步)

        /// <summary>
        /// 获取指定key的List(异步)
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<List<string>> ListLeftRangeAsync(string key, int count = 1000)
        {
            return Task.FromResult(this.ListLeftRange(key, count));
        }

        #endregion

        #region 获取指定key的List(异步)

        /// <summary>
        /// 获取指定key的List(异步)
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<List<T>> ListLeftRangeAsync<T>(string key, int count = 1000)
        {
            return Task.FromResult(this.ListLeftRange<T>(key, count));
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<List<string>> ListRightRangeAsync(string key, int count = 1000)
        {
            return Task.FromResult<List<string>>(this.ListRightRange(key, count));
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<List<T>> ListRightRangeAsync<T>(string key, int count = 1000)
        {
            return Task.FromResult<List<T>>(this.ListRightRange<T>(key, count));
        }

        #endregion

        #region 根据缓存键以及对应的list的值删除(异步)

        /// <summary>
        /// 根据缓存键以及对应的list的值删除(异步)
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <returns>返回队列中总数</returns>
        public Task<long> ListRemoveAsync(string key, string value)
        {
            return Task.FromResult<long>(this.ListRemove(key, value));
        }

        #endregion

        #region 根据缓存键以及对应的list的值删除(异步)

        /// <summary>
        /// 根据缓存键以及对应的list的值删除(异步)
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public Task<long> ListRemoveAsync<T>(string key, T value)
        {
            return Task.FromResult<long>(this.ListRemove(key, value));
        }

        #endregion

        #region 获取集合中的数量(异步)

        /// <summary>
        /// 获取集合中的数量(异步)
        /// </summary>
        /// <param name="key">缓存key的值</param>
        /// <returns></returns>
        public Task<long> ListLengthAsync(string key)
        {
            return Task.FromResult<long>(this.ListLength(key));
        }

        #endregion
    }
}
