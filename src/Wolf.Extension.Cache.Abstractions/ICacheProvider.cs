// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Response.Base;

namespace Wolf.Extension.Cache.Abstractions
{
    /// <summary>
    /// 基本存储
    /// </summary>
    public partial interface ICacheProvider
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool Set(string key, string value, PersistentOps persistentOps = null);

        /// <summary>
        /// 设置缓存键值对集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool Set(ICollection<BaseRequest<string>> list, PersistentOps persistentOps = null);

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(string key, T obj, PersistentOps persistentOps = null);

        /// <summary>
        /// 保存多个对象集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(ICollection<BaseRequest<T>> list, PersistentOps persistentOps = null);

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取多组缓存键集合
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        List<BaseResponse<string>> Get(ICollection<string> keys);

        /// <summary>
        /// 获取指定缓存的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取多组缓存键集合
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        List<BaseResponse<T>> Get<T>(ICollection<string> keys);

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">待增加的值</param>
        /// <returns>原value+val</returns>
        long Increment(string key, long val = 1);

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">待减的值</param>
        /// <returns>原value-val</returns>
        long Decrement(string key, long val = 1);

        /// <summary>
        /// 检查指定的缓存key是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        bool Exist(string key);

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool SetExpire(string key, BasePersistentOps persistentOps = null);

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        long Remove(string key);

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        long RemoveRange(ICollection<string> keys);

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key
        /// 默认得到全部的键
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        List<string> Keys(string pattern = "*");
    }
}
