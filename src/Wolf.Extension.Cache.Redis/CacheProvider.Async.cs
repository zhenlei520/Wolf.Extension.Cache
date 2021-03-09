// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRedis;
using Wolf.Extension.Cache.Abstractions.Common;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Response.Base;
using Wolf.Extension.Cache.Redis.Internal;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public partial class CacheProvider
    {
        #region 设置缓存（异步）

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> SetAsync(string key, string value, BasePersistentOps persistentOps = null)
        {
            return this.SetAsync<string>(key, value, persistentOps);
        }

        #endregion

        #region 设置缓存键值对集合

        /// <summary>
        /// 设置缓存键值对集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public Task<bool> SetAsync(List<BaseRequest<string>> list, PersistentOps persistentOps = null)
        {
            return this.SetAsync<string>(list, persistentOps);
        }

        #endregion

        #region 保存一个对象

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SetAsync<T>(string key, T obj, BasePersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            return this._client.SetAsync(key, obj, persistentOps.OverdueTimeSpan,
                persistentOps.SetStrategy.GetRedisExistence());
        }

        #endregion

        #region 保存多个对象集合

        /// <summary>
        /// 保存多个对象集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> SetAsync<T>(List<BaseRequest<T>> list, PersistentOps persistentOps = null)
        {
            persistentOps = persistentOps.Get();
            RedisExistence? exists = persistentOps.SetStrategy.GetRedisExistence();
            var ret = this._client.StartPipe(client =>
            {
                CSRedisClientPipe<bool> clientPipe = null;
                foreach (var item in list)
                {
                    if (clientPipe == null)
                    {
                        clientPipe = client.Set(item.Key, item.Value, persistentOps.OverdueTimeSpan,
                            exists);
                    }
                    else
                    {
                        clientPipe.Set(item.Key, item.Value, persistentOps.OverdueTimeSpan, exists);
                    }
                }
            });

            return Task.FromResult(ret.Any(x => (bool) x));
        }

        #endregion

        #region 获取单个key的值（异步）

        /// <summary>
        /// 获取单个key的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public Task<string> GetAsync(string key)
        {
            return this._client.GetAsync(key);
        }

        #endregion

        #region 获取多组缓存键集合（异步）

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        public Task<List<BaseResponse<string>>> GetAsync(ICollection<string> keys)
        {
            return this.GetAsync<string>(keys);
        }

        #endregion

        #region 获取指定缓存的值（异步）

        /// <summary>
        /// 获取指定缓存的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key)
        {
            return this._client.GetAsync<T>(key);
        }

        #endregion

        #region 获取多组缓存键集合（异步）

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        public Task<List<BaseResponse<T>>> GetAsync<T>(ICollection<string> keys)
        {
            var ret = this._client.StartPipe(client =>
            {
                CSRedisClientPipe<T> clientPipe = null;
                foreach (var key in keys)
                {
                    if (clientPipe == null)
                    {
                        clientPipe = client.Get<T>(key);
                    }
                    else
                    {
                        clientPipe.Get<T>(key);
                    }
                }
            });
            List<BaseResponse<T>> list = new List<BaseResponse<T>>();
            for (int index = 0; index < keys.Count(); index++)
            {
                list.Add(new BaseResponse<T>(keys.ToList()[index], (T) ret[index]));
            }

            return Task.FromResult(list);
        }

        #endregion

        #region 为数字增长val（异步）

        /// <summary>
        /// 为数字增长val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        public Task<long> IncrementAsync(string key, long val = 1)
        {
            return this._client.IncrByAsync(key, val);
        }

        #endregion

        #region 为数字减少val

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">待减的值</param>
        /// <returns>原value-val</returns>
        public Task<long> DecrementAsync(string key, long val = 1)
        {
            return this._client.IncrByAsync(key, -val);
        }

        #endregion

        #region 检查指定的缓存key是否存在（异步）

        /// <summary>
        /// 检查指定的缓存key是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<bool> ExistAsync(string key)
        {
            return this._client.ExistsAsync(key);
        }

        #endregion

        #region 设置过期时间（异步）

        /// <summary>
        /// 设置过期时间（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="timeSpan">过期时间，永久保存：TimeSpan.Zero</param>
        /// <param name="strategy">过期策略,默认绝对过期</param>
        /// <returns></returns>
        public Task<bool> SetExpireAsync(string key, TimeSpan timeSpan,
            OverdueStrategy strategy = OverdueStrategy.AbsoluteExpiration)
        {
            if (timeSpan < TimeSpan.Zero)
            {
                throw new NotSupportedException(nameof(timeSpan));
            }

            return this._client.ExpireAsync(key, timeSpan);
        }

        #endregion

        #region 删除指定Key的缓存（异步）

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        public Task<long> RemoveAsync(string key)
        {
            return this._client.DelAsync(key);
        }

        #endregion

        #region 删除指定Key的缓存（异步）

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        public Task<long> RemoveRangeAsync(List<string> keys)
        {
            if (keys == null || !keys.Any())
            {
                return Task.FromResult(0l);
            }

            return this._client.DelAsync(keys.ToArray());
        }

        #endregion

        #region 查找所有符合给定模式( pattern)的 key（异步）

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key（异步）
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public async Task<List<string>> KeysAsync(string pattern = "*")
        {
            return (await this._client.KeysAsync(pattern)).ToList();
        }

        #endregion
    }
}
