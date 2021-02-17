// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Response.Base;
using Wolf.Extension.Cache.Abstractions.Response.Hash;

namespace Wolf.Extension.Cache.Redis
{
    /// <summary>
    /// 基本存储
    /// </summary>
    public partial class CacheProvider :BaseRedisCacheProvider, ICacheProvider
    {
        #region 同步

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        public bool Set(string key, string value, TimeSpan? expiry = null, PersistentOps persistentOps = null)
        {
        }

        bool ICacheProvider.Set(IEnumerable<BaseRequest<string>> list, TimeSpan? expiry, PersistentOps persistentOps)
        {
            return Set(list, expiry, persistentOps);
        }

        bool ICacheProvider.Set<T>(string key, T obj, TimeSpan? expiry, PersistentOps persistentOps)
        {
            return Set(key, obj, expiry, persistentOps);
        }

        bool ICacheProvider.Set<T>(IEnumerable<BaseRequest<T>> list, TimeSpan? expiry, PersistentOps persistentOps)
        {
            return Set(list, expiry, persistentOps);
        }

        string ICacheProvider.Get(string key)
        {
            return Get(key);
        }

        List<BaseResponse<string>> ICacheProvider.Get(IEnumerable<string> keys)
        {
            return Get(keys);
        }

        T ICacheProvider.Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        List<BaseResponse<T>> ICacheProvider.Get<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        long ICacheProvider.Increment(string key, long val)
        {
            return Increment(key, val);
        }

        long ICacheProvider.Decrement(string key, long val)
        {
            return Decrement(key, val);
        }

        bool ICacheProvider.Exist(string key)
        {
            return Exist(key);
        }

        bool ICacheProvider.SetExpire(string key, TimeSpan expiry, PersistentOps persistentOps)
        {
            return SetExpire(key, expiry, persistentOps);
        }

        bool ICacheProvider.Remove(string key)
        {
            return Remove(key);
        }

        bool ICacheProvider.RemoveRange(IEnumerable<string> keys)
        {
            return RemoveRange(keys);
        }

        List<string> ICacheProvider.Keys(string pattern)
        {
            return Keys(pattern);
        }

        Task<bool> ICacheProvider.SetAsync(string key, string value, TimeSpan? expiry, PersistentOps persistentOps)
        {
            return SetAsync(key, value, expiry, persistentOps);
        }

        Task<bool> ICacheProvider.SetAsync(List<BaseRequest<string>> list, TimeSpan? expiry, PersistentOps persistentOps)
        {
            return SetAsync(list, expiry, persistentOps);
        }

        Task<bool> ICacheProvider.SetAsync<T>(string key, T obj, TimeSpan? expiry, PersistentOps persistentOps)
        {
            return SetAsync(key, obj, expiry, persistentOps);
        }

        Task<bool> ICacheProvider.SetAsync<T>(List<BaseRequest<T>> list, TimeSpan? expiry, PersistentOps persistentOps)
        {
            return SetAsync(list, expiry, persistentOps);
        }

        Task<string> ICacheProvider.GetAsync(string key)
        {
            return GetAsync(key);
        }

        Task<List<BaseResponse<string>>> ICacheProvider.GetAsync(IEnumerable<string> keys)
        {
            return GetAsync(keys);
        }

        Task<T> ICacheProvider.GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        Task<List<BaseResponse<T>>> ICacheProvider.GetAsync<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        Task<long> ICacheProvider.IncrementAsync(string key, long val)
        {
            return IncrementAsync(key, val);
        }

        Task<long> ICacheProvider.DecrementAsync(string key, long val)
        {
            return DecrementAsync(key, val);
        }

        Task<bool> ICacheProvider.ExistAsync(string key)
        {
            return ExistAsync(key);
        }

        Task<bool> ICacheProvider.SetExpireAsync(string key, TimeSpan expiry, PersistentOps persistentOps)
        {
            return SetExpireAsync(key, expiry, persistentOps);
        }

        Task<bool> ICacheProvider.RemoveAsync(string key)
        {
            return RemoveAsync(key);
        }

        Task<bool> ICacheProvider.RemoveRangeAsync(List<string> keys)
        {
            return RemoveRangeAsync(keys);
        }

