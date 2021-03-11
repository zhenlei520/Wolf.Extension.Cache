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
    /// Hash异步
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
            return this._quickHelperBase.HashGetAsync(key, hashKey);
        }

        #endregion

        #region 从缓存中取出缓存key对应的hash键集合（异步）

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        public async Task<List<HashResponse<string>>> HashGetAsync(string key, List<string> hashKeys)
        {
            var list = await this._quickHelperBase.HashGetAsync(key, hashKeys.ToArray());
            return list.Select(x => new HashResponse<string>(x.Key, x.Value)).ToList();
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
            return this._quickHelperBase.HashGetAsync(list);
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
        public async Task<T> HashGetAsync<T>(string key, string hashKey)
        {
            var vaue = await this.HashGetAsync(key, hashKey);
            return ConvertObj<T>(vaue);
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
            return (await this.HashGetAsync(key, hashKeys)).Select(x => new HashResponse<T>()
            {
                HashKey = x.HashKey,
                HashValue = ConvertObj<T>(x.HashValue)
            }).ToList();
        }

        #endregion

        #region 从缓存中取出多个key对应的hash键集合（异步）

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合（异步）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<HashMultResponse<T>>> HashGetAsync<T>(List<MultHashRequest<string>> list)
        {
            return (await this.HashGetAsync(list)).Select(x => new HashMultResponse<T>()
            {
                Key = x.Key,
                List = x.List.Select(y => new HashResponse<T>()
                {
                    HashKey = y.HashKey,
                    HashValue = ConvertObj<T>(y.HashValue)
                }).ToList()
            }).ToList();
        }

        #endregion

        #region 得到指定缓存key下的所有hash键集合

        /// <summary>
        /// 得到指定缓存key下的所有hash键集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键集合，默认查询全部</param>
        /// <returns></returns>
        public async Task<List<string>> HashKeyListAsync(string key, int top = bad)
        {
            var list = await this._quickHelperBase.HashKeysAsync(key);
            if (top == null)
            {
                return list.ToList();
            }

            return list.Take(top.Value).ToList();
        }

        #endregion

        #region 根据缓存key得到全部的hash键值对集合

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public async Task<List<HashResponse<string>>> HashListAsync(string key, int? top = null)
        {
            var hashKeys = await this.HashKeyListAsync(key, top);
            return this.HashGet(key, hashKeys);
        }

        #endregion

        #region 根据缓存key得到全部的hash键值对集合

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public async Task<List<HashResponse<T>>> HashListAsync<T>(string key, int top = -1)
        {
            var hashKeys = await this.HashKeyListAsync(key, top);
            return this.HashGet<T>(key, hashKeys);
        }

        #endregion

        #region 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public Task<List<HashMultResponse<string>>> HashMultListAsync(IEnumerable<string> keys, int? top = null)
        {
            var list = keys.Select(key => new MultHashRequest<string>()
            {
                Key = key,
                List = HashKeyList(key, top)
            });
            return this.HashGetAsync(list.ToList());
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
        public Task<List<HashMultResponse<T>>> HashMultListAsync<T>(IEnumerable<string> keys, int? top = null)
        {
            var list = keys.Select(key => new MultHashRequest<string>()
            {
                Key = key,
                List = HashKeyList(key, top)
            });
            return this.HashGetAsync<T>(list.ToList());
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
            return this._quickHelperBase.HashExistsAsync(key, hashKey);
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
            return await this._quickHelperBase.HashDeleteAsync(key, hashKey) >= 0;
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
            return await this._quickHelperBase.HashDeleteAsync(key, hashKeys.ToArray()) >= 0;
        }

        #endregion

        #region 删除多个缓存键对应的hash键集合（异步）

        /// <summary>
        /// 删除多个缓存键对应的hash键集合（异步）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> HashRangeDeleteAsync(List<BaseRequest<List<string>>> request)
        {
            bool isOk = false;
            foreach (var item in request.ToList())
            {
                if (await this.HashDeleteAsync(item.Key, item.Value))
                {
                    isOk = true;
                }
            }

            return isOk;
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
            return this._quickHelperBase.HashIncrementAsync(key, hashKey, val);
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
            return this._quickHelperBase.HashIncrementAsync(key, hashKey, -val);
        }

        #endregion
    }
}
