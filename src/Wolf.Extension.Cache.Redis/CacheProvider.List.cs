// Copyright (c) zhenlei520 All rights reserved.

namespace Wolf.Extension.Cache.Redis
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
            return this._quickHelperBase.RPush(key, new string[1] {value});
        }

        #endregion

        #region 入队（先进先出）

        /// <summary>
        /// 入队（先进先出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        public long ListRightPush<T>(string key, T value)
        {
            return this._quickHelperBase.RPush(key, new string[1] {ConvertJson(value)});
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
            return this._quickHelperBase.RPop(key);
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
            return ConvertObj<T>(this._quickHelperBase.RPop(key));
        }

        #endregion

        /// <summary>
        /// 入栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <returns>返回队列中总数</returns>
        long ListLeftPush(string key, string value);

        /// <summary>
        /// 入栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        long ListLeftPush<T>(string key, T value);

        /// <summary>
        /// 出栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        string ListLeftPop(string key);

        /// <summary>
        /// 出栈（先进后出）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        T ListLeftPop<T>(string key);

        /// <summary>
        /// 获取指定key的队列List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        List<string> ListLeftRange(string key, int count = 1000);

        /// <summary>
        /// 获取指定key的队列List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        List<T> ListLeftRange<T>(string key, int count = 1000);

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        List<string> ListRightRange(string key, int count = 1000);

        /// <summary>
        /// 获取指定key的栈List
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="count">获取多少的列表，默认获取1000条，不限制为-1</param>
        /// <returns></returns>
        List<T> ListRightRange<T>(string key, int count = 1000);

        /// <summary>
        /// 根据缓存键以及对应的list的值删除
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <returns>返回队列中总数</returns>
        long ListRemove(string key, string value);

        /// <summary>
        /// 根据缓存键以及对应的list的值删除
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">指定list列表的值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回队列中总数</returns>
        long ListRemove<T>(string key, T value);

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key">缓存key的值</param>
        /// <returns></returns>
        long ListLength(string key);
    }
}