        Task<List<string>> ICacheProvider.KeysAsync(string pattern)
        {
            return KeysAsync(pattern);
        }

        public bool HashSet(string key, string hashKey, string value, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool HashSet(MultHashRequest<HashRequest<string>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool HashSet(List<MultHashRequest<HashRequest<string>>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool HashSet<T>(string key, string hashKey, T value, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool HashSet<T>(MultHashRequest<HashRequest<T>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool HashSet<T>(List<MultHashRequest<HashRequest<T>>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public string HashGet(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<string>> HashGet(string key, List<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<string>> HashGet(List<MultHashRequest<string>> list)
        {
            throw new NotImplementedException();
        }

        public T HashGet<T>(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<T>> HashGet<T>(string key, List<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<T>> HashGet<T>(List<MultHashRequest<string>> list)
        {
            throw new NotImplementedException();
        }

        public List<string> HashKeyList(string key, int? top = null)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<string>> HashList(string key, int? top = null)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<T>> HashList<T>(string key, int? top = null)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<string>> HashMultList(IEnumerable<string> keys, int? top = null)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<T>> HashMultList<T>(IEnumerable<string> keys, int? top = null)
        {
            throw new NotImplementedException();
        }

        public bool HashExists(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public bool HashDelete(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public bool HashDelete(string key, List<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public bool HashRangeDelete(List<BaseRequest<List<string>>> request)
        {
            throw new NotImplementedException();
        }

        public long HashIncrement(string key, string hashKey, long val = 1)
        {
            throw new NotImplementedException();
        }

        public long HashDecrement(string key, string hashKey, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashSetAsync(string key, string hashKey, string value, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashSetAsync(MultHashRequest<HashRequest<string>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashSetAsync(List<MultHashRequest<HashRequest<string>>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashSetAsync<T>(string key, string hashKey, T value, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashSetAsync<T>(MultHashRequest<HashRequest<T>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashSetAsync<T>(List<MultHashRequest<HashRequest<T>>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> HashGetAsync(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashResponse<string>>> HashGetAsync(string key, List<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashMultResponse<string>>> HashGetAsync(List<MultHashRequest<string>> list)
        {
            throw new NotImplementedException();
        }

        public Task<T> HashGetAsync<T>(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashResponse<T>>> HashGetAsync<T>(string key, List<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashMultResponse<T>>> HashGetAsync<T>(List<MultHashRequest<string>> list)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> HashKeyListAsync(string key, int? top = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashResponse<string>>> HashListAsync(string key, int? top = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashResponse<T>>> HashListAsync<T>(string key, int? top = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashMultResponse<string>>> HashMultListAsync(IEnumerable<string> keys, int? top = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashMultResponse<T>>> HashMultListAsync<T>(IEnumerable<string> keys, int? top = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashExistsAsync(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashDeleteAsync(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashDeleteAsync(string key, List<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashRangeDeleteAsync(List<BaseRequest<List<string>>> request)
        {
            throw new NotImplementedException();
        }

        public Task<long> HashIncrementAsync(string key, string hashKey, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<long> HashDecrementAsync(string key, string hashKey, long val = 1)
        {
            throw new NotImplementedException();
        }

        public bool SortedSet(string key, string value, double score)
        {
            throw new NotImplementedException();
        }

        public bool SortedSet<T>(string key, T value, double score)
        {
            throw new NotImplementedException();
        }

        public bool SortedSetRemove(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool SortedSetRemove<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public List<string> SortedSetRangeByRank(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public List<T> SortedSetRangeByRank<T>(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public List<string> SortedSetRangeFrom(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public List<T> SortedSetRangeFrom<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public bool SortedSetExist<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public long SortedSetLength(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetAsync(string key, string value, double score)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetAsync<T>(string key, T value, double score)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetRemoveAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> SortedSetRangeByRankAsync(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> SortedSetRangeByRankAsync<T>(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> SortedSetRangeFromAsync(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> SortedSetRangeFromAsync<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetExistAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<long> SortedSetLengthAsync(string key)
        {
            throw new NotImplementedException();
        }

        public long ListRightPush(string key, string value)
        {
            throw new NotImplementedException();
        }

        public long ListRightPush<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public string ListRightPop(string key)
        {
            throw new NotImplementedException();
        }

        public T ListRightPop<T>(string key)
        {
            throw new NotImplementedException();
        }

        public long ListLeftPush(string key, string value)
        {
            throw new NotImplementedException();
        }

        public long ListLeftPush<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public string ListLeftPop(string key)
        {
            throw new NotImplementedException();
        }

        public T ListLeftPop<T>(string key)
        {
            throw new NotImplementedException();
        }

        public List<string> ListLeftRange(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public List<T> ListLeftRange<T>(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public List<string> ListRightRange(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public List<T> ListRightRange<T>(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public long ListRemove(string key, string value)
        {
            throw new NotImplementedException();
        }

        public long ListRemove<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public long ListLength(string key)
        {
            throw new NotImplementedException();
        }

        public Task<long> ListRightPushAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<long> ListRightPushAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<string> ListRightPopAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> ListRightPopAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<long> ListLeftPushAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<string> ListLeftPopAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> ListLeftPopAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ListLeftRangeAsync(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListLeftRangeAsync<T>(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ListRightRangeAsync(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListRightRangeAsync<T>(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<long> ListRemoveAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<long> ListRemoveAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<long> ListLengthAsync(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置缓存键值对集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool Set(IEnumerable<BaseRequest<string>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(string key, T obj, TimeSpan? expiry = null, PersistentOps persistentOps = null);

        /// <summary>
        /// 保存多个对象集合
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(IEnumerable<BaseRequest<T>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null);

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
        List<BaseResponse<string>> Get(IEnumerable<string> keys);

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
        List<BaseResponse<T>> Get<T>(IEnumerable<string> keys);

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
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        bool SetExpire(string key, TimeSpan expiry, PersistentOps persistentOps = null);

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        bool Remove(string key);

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        bool RemoveRange(IEnumerable<string> keys);

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key
        /// 默认得到全部的键
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        List<string> Keys(string pattern = "*");

        #endregion

        #region 异步

        /// <summary>
        /// 设置缓存（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        Task<bool> SetAsync(
            string key,
            string value,
            TimeSpan? expiry = null,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 设置缓存键值对集合(异步)
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        Task<bool> SetAsync(List<BaseRequest<string>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 保存一个对象(异步)
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="obj">缓存值</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(
            string key,
            T obj,
            TimeSpan? expiry = null,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 保存多个对象集合(异步)
        /// </summary>
        /// <param name="list">缓存键值对集合</param>
        /// <param name="expiry">过期时间，null：永不过期</param>
        /// <param name="persistentOps">策略</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(List<BaseRequest<T>> list, TimeSpan? expiry = null,
            PersistentOps persistentOps = null);

        /// <summary>
        /// 获取单个key的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        Task<List<BaseResponse<string>>> GetAsync(IEnumerable<string> keys);

        /// <summary>
        /// 获取指定缓存的值（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key) where T : class, new();

        /// <summary>
        /// 获取多组缓存键集合（异步）
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        /// <returns></returns>
        Task<List<BaseResponse<T>>> GetAsync<T>(IEnumerable<string> keys) where T : class, new();

        /// <summary>
        /// 为数字增长val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">增加的值</param>
        /// <returns></returns>
        Task<long> IncrementAsync(string key, long val = 1);

        /// <summary>
        /// 为数字减少val（异步）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">减少的值</param>
        /// <returns></returns>
        Task<long> DecrementAsync(string key, long val = 1);

        /// <summary>
        /// 检查指定的缓存key是否存在（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<bool> ExistAsync(string key);

        /// <summary>
        /// 设置过期时间（异步）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        Task<bool> SetExpireAsync(string key, TimeSpan expiry, PersistentOps persistentOps = null);

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>返回删除的数量</returns>
        Task<bool> RemoveAsync(string key);

        /// <summary>
        /// 删除指定Key的缓存（异步）
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        Task<bool> RemoveRangeAsync(List<string> keys);

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key（异步）
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        Task<List<string>> KeysAsync(string pattern = "*");

        #endregion
    }
}
