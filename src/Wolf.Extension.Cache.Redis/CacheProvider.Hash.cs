// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using CSRedis;
using Wolf.Extension.Cache.Abstractions.Common;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Response.Hash;
using Wolf.Extension.Cache.Redis.Internal;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// Hash
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
            if (persistentOps.IsCanHashExpire)
            {
                return false;
            }
            else
            {
                return this._client.StartPipe(pipe =>
                {
                    pipe.HSet(key, hashKey, value);
                    pipe.Expire(key, persistentOps.HashOverdueTimeSpan);
                }).Any(x => (bool) x);
            }
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
            var ret = this._client.StartPipe(client =>
            {
                CSRedisClientPipe<bool> clientPipe = null;
                RedisExistence? exists = persistentOps.SetStrategy.GetRedisExistence();

                if (persistentOps.IsCanHashExpire)
                {
                    //指定hash键过期
                }
                else
                {
                    foreach (var hashRequest in request.List)
                    {
                        if (clientPipe == null)
                        {
                            clientPipe = client.HSet(request.Key, hashRequest.HashKey, hashRequest.Value, exists);
                        }
                        else
                        {
                            clientPipe.HSet(request.Key, hashRequest.HashKey, hashRequest.Value, exists);
                        }
                    }

                    clientPipe?.Expire(request.Key, persistentOps.HashOverdueTimeSpan);
                }
            });
            return ret.Any(x => (bool) x);
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
            ICollection<MultHashRequest<HashRequest<T>>> request,
            HashPersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            var ret = this._client.StartPipe(client =>
            {
                CSRedisClientPipe<bool> clientPipe = null;
                RedisExistence? exists = persistentOps.SetStrategy.GetRedisExistence();

                if (persistentOps.IsCanHashExpire)
                {
                    //指定hash键过期
                }
                else
                {
                    foreach (var item in request)
                    {
                        foreach (var hashRequest in item.List)
                        {
                            if (clientPipe == null)
                            {
                                clientPipe = client.HSet(item.Key, hashRequest.HashKey, hashRequest.Value, exists);
                            }
                            else
                            {
                                clientPipe.HSet(item.Key, hashRequest.HashKey, hashRequest.Value, exists);
                            }
                        }

                        clientPipe?.Expire(item.Key, persistentOps.HashOverdueTimeSpan);
                    }
                }
            });
            return ret.Any(x => (bool) x);
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
            return this.HashGet<string>(key, hashKey);
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
            return this._client.HGet<T>(key, hashKey);
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
            if (hashKeys == null || hashKeys.Count == 0)
            {
                return new List<HashResponse<T>>();
            }

            List<HashResponse<T>> list = new List<HashResponse<T>>();
            var ret = this._client.HMGet<T>(key, hashKeys.ToArray());
            var hashKeyList = hashKeys.ToList();
            for (int i = 0; i < hashKeyList.Count; i++)
            {
                list.Add(new HashResponse<T>(hashKeyList[i], ret[i]));
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
            return list.Select(x => new HashMultResponse<T>()
            {
                Key = x.Key,
                List = this.HashGet<T>(x.Key, x.List.ToList())
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
        public List<string> HashKeyList(string key, int? top = null)
        {
            var arrays = this._client.HKeys(key);
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
            var hashKeys = this.HashKeyList(key, top);
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
        public List<HashMultResponse<string>> HashMultList(ICollection<string> keys, int? top = null)
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
        public List<HashMultResponse<T>> HashMultList<T>(ICollection<string> keys, int? top = null)
        {
            return keys.Select(key => new HashMultResponse<T>()
            {
                Key = key,
                List = this.HashList<T>(key, top).ToList()
            }).ToList();
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
            return this._client.HExists(key, hashKey);
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
            return this._client.HDel(key, hashKey) > 0;
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
            return this._client.HDel(key, hashKeys.ToArray()) > 0;
        }

        #endregion

        #region 删除多个缓存键对应的hash键集合

        /// <summary>
        /// 删除多个缓存键对应的hash键集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool HashRangeDelete(ICollection<BaseRequest<List<string>>> request)
        {
            return request.Select(x => HashDelete(x.Key, x.Value)).Any();
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
            return this._client.HIncrBy(key, hashKey, val);
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
            return this._client.HIncrBy(key, hashKey, -val);
        }

        #endregion
    }
}
