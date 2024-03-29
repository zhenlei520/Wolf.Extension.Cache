﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Response.Hash;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// Hash
    /// </summary>
    public partial interface ICacheProvider
    {
        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <param name="value">hash值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool HashSet(
            string key,
            string hashKey,
            string value,
            HashPersistentOps persistentOps = null);

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">缓存键集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool HashSet(
            MultHashRequest<HashRequest<string>> request,
            HashPersistentOps persistentOps = null);

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">缓存键以及Hash键值对集合的集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool HashSet(
            List<MultHashRequest<HashRequest<string>>> request,
            HashPersistentOps persistentOps = null);

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <param name="value">hash值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSet<T>(
            string key,
            string hashKey,
            T value,
            HashPersistentOps persistentOps = null);

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">hash键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSet<T>(MultHashRequest<HashRequest<T>> request,
            HashPersistentOps persistentOps = null);

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">缓存键与hash键值对集合的集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HashSet<T>(
            ICollection<MultHashRequest<HashRequest<T>>> request,
            HashPersistentOps persistentOps = null);

        /// <summary>
        /// 根据缓存key以及hash key找到唯一对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <returns></returns>
        string HashGet(string key, string hashKey);

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        List<HashResponse<string>> HashGet(string key, ICollection<string> hashKeys);

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<HashMultResponse<string>> HashGet(ICollection<MultHashRequest<string>> list);

        /// <summary>
        /// 根据缓存key以及hash key找到唯一对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T HashGet<T>(string key, string hashKey);

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        List<HashResponse<T>> HashGet<T>(string key, ICollection<string> hashKeys);

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<HashMultResponse<T>> HashGet<T>(ICollection<MultHashRequest<string>> list);

        /// <summary>
        /// 得到指定缓存key下的所有hash键集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键集合，默认查询全部</param>
        /// <returns></returns>
        List<string> HashKeyList(string key, int top = -1);

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        List<HashResponse<string>> HashList(string key, int top = -1);

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        List<HashResponse<T>> HashList<T>(string key, int top = -1);

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        List<HashMultResponse<string>> HashMultList(ICollection<string> keys, int top = -1);

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<HashMultResponse<T>> HashMultList<T>(ICollection<string> keys, int top = -1);

        /// <summary>
        /// 判断HashKey是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">哈希键</param>
        /// <returns></returns>
        bool HashExists(string key, string hashKey);

        /// <summary>
        /// 移除指定缓存键的Hash键对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <returns></returns>
        bool HashDelete(string key, string hashKey);

        /// <summary>
        /// 移除指定缓存键的多个Hash键对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">hash键集合</param>
        /// <returns></returns>
        bool HashDelete(string key, ICollection<string> hashKeys);

        /// <summary>
        /// 删除多个缓存键对应的hash键集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool HashRangeDelete(ICollection<BaseRequest<List<string>>> request);

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <param name="val">增加的值</param>
        /// <returns>增长后的值</returns>
        long HashIncrement(string key, string hashKey, long val = 1);

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <param name="val">减少的值</param>
        /// <returns>减少后的值</returns>
        long HashDecrement(string key, string hashKey, long val = 1);
    }
}
