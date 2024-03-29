﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Response.Base;
using Wolf.Extension.Cache.Abstractions.Response.Hash;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// Hash
    /// </summary>
    public partial class CacheProvider
    {
        #region 存储数据到Hash表（指定缓存键值对存储）

        /// <summary>
        /// 存储数据到Hash表
        /// 指定缓存键值对存储
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

        #region 存储数据到Hash表（一组hash键值对集合）

        /// <summary>
        /// 存储数据到Hash表
        /// 一组hash键值对集合
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

        #region 存储数据到Hash表（多组缓存键与hash键值对集合的集合）

        /// <summary>
        /// 存储数据到Hash表
        /// 多组缓存键与hash键值对集合的集合
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

        #region 存储数据到Hash表（指定缓存键值对存储）

        /// <summary>
        /// 存储数据到Hash表
        /// 指定缓存键值对存储
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
            persistentOps = persistentOps ?? new HashPersistentOps();
            lock (_obj)
            {
                var hashSet = this.Get<HashSet<HashResponse<T>>>(key);
                if (hashSet != null)
                {
                    if (hashSet.Any(x => x.HashKey == hashKey))
                    {
                        if (persistentOps.SetStrategy != null && persistentOps.SetStrategy == SetStrategy.NoFind)
                        {
                            return false;
                        }
                        hashSet.RemoveWhere(x => x.HashKey == hashKey);
                    }
                    else
                    {
                        if(persistentOps.SetStrategy!=null&& persistentOps.SetStrategy==SetStrategy.Exist)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    hashSet = new HashSet<HashResponse<T>>();
                }

                hashSet.Add(new HashResponse<T>(hashKey, value));
                return this.Set(key, hashSet, new PersistentOps(persistentOps.HashOverdueTime)
                {
                    IsAtomic = false,
                    Strategy = OverdueStrategy.AbsoluteExpiration
                });
            }
        }

        #endregion

        #region 存储数据到Hash表（一组hash键值对集合）

        /// <summary>
        /// 存储数据到Hash表
        /// 一组hash键值对集合
        /// </summary>
        /// <param name="request">hash键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSet<T>(MultHashRequest<HashRequest<T>> request,
            HashPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps ?? new HashPersistentOps();
            lock (_obj)
            {
                var hashSet = this.Get<HashSet<HashResponse<T>>>(request.Key);
                foreach (var item in request.List)
                {
                    if (hashSet == null)
                    {
                        hashSet = new HashSet<HashResponse<T>>();
                    }

                    if (hashSet.Any(x => x.HashKey == item.HashKey))
                    {
                        if (persistentOps.SetStrategy != null && persistentOps.SetStrategy == SetStrategy.NoFind)
                        {
                            continue;
                        }
                        hashSet.RemoveWhere(x => x.HashKey == item.HashKey);
                    }
                    else
                    {
                        if (persistentOps.SetStrategy != null && persistentOps.SetStrategy == SetStrategy.Exist)
                        {
                            continue;
                        }
                    }

                    hashSet.Add(new HashResponse<T>(item.HashKey, item.Value));
                }

                return this.Set(request.Key, hashSet, new PersistentOps(persistentOps.HashOverdueTime)
                {
                    IsAtomic = false,
                    Strategy = OverdueStrategy.AbsoluteExpiration
                });
            }
        }

        #endregion

        #region 存储数据到Hash表（多组缓存键与hash键值对集合的集合）

        /// <summary>
        /// 存储数据到Hash表
        /// 多组缓存键与hash键值对集合的集合
        /// </summary>
        /// <param name="request">缓存键与hash键值对集合的集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSet<T>(ICollection<MultHashRequest<HashRequest<T>>> request, HashPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps ?? new HashPersistentOps();
            lock (_obj)
            {
                List<BaseRequest<HashSet<HashResponse<T>>>> list = new List<BaseRequest<HashSet<HashResponse<T>>>>();
                var cacheList = Get<HashSet<HashResponse<T>>>((ICollection<string>) request.Select(x => x.Key)) ??
                                new List<BaseResponse<HashSet<HashResponse<T>>>>(); //得到缓存键对应的hash集合的集合
                foreach (var item in request)
                {
                    var hashSet = cacheList.FirstOrDefault(x => x.Key == item.Key) ??new BaseResponse<HashSet<HashResponse<T>>>(); //指定缓存的hash键值对集合

                    List<string> takeHashKeyList = new List<string>();//跳过的hash集合
                    foreach (var hashInfo in item.List)
                    {
                        if (hashSet.Value.Any(x=>x.HashKey==hashInfo.HashKey))
                        {
                            if (persistentOps.SetStrategy != null && persistentOps.SetStrategy == SetStrategy.NoFind)
                            {
                                takeHashKeyList.Add(hashInfo.HashKey);
                                continue;
                            }
                            hashSet.Value.RemoveWhere(x=>x.HashKey==hashInfo.HashKey);
                        }
                        else
                        {
                            if (persistentOps.SetStrategy != null && persistentOps.SetStrategy == SetStrategy.Exist)
                            {
                                takeHashKeyList.Add(hashInfo.HashKey);
                            }
                        }
                    }

                    foreach (var hashInfo in item.List.Where(x=>!takeHashKeyList.Contains(x.HashKey)))
                    {
                        hashSet.Value.Add(new HashResponse<T>(hashInfo.HashKey, hashInfo.Value));
                    }

                    list.Add(new BaseRequest<HashSet<HashResponse<T>>>()
                    {
                        Key = hashSet.Key,
                        Value = hashSet.Value
                    });
                }

                return this.Set(list, new PersistentOps(persistentOps.HashOverdueTime)
                {
                    IsAtomic = false,
                    Strategy = OverdueStrategy.AbsoluteExpiration
                });
            }
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
            return HashGet<string>(key, hashKey);
        }

        #endregion

        #region 从缓存中取出缓存key对应的hash键集合

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        public List<HashResponse<string>> HashGet(string key, ICollection<string> hashKeys)
        {
            return this.HashGet<string>(key, hashKeys);
        }

        #endregion

        #region 从缓存中取出多个key对应的hash键集合

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<HashMultResponse<string>> HashGet(ICollection<MultHashRequest<string>> list)
        {
            return this.HashGet<string>(list);
        }

        #endregion

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
            var hashInfo = this.Get<HashSet<HashResponse<T>>>(key);
            if (hashInfo == null || hashInfo.All(x => x.HashKey != hashKey))
            {
                return default(T);
            }

            return hashInfo.Where(x=>x.HashKey==hashKey).Select(x=>x.HashValue).FirstOrDefault();
        }

        #endregion

        #region 从缓存中取出缓存key对应的hash键集合

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        public List<HashResponse<T>> HashGet<T>(string key, ICollection<string> hashKeys)
        {
            var hashInfo = this.Get<HashSet<HashResponse<T>>>(key);
            if (hashInfo == null)
            {
                return new List<HashResponse<T>>();
            }

            List<HashResponse<T>> list = new List<HashResponse<T>>();
            foreach (var hashKey in hashKeys)
            {
                list.Add(hashInfo.Any(x=>x.HashKey==hashKey)
                    ? new HashResponse<T>(hashKey,  hashInfo.Where(x=>x.HashKey== hashKey).Select(x=>x.HashValue).FirstOrDefault())
                    : new HashResponse<T>(hashKey, default(T)));
            }
            return list;
        }

        #endregion

        #region 从缓存中取出多个key对应的hash键集合

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<HashMultResponse<T>> HashGet<T>(ICollection<MultHashRequest<string>> list)
        {
            var cacheKeyList = list.Select(x => x.Key).ToList();
            List<BaseResponse<List<BaseResponse<T>>>> cacheList = this.Get<List<BaseResponse<T>>>(cacheKeyList);
            List<HashMultResponse<T>> res = new List<HashMultResponse<T>>();
            foreach (var item in list)
            {
                BaseResponse<List<BaseResponse<T>>> cacheInfo = cacheList.FirstOrDefault(x => x.Key == item.Key);
                if (cacheInfo?.Value == null)
                {
                    res.Add(new HashMultResponse<T>()
                    {
                        Key = item.Key,
                        List = item.List.Select(x => new HashResponse<T>()
                        {
                            HashKey = x,
                            HashValue = default(T)
                        }).ToList()
                    });
                }
                else
                {
                    HashMultResponse<T> hash = new HashMultResponse<T>()
                    {
                        Key = item.Key,
                        List = new List<HashResponse<T>>()
                    };
                    foreach (var key in item.List)
                    {
                        if (cacheInfo.Value.Any(x => x.Key == key))
                        {
                            var value = cacheInfo.Value.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
                            hash.List.Add(new HashResponse<T>(key, value));
                        }
                        else
                        {
                            hash.List.Add(new HashResponse<T>(key, default(T)));
                        }
                    }

                    res.Add(hash);
                }
            }
            return res;
        }

        #endregion

        #region 得到指定缓存key下的所有hash键集合

        /// <summary>
        /// 得到指定缓存key下的所有hash键集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键集合，默认查询全部</param>
        /// <returns></returns>
        public List<string> HashKeyList(string key, int top = -1)
        {
            var cacheInfo = this.Get<HashSet<HashResponse<string>>>(key);
            if (cacheInfo == null)
            {
                return new List<string>();
            }

            List<string> list = new List<string>();
            var i = 0;
            foreach (var item in cacheInfo)
            {
                if (top !=-1 || i < top)
                {
                    list.Add(item.HashKey);
                }

                i++;
            }

            return list;
        }

        #endregion

        #region 根据缓存key得到全部的hash键值对集合

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public List<HashResponse<string>> HashList(string key, int top = -1)
        {
            return this.HashList<string>(key, top);
        }

        #endregion

        #region 根据缓存key得到全部的hash键值对集合

        /// <summary>
        /// 根据缓存key得到全部的hash键值对集合
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public List<HashResponse<T>> HashList<T>(string key, int top = -1)
        {
            var cacheInfo = this.Get<HashSet<HashResponse<T>>>(key);
            if (cacheInfo == null)
            {
                return new List<HashResponse<T>>();
            }

            var i = 0;
            List<HashResponse<T>> list = new List<HashResponse<T>>();
            foreach (var item in cacheInfo)
            {
                if (top != -1 || i < top)
                {
                    list.Add(new HashResponse<T>()
                    {
                        HashKey = item.HashKey,
                        HashValue = item.HashValue
                    });
                }

                i++;
            }

            return list;
        }

        #endregion

        #region 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表

        /// <summary>
        /// 根据多个缓存key得到缓存key对应缓存key全部的hash键值对集合的集合列表
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <param name="top">得到前top条的Hash键值对集合，默认查询全部</param>
        /// <returns></returns>
        public List<HashMultResponse<string>> HashMultList(ICollection<string> keys, int top = -1)
        {
            return this.HashMultList<string>(keys, top);
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
        public List<HashMultResponse<T>> HashMultList<T>(ICollection<string> keys, int top = -1)
        {
            if (keys == null || !keys.Any())
            {
                return new List<HashMultResponse<T>>();
            }

            var cacheList = Get<HashSet<HashResponse<T>>>(keys);
            if (cacheList == null || !cacheList.Any())
            {
                return new List<HashMultResponse<T>>();
            }

            List<HashMultResponse<T>> multList = new List<HashMultResponse<T>>();
            foreach (var key in keys)
            {
                var cacheInfo = cacheList.FirstOrDefault(x => x.Key == key);
                var i = 0;
                List<HashResponse<T>> list = new List<HashResponse<T>>();
                foreach (var item in cacheInfo.Value)
                {
                    if (top != -1 || i < top)
                    {
                        list.Add(new HashResponse<T>()
                        {
                            HashKey = item.HashKey,
                            HashValue = item.HashValue
                        });
                    }

                    i++;
                }

                multList.Add(new HashMultResponse<T>()
                {
                    Key = key,
                    List = list
                });
            }

            return multList;
        }

        #endregion

        #region 判断HashKey是否存在

        /// <summary>
        /// 判断HashKey是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKey">哈希键</param>
        /// <returns></returns>
        public bool HashExists(string key, string hashKey)
        {
            var cacheInfo = this.Get<HashSet<HashResponse<object>>>(key);
            return cacheInfo != null && cacheInfo.Any(x=>x.HashKey==hashKey);
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
            var cacheInfo = this.Get<HashSet<HashResponse<object>>>(key);
            return this.DeleteHash(cacheInfo, hashKey);
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
            var cacheInfo = this.Get<HashSet<HashResponse<object>>>(key);
            if (cacheInfo == null)
            {
                return true;
            }

            foreach (var hashKey in hashKeys)
            {
                this.DeleteHash(cacheInfo, hashKey);
            }

            return true;
        }

        #endregion

        #region 删除多个缓存键对应的hash键集合

        /// <summary>
        /// 删除多个缓存键对应的hash键集合(存在一个成功的即为成功)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool HashRangeDelete(ICollection<BaseRequest<List<string>>> request)
        {
            var cacheKeyList = request.Select(x => x.Key).ToList();
            var hashList = this.Get<HashSet<HashResponse<object>>>(cacheKeyList);
            bool isSuccess = false;
            foreach (var item in request)
            {
                var hashInfo = hashList.FirstOrDefault(x => x.Key == item.Key);
                if (hashInfo != null)
                {
                    foreach (var hashKey in item.Value)
                    {
                        if (this.DeleteHash(hashInfo.Value, hashKey) && !isSuccess)
                        {
                            isSuccess = true;
                        }
                    }
                }
            }

            return isSuccess;
        }

        #endregion

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
            lock (_obj)
            {
                var cacheInfo = this.Get<HashSet<HashResponse<long>>>(key);
                if (cacheInfo == null)
                {
                    cacheInfo = new HashSet<HashResponse<long>>();
                }

                long hashValue;
                if (cacheInfo.Any(x=>x.HashKey==hashKey))
                {
                    hashValue = cacheInfo.Where(x => x.HashKey == hashKey).Select(x => x.HashValue).FirstOrDefault();
                    cacheInfo.RemoveWhere(x=>x.HashKey==hashKey);
                }
                else
                {
                    hashValue = 0;
                }

                var res = hashValue + val;
                cacheInfo.Add(new HashResponse<long>(hashKey, res) );
                this.Set(key, cacheInfo);
                return res;
            }
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
            lock (_obj)
            {
                var cacheInfo = this.Get<HashSet<HashResponse<long>>>(key);
                if (cacheInfo == null)
                {
                    cacheInfo = new HashSet<HashResponse<long>>();
                }

                long hashValue;
                if (cacheInfo.Any(x=>x.HashKey==hashKey))
                {
                    hashValue = cacheInfo.Where(x => x.HashKey == hashKey).Select(x => x.HashValue).FirstOrDefault();
                    cacheInfo.RemoveWhere(x=>x.HashKey==hashKey);
                }
                else
                {
                    hashValue = 0;
                }

                var res = hashValue - val;
                cacheInfo.Add(new HashResponse<long>(hashKey, res));
                this.Set(key, cacheInfo);
                return res;
            }
        }

        #endregion

        #region private methods

        #region 删除hashkey

        /// <summary>
        /// 删除hashkey
        /// </summary>
        /// <param name="hashSet"></param>
        /// <param name="hashKey"></param>
        /// <returns></returns>
        private bool DeleteHash(HashSet<HashResponse<object>> hashSet, string hashKey)
        {
            if (hashSet != null && hashSet.Any(x=>x.HashKey==hashKey))
            {
                hashSet.RemoveWhere(x=>x.HashKey==hashKey);
            }

            return true;
        }

        #endregion

        #endregion
    }
}
