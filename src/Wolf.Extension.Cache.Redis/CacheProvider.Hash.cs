// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions.Common;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Response.Hash;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Redis.Common;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// Hash缓存
    /// </summary>
    public partial class CacheProvider
    {
        #region 存储数据到Hash表

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <param name="value">hash值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool HashSet(
            string key,
            string hashKey,
            string value,
            HashPersistentOps persistentOps = null)
        {
            return this.HashSet<string>(key, hashKey, value, persistentOps);
        }

        #endregion

        #region 存储数据到Hash表

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">缓存键集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool HashSet(
            MultHashRequest<HashRequest<string>> request,
            HashPersistentOps persistentOps = null)
        {
            return this.HashSet<string>(request, persistentOps);
        }

        #endregion

        #region 存储数据到Hash表

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">缓存键以及Hash键值对集合的集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool HashSet(
            List<MultHashRequest<HashRequest<string>>> request,
            HashPersistentOps persistentOps = null)
        {
            return this.HashSet<string>(request, persistentOps);
        }

        #endregion

        #region 存储数据到Hash表

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <param name="value">hash值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSet<T>(
            string key,
            string hashKey,
            T value,
            HashPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>()
            {
                {key, new[] {hashKey, ConvertJson(value)}}
            };
            string cacheValue = "";
            cacheValue = persistentOps.IsCanHashExpire
                ? this._quickHelperBase.HashSetExpireByHash(dictionary, persistentOps)
                : this._quickHelperBase.HashSetExpire(dictionary, persistentOps);
            return string.Equals(cacheValue, "OK",
                StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region 存储数据到Hash表

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">hash键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSet<T>(MultHashRequest<HashRequest<T>> request,
            HashPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>()
            {
                {
                    request.Key,request.List.SelectMany(x=>new List<string>()
                    {
                        x.HashKey,
                        ConvertJson(x.Value)
                    }).ToArray()
                }
            };
            var cacheValue = persistentOps.IsCanHashExpire
                ? this._quickHelperBase.HashSetExpireByHash(dictionary, persistentOps)
                : this._quickHelperBase.HashSetExpire(dictionary, persistentOps);
            return string.Equals(cacheValue, "OK",
                StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region 存储数据到Hash表

        /// <summary>
        /// 存储数据到Hash表
        /// </summary>
        /// <param name="request">缓存键与hash键值对集合的集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSet<T>(
            List<MultHashRequest<HashRequest<T>>> request,
            HashPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
            request.ToList().ForEach(item =>
            {
                dictionary.Add(item.Key,item.List.SelectMany(x=>new List<string>()
                {
                    x.HashKey,
                    ConvertJson(x.Value)
                }).ToArray());
            });
            var cacheValue = persistentOps.IsCanHashExpire
                ? this._quickHelperBase.HashSetExpireByHash(dictionary, persistentOps)
                : this._quickHelperBase.HashSetExpire(dictionary, persistentOps);
            return string.Equals(cacheValue, "OK",
                StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region 根据缓存key以及hash key找到唯一对应的值

        /// <summary>
        /// 根据缓存key以及hash key找到唯一对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <returns></returns>
        public string HashGet(string key, string hashKey)
        {
            return this._quickHelperBase.HashGet(key, hashKey);
        }

        #endregion

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        List<HashResponse<string>> HashGet(string key, List<string> hashKeys);

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<HashMultResponse<string>> HashGet(List<MultHashRequest<string>> list);

        #region 根据缓存key以及hash key找到唯一对应的值

        /// <summary>
        /// 根据缓存key以及hash key找到唯一对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T HashGet<T>(string key, string hashKey)
        {
            var value = this.HashGet(key, hashKey);
            return ConvertObj<T>(value);
        }

        #endregion

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        List<HashResponse<T>> HashGet<T>(string key, List<string> hashKeys);

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<HashMultResponse<T>> HashGet<T>(List<MultHashRequest<string>> list);

        #region 得到指定缓存key下的所有hash键集合

        /// <summary>
        /// 得到指定缓存key下的所有hash键集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键集合，默认查询全部</param>
        /// <returns></returns>
        public List<string> HashKeyList(string key, int? top = null)
        {
            var list = QuickHelperBase.HashKeys(key);
            if (top == null)
            {
                return list.ToList();
            }

            return list.Take(top.Value).ToList();
        }

        #endregion

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        List<HashResponse<string>> HashList(string key, int? top = null);

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        List<HashResponse<T>> HashList<T>(string key, int? top = null);

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        List<HashMultResponse<string>> HashMultList(IEnumerable<string> keys, int? top = null);

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<HashMultResponse<T>> HashMultList<T>(IEnumerable<string> keys, int? top = null);

        #region 判断HashKey是否存在

        /// <summary>
        /// 判断HashKey是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">哈希键</param>
        /// <returns></returns>
        public bool HashExists(string key, string hashKey)
        {
            return QuickHelperBase.HashExists(key, hashKey);
        }

        #endregion

        #region 移除指定缓存键的Hash键对应的值

        /// <summary>
        /// 移除指定缓存键的Hash键对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <returns></returns>
        public bool HashDelete(string key, string hashKey)
        {
            return QuickHelperBase.HashDelete(key, hashKey) >= 0;
        }

        #endregion

        #region 移除指定缓存键的多个Hash键对应的值

        /// <summary>
        /// 移除指定缓存键的多个Hash键对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">hash键集合</param>
        /// <returns></returns>
        public bool HashDelete(string key, ICollection<string> hashKeys)
        {
            return QuickHelperBase.HashDelete(key, hashKeys.ToArray()) >= 0;
        }

        #endregion

        /// <summary>
        /// 删除多个缓存键对应的hash键集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool HashRangeDelete(List<BaseRequest<List<string>>> request);

        #region 为数字增长val

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <param name="val">增加的值</param>
        /// <returns>增长后的值</returns>
        public long HashIncrement(string key, string hashKey, long val = 1)
        {
            return QuickHelperBase.HashIncrement(key, hashKey, val);
        }

        #endregion

        #region 为数字减少val

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <param name="val">减少的值</param>
        /// <returns>减少后的值</returns>
        public long HashDecrement(string key, string hashKey, long val = 1)
        {
            return QuickHelperBase.HashIncrement(key, hashKey, -val);
        }

        #endregion
    }
}
