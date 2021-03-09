// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Response.Hash;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// Hash
    /// </summary>
    public partial class CacheProvider
    {
        #region 存储数据到Hash表（异步）

        /// <summary>
        /// 存储数据到Hash表（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <param name="value">hash值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> HashSetAsync(
            string key,
            string hashKey,
            string value,
            HashPersistentOps persistentOps = null)
        {
            return this.HashSetAsync<string>(key, hashKey, value, persistentOps);
        }

        #endregion

        #region 存储数据到Hash表（异步）

        /// <summary>
        /// 存储数据到Hash表（异步）
        /// </summary>
        /// <param name="request">缓存键集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> HashSetAsync(
            MultHashRequest<HashRequest<string>> request,
            HashPersistentOps persistentOps = null)
        {
            return this.HashSetAsync<string>(request, persistentOps);
        }

        #endregion

        #region 存储数据到Hash表（异步）

        /// <summary>
        /// 存储数据到Hash表（异步）
        /// </summary>
        /// <param name="request">缓存键以及Hash键值对集合的集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> HashSetAsync(
            List<MultHashRequest<HashRequest<string>>> request,
            HashPersistentOps persistentOps = null)
        {
            return this.HashSetAsync<string>(request, persistentOps);
        }

        #endregion

        #region 存储数据到Hash表（异步）

        /// <summary>
        /// 存储数据到Hash表（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <param name="value">hash值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> HashSetAsync<T>(
            string key,
            string hashKey,
            T value,
            HashPersistentOps persistentOps = null)
        {
            return Task.FromResult(this.HashSet(key, hashKey, value, persistentOps));
        }

        #endregion

        #region 存储数据到Hash表（异步）

        /// <summary>
        /// 存储数据到Hash表（异步）
        /// </summary>
        /// <param name="request">hash键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> HashSetAsync<T>(MultHashRequest<HashRequest<T>> request,
            HashPersistentOps persistentOps = null)
        {
            return Task.FromResult(this.HashSet(request, persistentOps));
        }

        #endregion

        #region 存储数据到Hash表（异步）

        /// <summary>
        /// 存储数据到Hash表（异步）
        /// </summary>
        /// <param name="request">缓存键与hash键值对集合的集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> HashSetAsync<T>(
            List<MultHashRequest<HashRequest<T>>> request,
            HashPersistentOps persistentOps = null)
        {
            return Task.FromResult(this.HashSet(request, persistentOps));
        }

        #endregion

        #region 根据缓存key以及hash key找到唯一对应的值（异步）

        /// <summary>
        /// 根据缓存key以及hash key找到唯一对应的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <returns></returns>
        public Task<string> HashGetAsync(string key, string hashKey)
        {
            return this.HashGetAsync<string>(key, hashKey);
        }

        #endregion

        #region 从缓存中取出缓存key对应的hash键集合（异步）

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        public Task<List<HashResponse<string>>> HashGetAsync(string key, List<string> hashKeys)
        {
            return this.HashGetAsync<string>(key, hashKeys);
        }

        #endregion

        #region 从缓存中取出多个key对应的hash键集合（异步）

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合（异步）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task<List<HashMultResponse<string>>> HashGetAsync(List<MultHashRequest<string>> list)
        {
            return this.HashGetAsync<string>(list);
        }

        #endregion

        #region 根据缓存key以及hash key找到唯一对应的值（异步）

        /// <summary>
        /// 根据缓存key以及hash key找到唯一对应的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> HashGetAsync<T>(string key, string hashKey)
        {
            return this._client.HGetAsync<T>(key, hashKey);
        }

        #endregion

        #region 从缓存中取出缓存key对应的hash键集合（异步）

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        public async Task<List<HashResponse<T>>> HashGetAsync<T>(string key, ICollection<string> hashKeys)
        {
            if (hashKeys == null || hashKeys.Count == 0)
            {
                return new List<HashResponse<T>>();
            }

            List<HashResponse<T>> list = new List<HashResponse<T>>();
            var ret = await this._client.HMGetAsync<T>(key, hashKeys.ToArray());
            var hashKeyList = hashKeys.ToList();
            for (int i = 0; i < hashKeyList.Count; i++)
            {
                list.Add(new HashResponse<T>(hashKeyList[i], ret[i]));
            }

            return list;
        }

        #endregion

        #region 从缓存中取出多个key对应的hash键集合（异步）

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合（异步）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<HashMultResponse<T>>> HashGetAsync<T>(ICollection<MultHashRequest<string>> list)
        {
            List<HashMultResponse<T>> ret = new List<HashMultResponse<T>>();
            foreach (var item in list)
            {
                ret.Add(new HashMultResponse<T>()
                {
                    Key = item.Key,
                    List = await this.HashGetAsync<T>(item.Key, item.List.ToList())
                });
            }

            return ret;
        }

        #endregion

        #region 得到指定缓存key下的所有hash键集合

        /// <summary>
        /// 得到指定缓存key下的所有hash键集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键集合，默认查询全部</param>
        /// <returns></returns>
        public async Task<List<string>> HashKeyListAsync(string key, int? top = null)
        {
            var arrays = await this._client.HKeysAsync(key);
            if (top != null)
            {
                return arrays.Take(top.Value).ToList();
            }

            return arrays.ToList();
        }

        #endregion

        #region 根据缓存key得到全部的hash键值对集合

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public Task<List<HashResponse<string>>> HashListAsync(string key, int? top = null)
        {
            return this.HashListAsync<string>(key, top);
        }

        #endregion

        #region 根据缓存key得到全部的hash键值对集合

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public Task<List<HashResponse<T>>> HashListAsync<T>(string key, int? top = null)
        {
            var hashKeys = this.HashKeyList(key, top);
            return this.HashGetAsync<T>(key, hashKeys);
        }

        #endregion

        #region 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public Task<List<HashMultResponse<string>>> HashMultListAsync(ICollection<string> keys, int? top = null)
        {
            return this.HashMultListAsync<string>(keys, top);
        }

        #endregion

        #region 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<HashMultResponse<T>>> HashMultListAsync<T>(ICollection<string> keys, int? top = null)
        {
            List<HashMultResponse<T>> ret = new List<HashMultResponse<T>>();
            foreach (var key in keys)
            {
                ret.Add(new HashMultResponse<T>()
                {
                    Key = key,
                    List = await this.HashListAsync<T>(key, top)
                });
            }

            return ret;
        }

        #endregion

        #region 判断HashKey是否存在（异步）

        /// <summary>
        /// 判断HashKey是否存在（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">哈希键</param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string hashKey)
        {
            return this._client.HExistsAsync(key, hashKey);
        }

        #endregion

        #region 移除指定缓存键的Hash键对应的值（异步）

        /// <summary>
        /// 移除指定缓存键的Hash键对应的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">Hash键</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string hashKey)
        {
            return await (this._client.HDelAsync(key, hashKey)) > 0;
        }

        #endregion

        #region 移除指定缓存键的多个Hash键对应的值（异步）

        /// <summary>
        /// 移除指定缓存键的多个Hash键对应的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">hash键集合</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, ICollection<string> hashKeys)
        {
            return await (this._client.HDelAsync(key, hashKeys.ToArray())) > 0;
        }

        #endregion

        #region 删除多个缓存键对应的hash键集合（异步）

        /// <summary>
        /// 删除多个缓存键对应的hash键集合（异步）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> HashRangeDeleteAsync(ICollection<BaseRequest<List<string>>> request)
        {
            List<bool> ret = new List<bool>();
            foreach (var item in request)
            {
                ret.Add(await this.HashDeleteAsync(item.Key, item.Value));
            }

            return ret.Any();
        }

        #endregion

        #region 为数字增长val（异步）

        /// <summary>
        /// 为数字增长val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">hash键</param>
        /// <param name="val">增加的值</param>
        /// <returns>增长后的值</returns>
        public Task<long> HashIncrementAsync(string key, string hashKey, long val = 1)
        {
            return this._client.HIncrByAsync(key, hashKey, val);
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
        public Task<long> HashDecrementAsync(string key, string hashKey, long val = 1)
        {
            return this._client.HIncrByAsync(key, hashKey, -val);
        }

        #endregion
    }
}
