// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Response.Base;
using Wolf.Extension.Cache.Abstractions.Response.Hash;
using Wolf.Systems.Core;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// Hash
    /// </summary>
    public partial class CacheProvider
    {
        #region 同步

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
            lock (obj)
            {
                var hashTable = this.Get<Hashtable>(key);
                if (hashTable != null)
                {
                    if (hashTable.ContainsKey(hashKey))
                    {
                        hashTable.Remove(hashKey);
                    }
                }
                else
                {
                    hashTable = new Hashtable();
                }

                hashTable.Add(hashKey, value);
                return this.Set(key, hashTable, persistentOps: persistentOps);
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
            lock (obj)
            {
                var hashTable = this.Get<Hashtable>(request.Key);
                foreach (var item in request.List)
                {
                    if (hashTable == null)
                    {
                        hashTable = new Hashtable();
                    }

                    if (hashTable.ContainsKey(item.HashKey))
                    {
                        hashTable.Remove(item.HashKey);
                    }

                    hashTable.Add(item.HashKey, item.Value);
                }

                return this.Set(request.Key, hashTable, persistentOps: persistentOps);
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
        public bool HashSet<T>(
            List<MultHashRequest<HashRequest<T>>> request,
            HashPersistentOps persistentOps = null)
        {
            lock (obj)
            {
                List<BaseRequest<Hashtable>> list = new List<BaseRequest<Hashtable>>();

                var cacheList = this.Get<Hashtable>(request.Select(x => x.Key)) ??
                                new List<BaseResponse<Hashtable>>(); //得到缓存键对应的hash集合的集合
                request.ForEach(item =>
                {
                    var hashTable = cacheList.FirstOrDefault(x => x.Key == item.Key) ??
                                    new BaseResponse<Hashtable>(item.Key, new Hashtable()); //指定缓存的hash键值对集合

                    foreach (var hashInfo in item.List)
                    {
                        if (hashTable.Value.ContainsKey(hashInfo.HashKey))
                        {
                            hashTable.Value.Remove(hashInfo.HashKey);
                        }
                    }

                    foreach (var hashInfo in item.List)
                    {
                        hashTable.Value.Add(hashInfo.HashKey, hashInfo.Value);
                    }

                    list.Add(new BaseRequest<Hashtable>()
                    {
                        Key = hashTable.Key,
                        Value = hashTable.Value
                    });
                });

                return this.Set(list, null, persistentOps);
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
        public List<HashResponse<string>> HashGet(string key, List<string> hashKeys)
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
        public List<HashMultResponse<string>> HashGet(List<MultHashRequest<string>> list)
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
            var hashInfo = this.Get<Hashtable>(key);
            if (hashInfo == null || !hashInfo.ContainsKey(hashKey))
            {
                return default(T);
            }

            return (T) hashInfo[hashKey];
        }

        #endregion

        #region 从缓存中取出缓存key对应的hash键集合

        /// <summary>
        /// 从缓存中取出缓存key对应的hash键集合
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">Hash键集合</param>
        /// <returns></returns>
        public List<HashResponse<T>> HashGet<T>(string key, List<string> hashKeys)
        {
            var hashInfo = this.Get<Hashtable>(key);
            if (hashInfo == null)
            {
                return new List<HashResponse<T>>();
            }

            List<HashResponse<T>> list = new List<HashResponse<T>>();
            hashKeys.ForEach(hashKey =>
            {
                list.Add(hashInfo.ContainsKey(hashKey)
                    ? new HashResponse<T>(hashKey, (T) hashInfo[hashKey])
                    : new HashResponse<T>(hashKey, default(T)));
            });
            return list;
        }

        #endregion

        #region 从缓存中取出多个key对应的hash键集合

        /// <summary>
        /// 从缓存中取出多个key对应的hash键集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<HashMultResponse<T>> HashGet<T>(List<MultHashRequest<string>> list)
        {
            var cacheKeyList = list.Select(x => x.Key).ToList();
            List<BaseResponse<List<BaseResponse<T>>>> cacheList = this.Get<List<BaseResponse<T>>>(cacheKeyList);
            List<HashMultResponse<T>> res = new List<HashMultResponse<T>>();
            list.ForEach(item =>
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
            });
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
        public List<string> HashKeyList(string key, int? top = null)
        {
            var cacheInfo = this.Get<Hashtable>(key);
            if (cacheInfo == null)
            {
                return new List<string>();
            }

            List<string> list = new List<string>();
            var i = 0;
            foreach (string hashKey in cacheInfo.Keys)
            {
                if (top == null || i < top.Value)
                {
                    list.Add(hashKey);
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
        public List<HashResponse<string>> HashList(string key, int? top = null)
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
        public List<HashResponse<T>> HashList<T>(string key, int? top = null)
        {
            var cacheInfo = this.Get<Hashtable>(key);
            if (cacheInfo == null)
            {
                return new List<HashResponse<T>>();
            }

            var i = 0;
            List<HashResponse<T>> list = new List<HashResponse<T>>();
            foreach (string hashKey in cacheInfo.Keys)
            {
                if (top == null || i < top.Value)
                {
                    list.Add(new HashResponse<T>()
                    {
                        HashKey = hashKey,
                        HashValue = (T) cacheInfo[hashKey]
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
        public List<HashMultResponse<string>> HashMultList(IEnumerable<string> keys, int? top = null)
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
        public List<HashMultResponse<T>> HashMultList<T>(IEnumerable<string> keys, int? top = null)
        {
            if (keys == null || !keys.Any())
            {
                return new List<HashMultResponse<T>>();
            }

            var cacheList = this.Get<Hashtable>(keys);
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
                foreach (string hashKey in cacheInfo.Value.Keys)
                {
                    if (top == null || i < top.Value)
                    {
                        list.Add(new HashResponse<T>()
                        {
                            HashKey = hashKey,
                            HashValue = (T) cacheInfo.Value[hashKey]
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
            var cacheInfo = this.Get<Hashtable>(key);
            return cacheInfo != null && cacheInfo.ContainsKey(hashKey);
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
            var cacheInfo = this.Get<Hashtable>(key);
            if (cacheInfo == null || !cacheInfo.ContainsKey(hashKey))
            {
                return true;
            }

            cacheInfo.Remove(hashKey);
            return true;
        }

        #endregion

        #region 移除指定缓存键的多个Hash键对应的值

        /// <summary>
        /// 移除指定缓存键的多个Hash键对应的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashKeys">hash键集合</param>
        /// <returns></returns>
        public bool HashDelete(string key, List<string> hashKeys)
        {
            var cacheInfo = this.Get<Hashtable>(key);
            if (cacheInfo == null)
            {
                return true;
            }

            foreach (var hashKey in hashKeys)
            {
                cacheInfo.Remove(hashKey);
            }

            return true;
        }

        #endregion

        /// <summary>
        /// 删除多个缓存键对应的hash键集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool HashRangeDelete(List<HashMultResponse<string>> request);

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

        #endregion

        #region 异步

        #endregion
    }
}
