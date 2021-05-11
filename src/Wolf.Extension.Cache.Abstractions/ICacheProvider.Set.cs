// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// Set
    /// </summary>
    partial interface ICacheProvider
    {
        /// <summary>
        /// 向集合添加一个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        bool SetAdd(string key, string value);

        /// <summary>
        /// 向集合添加一个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool SetAdd<T>(string key, T value);

        /// <summary>
        /// 向集合添加一个或多个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <returns></returns>
        bool SetAddRange(string key, params string[] values);

        /// <summary>
        /// 向集合添加一个或多个成员
        /// </summary>
        /// <param name="key">缓存</param>
        /// <param name="values">缓存值多个</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool SetAddRange<T>(string key, params T[] values);

        /// <summary>
        /// 得到指定缓存的长度
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        long SetLength(string key);

        /// <summary>
        /// 得到给定缓存key集合的差集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        string[] SetDiff(params string[] keys);

        /// <summary>
        /// 得到给定缓存key集合的差集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns></returns>
        T[] SetDiff<T>(params string[] keys);

        /// <summary>
        /// 将指定缓存key集合的差集保存到指定的缓存key中
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        bool SetDiffStore(string destination, params string[] keys);

        /// <summary>
        /// 得到给定集合的交集
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        List<string> SetInter(params string[] keys);

        /// <summary>
        /// 得到给定集合的交集
        /// </summary>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        List<T> SetInter<T>(params string[] keys);

        /// <summary>
        /// 将给定集合的交集存储到指定的缓存key中
        /// </summary>
        /// <param name="destination">新的无序集合</param>
        /// <param name="keys">缓存key的集合</param>
        /// <returns></returns>
        bool SetInterStore(string destination, params string[] keys);

        /// <summary>
        /// 判断指定的缓存key的value是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        bool SetExists(string key, string value);

        /// <summary>
        /// 判断指定的缓存key的value是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool SetExists<T>(string key, T value);

        /// <summary>
        /// 根据缓存key得到集合中的所有成员
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        List<string> SetGet(string key);

        /// <summary>
        /// 根据缓存key得到集合中的所有成员
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        List<T> SetGet<T>(string key);

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        bool SetMove(string key, string optKey, string value);

        /// <summary>
        /// 根据缓存key将源集合移动到destination集合
        /// </summary>
        /// <param name="key">源缓存key</param>
        /// <param name="optKey">目标缓存key</param>
        /// <param name="value">缓存值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool SetMove<T>(string key, string optKey, T value);

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        string SetPop(string key);

        /// <summary>
        /// 根据缓存key获取一个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        T SetPop<T>(string key);

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        string[] SetPop(string key, int count);

        /// <summary>
        /// 根据缓存key获取count个随机元素并移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        T[] SetPop<T>(string key, int count);

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>获取一个随机元素</returns>
        string SetRandomGet(string key);

        /// <summary>
        /// 根据缓存key获取一个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取一个随机元素</returns>
        T SetRandomGet<T>(string key);

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <returns>获取count个随机元素</returns>
        string[] SetRandomGet(string key, int count);

        /// <summary>
        /// 根据缓存key获取count个随机元素但不移除
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="count">获取指定count的缓存</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>获取count个随机元素</returns>
        T[] SetRandomGet<T>(string key, int count);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        long SetRem(string key, string value);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">多个缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        long SetRem(string key, params string[] values);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        long SetRem<T>(string key, T value);

        /// <summary>
        /// 根据缓存key以及缓存值移除指定元素
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="values">缓存值</param>
        /// <returns>被成功移除的元素的数量，不包括被忽略的元素</returns>
        long SetRem<T>(string key, params T[] values);

        /// <summary>
        /// 得到给定集合的并集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns>并集成员的列表</returns>
        string[] SetUnion(params string[] keys);

        /// <summary>
        /// 得到给定集合的并集
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>并集成员的列表</returns>
        T[] SetUnion<T>(params string[] keys);

        /// <summary>
        /// 将指定的缓存key集合并到一起且保存到新的集合中并返回结果集中的元素数量
        /// </summary>
        /// <param name="optKey">新的缓存key</param>
        /// <param name="keys">指定缓存key集合</param>
        /// <returns>结果集中的元素数量</returns>
        long SetUnionStore(string optKey, params string[] keys);
    }
}
