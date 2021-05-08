// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Wolf.Systems.Core;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// set 存储
    /// </summary>
    public partial class CacheProvider
    {
        #region 向集合添加一个成员

        /// <summary>
        /// 向集合添加一个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public bool SetAdd(string key, string value)
        {
            return this._client.SAdd(key, value) > 0;
        }

        /// <summary>
        /// 向集合添加一个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SetAdd<T>(string key, T value)
        {
            return this._client.SAdd(key, value) > 0;
        }

        #endregion

        #region 向集合添加一个或多个成员

        /// <summary>
        /// 向集合添加一个或多个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <returns></returns>
        public bool SetAddRange(string key, params string[] values)
        {
            return this._client.SAdd(key, values) > 0;
        }

        /// <summary>
        /// 向集合添加一个或多个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SetAddRange<T>(string key, params T[] values)
        {
            return this._client.SAdd(key, values) > 0;
        }

        #endregion

        #region 得到指定缓存的长度

        /// <summary>
        /// 得到指定缓存的长度
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public long SetLength(string key)
        {
            if (key.IsNullOrWhiteSpace())
            {
                return 0;
            }

            return this._client.SCard(key);
        }

        #endregion

        #region 得到给定缓存key集合的差集

        /// <summary>
        /// 得到给定缓存key集合的差集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        public string[] SetDiff(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return new string[0];
            }

            return this._client.SDiff(keys);
        }

        /// <summary>
        /// 得到给定缓存key集合的差集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        public T[] SetDiff<T>(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return new T[0];
            }

            return this._client.SDiff<T>(keys);
        }

        #endregion

        #region 将指定缓存key集合的差集保存到指定的缓存key中

        /// <summary>
        /// 将指定缓存key集合的差集保存到指定的缓存key中
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public bool SetDiffStore(string destination, params string[] keys)
        {
            return this._client.SDiffStore(destination, keys) > 0;
        }

        #endregion

        #region 得到给定集合的交集

        /// <summary>
        /// 得到给定集合的交集
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public List<string> SetInter(params string[] keys)
        {
            return this._client.SInter(keys).ToList();
        }

        /// <summary>
        /// 得到给定集合的交集
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public List<T> SetInter<T>(params string[] keys)
        {
            return this._client.SInter<T>(keys).ToList();
        }

        #endregion

        #region 将给定集合的交集存储到指定的缓存key中

        /// <summary>
        /// 将给定集合的交集存储到指定的缓存key中
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        public bool SetInterStore(string destination, params string[] keys)
        {
            return this._client.SInterStore(destination, keys) > 0;
        }

        #endregion

        #region 判断指定的缓存key的value是否存在

        /// <summary>
        /// 判断指定的缓存key的value是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public bool SetExists(string key, string value)
        {
            return this._client.SIsMember(key, value);
        }

        /// <summary>
        /// 判断指定的缓存key的value是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SetExists<T>(string key, T value)
        {
            return this._client.SIsMember(key, value);
        }

        #endregion

        #region 根据缓存key得到集合中的所有成员

        /// <summary>
        /// 根据缓存key得到集合中的所有成员
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public List<string> SetGet(string key)
        {
            return this._client.SMembers(key).ToList();
        }

        /// <summary>
        /// 根据缓存key得到集合中的所有成员
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public List<T> SetGet<T>(string key)
        {
            return this._client.SMembers<T>(key).ToList();
        }

        #endregion

        #region 根据缓存key将源集合移动到destination集合

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        public bool SetMove(string key, string optKey, string value)
        {
            return this._client.SMove(key, optKey, value);
        }

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SetMove<T>(string key, string optKey, T value)
        {
            return this._client.SMove(key, optKey, value);
        }

        #endregion

        #region 根据缓存key获取一个随机元素并移除

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        public string SetPop(string key)
        {
            return this._client.SPop(key);
        }

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        public T SetPop<T>(string key)
        {
            return this._client.SPop<T>(key);
        }

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        public string[] SetPop(string key, int count)
        {
            return this._client.SPop(key, count);
        }

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        public T[] SetPop<T>(string key, int count)
        {
            return this._client.SPop<T>(key, count);
        }

        #endregion

        #region 根据缓存key获取随机元素但不移除

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        public string SetRandomGet(string key)
        {
            return this._client.SRandMember(key);
        }

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        public T SetRandomGet<T>(string key)
        {
            return this._client.SRandMember<T>(key);
        }

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        public string[] SetRandomGet(string key, int count)
        {
            return this._client.SRandMembers(key, count);
        }

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        public T[] SetRandomGet<T>(string key, int count)
        {
            return this._client.SRandMembers<T>(key, count);
        }

        #endregion

        #region 根据缓存key移除元素

        #region 根据缓存key以及缓存值移除指定元素

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public long SetRem(string key, string value)
        {
            return this._client.SRem(key, value);
        }

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">多个缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public long SetRem(string key, params string[] values)
        {
            return this._client.SRem(key, values);
        }

        #endregion

        #region 根据缓存key以及缓存值移除指定元素

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public long SetRem<T>(string key, T value)
        {
            return this._client.SRem<T>(key, value);
        }

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        public long SetRem<T>(string key, params T[] values)
        {
            return this._client.SRem<T>(key, values);
        }

        #endregion

        #endregion

        #region 得到给定集合的并集

        /// <summary>
        /// 得到给定集合的并集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns>并集成员的列表</returns>
        public string[] SetUnion(params string[] keys)
        {
            return this._client.SUnion(keys);
        }

        /// <summary>
        /// 得到给定集合的并集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>并集成员的列表</returns>
        public T[] SetUnion<T>(params string[] keys)
        {
            return this._client.SUnion<T>(keys);
        }

        #endregion

        #region 将指定的缓存key集合并到一起且保存到新的集合中

        /// <summary>
        /// 将指定的缓存key集合并到一起且保存到新的集合中并返回结果集中的元素数量
        /// </summary>
        /// <param name="optKey">新的缓存key</param>
        /// <param name="keys">指定缓存key集合</param>
        /// <returns>结果集中的元素数量</returns>
        public long SetUnionStore(string optKey,params string[]keys)
        {
            return this._client.SUnionStore(optKey,keys);
        }

        #endregion
    }
}
