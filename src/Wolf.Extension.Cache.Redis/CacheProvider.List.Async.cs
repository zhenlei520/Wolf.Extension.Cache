// Copyright (c) zhenlei520 All rights reserved.

using System.Threading.Tasks;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// List存储（异步）
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
            return this._client.LPushAsync(key, value);
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
            return this._client.RPushAsync(key, value);
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
            return this._client.RPopAsync(key);
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
            return this._client.RPopAsync<T>(key);
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
            return this._client.LPushAsync(key, value);
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
            return this._client.LPushAsync(key, value);
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
            return this._client.LPopAsync(key);
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
            return this._client.LPopAsync<T>(key);
        }

        #endregion

        #region 获取指定key的List(异步)

        /// <summary>
        /// 获取指定key的List(异步)
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<string[]> ListLeftRangeAsync(string key, int count = 1000)
        {
            return this._client.LRangeAsync(key, 0, count);
        }

        #endregion

        #region 获取指定key的List(异步)

        /// <summary>
        /// 获取指定key的List(异步)
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<T[]> ListLeftRangeAsync<T>(string key, int count = 1000)
        {
            return this._client.LRangeAsync<T>(key, 0, count);
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<string[]> ListRightRangeAsync(string key, int count = 1000)
        {
            return this._client.LRangeAsync(key, -1, count);
        }

        #endregion

        #region 获取指定key的栈List

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        public Task<T[]> ListRightRangeAsync<T>(string key, int count = 1000)
        {
            return this._client.LRangeAsync<T>(key, -1, count);
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
            return this._client.LRemAsync(key, 0, value);
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
            return this._client.LRemAsync(key,0, value);
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
            return this._client.LLenAsync(key);
        }

        #endregion
    }
}
