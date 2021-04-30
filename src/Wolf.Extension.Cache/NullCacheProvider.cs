// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf.Extension.Cache.Abstractions;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.Abstractions.Request.Hash;
using Wolf.Extension.Cache.Abstractions.Request.SortedSet;
using Wolf.Extension.Cache.Abstractions.Response.Base;
using Wolf.Extension.Cache.Abstractions.Response.Hash;
using Wolf.Extension.Cache.Abstractions.Response.SortedSet;

namespace Wolf.Extension.Cache
{
    /// <summary>
    /// 空的缓存提供者
    /// </summary>
    public class NullCacheProvider : ICacheProvider
    {
        public bool SortedSet(string key, string value, decimal score)
        {
            throw new NotImplementedException();
        }

        public bool SortedSet(string key, params SortedSetRequest<string>[] request)
        {
            throw new NotImplementedException();
        }

        public bool SortedSet<T>(string key, T value, decimal score)
        {
            throw new NotImplementedException();
        }

        public bool SortedSet<T>(string key, params SortedSetRequest<T>[] request)
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

        public bool SortedSetRemoveByRank(string key, int fromRank, int toRank)
        {
            throw new NotImplementedException();
        }

        public bool SortedSetRemoveByScore(string key, decimal min, decimal max)
        {
            throw new NotImplementedException();
        }

        public string[] SortedSetRangeByRank(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public T[] SortedSetRangeByRank<T>(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public string[] SortedSetRangeFrom(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public T[] SortedSetRangeFrom<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public List<SortedSetResponse<string>> SortedSetRangeWithScoresFrom(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public List<SortedSetResponse<T>> SortedSetRangeWithScoresFrom<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public string[] SortedSetRangeByScore(string key, decimal min, decimal max, int skip = 0, int count = -1, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public T[] SortedSetRangeByScore<T>(string key, decimal min, decimal max, int skip = 0, int count = -1, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public List<SortedSetResponse<string>> SortedSetRangeByScoreWithScores(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public List<SortedSetResponse<T>> SortedSetRangeByScoreWithScores<T>(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public long? SortedSetIndex(string key, string value, bool isDesc)
        {
            throw new NotImplementedException();
        }

        public long? SortedSetIndex<T>(string key, T value, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public bool SortedSetExist(string key, string value)
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

        public long SortedSetLength(string key, decimal min, decimal max)
        {
            throw new NotImplementedException();
        }

        public decimal SortedSetIncrement(string key, string value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public decimal SortedSetIncrement<T>(string key, T value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public decimal SortedSetDecrement(string key, string value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public decimal SortedSetDecrement<T>(string key, T value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetAsync(string key, string value, decimal score)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetAsync(string key, params SortedSetRequest<string>[] request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetAsync<T>(string key, T value, decimal score)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetAsync<T>(string key, params SortedSetRequest<T>[] request)
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

        public Task<bool> SortedSetRemoveByRankAsync(string key, int fromRank, int toRank)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetRemoveByScoreAsync(string key, decimal min, decimal max)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> SortedSetRangeByRankAsync(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> SortedSetRangeByRankAsync<T>(string key, int count = 1000, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> SortedSetRangeFromAsync(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> SortedSetRangeFromAsync<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<SortedSetResponse<string>>> SortedSetRangeWithScoresFromAsync(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<SortedSetResponse<T>>> SortedSetRangeWithScoresFromAsync<T>(string key, int fromRank, int toRank, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> SortedSetRangeByScoreAsync(string key, decimal min, decimal max, int skip = 0, int count = -1, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> SortedSetRangeByScoreAsync<T>(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<SortedSetResponse<string>>> SortedSetRangeByScoreWithScoresAsync(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<SortedSetResponse<T>>> SortedSetRangeByScoreWithScoresAsync<T>(string key, decimal min, decimal max, int skip = 0, int count = -1,
            bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<long?> SortedSetIndexAsync(string key, string value, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<long?> SortedSetIndexAsync<T>(string key, T value, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SortedSetExistAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<long?> SortedSetIndexAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<long?> SortedSetIndexAsync<T>(string key, T value)
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

        public Task<long> SortedSetLengthAsync(string key, decimal min, decimal max)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> SortedSetIncrementAsync(string key, string value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> SortedSetIncrementAsync<T>(string key, T value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> SortedSetDecrementAsync(string key, string value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> SortedSetDecrementAsync<T>(string key, T value, long val = 1)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, string value, BasePersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool Set(ICollection<BaseRequest<string>> list, PersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T obj, BasePersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(ICollection<BaseRequest<T>> list, PersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public List<BaseResponse<string>> Get(ICollection<string> keys)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public List<BaseResponse<T>> Get<T>(ICollection<string> keys)
        {
            throw new NotImplementedException();
        }

        public long Increment(string key, long val = 1)
        {
            throw new NotImplementedException();
        }

        public long Decrement(string key, long val = 1)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string key)
        {
            throw new NotImplementedException();
        }

        public bool SetExpire(string key, TimeSpan timeSpan, OverdueStrategy strategy = OverdueStrategy.AbsoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public long Remove(string key)
        {
            throw new NotImplementedException();
        }

        public long RemoveRange(ICollection<string> keys)
        {
            throw new NotImplementedException();
        }

        public List<string> Keys(string pattern = "*")
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string value, BasePersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(List<BaseRequest<string>> list, PersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T obj, BasePersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(List<BaseRequest<T>> list, PersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<BaseResponse<string>>> GetAsync(ICollection<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<BaseResponse<T>>> GetAsync<T>(ICollection<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<long> IncrementAsync(string key, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<long> DecrementAsync(string key, long val = 1)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetExpireAsync(string key, TimeSpan timeSpan, OverdueStrategy strategy = OverdueStrategy.AbsoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public Task<long> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<long> RemoveRangeAsync(List<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> KeysAsync(string pattern = "*")
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

        public Task<List<HashResponse<T>>> HashGetAsync<T>(string key, ICollection<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashMultResponse<T>>> HashGetAsync<T>(ICollection<MultHashRequest<string>> list)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> HashKeyListAsync(string key, int top = -1)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashResponse<string>>> HashListAsync(string key, int top = -1)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashResponse<T>>> HashListAsync<T>(string key, int top = -1)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashMultResponse<string>>> HashMultListAsync(ICollection<string> keys, int top = -1)
        {
            throw new NotImplementedException();
        }

        public Task<List<HashMultResponse<T>>> HashMultListAsync<T>(ICollection<string> keys, int top = -1)
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

        public Task<bool> HashDeleteAsync(string key, ICollection<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashRangeDeleteAsync(ICollection<BaseRequest<List<string>>> request)
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

        public string[] ListLeftRange(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public T[] ListLeftRange<T>(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public string[] ListRightRange(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public T[] ListRightRange<T>(string key, int count = 1000)
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

        public Task<string[]> ListLeftRangeAsync(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> ListLeftRangeAsync<T>(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> ListRightRangeAsync(string key, int count = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> ListRightRangeAsync<T>(string key, int count = 1000)
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

        public bool HashSet<T>(ICollection<MultHashRequest<HashRequest<T>>> request, HashPersistentOps persistentOps = null)
        {
            throw new NotImplementedException();
        }

        public string HashGet(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<string>> HashGet(string key, ICollection<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<string>> HashGet(ICollection<MultHashRequest<string>> list)
        {
            throw new NotImplementedException();
        }

        public T HashGet<T>(string key, string hashKey)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<T>> HashGet<T>(string key, ICollection<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<T>> HashGet<T>(ICollection<MultHashRequest<string>> list)
        {
            throw new NotImplementedException();
        }

        public List<string> HashKeyList(string key, int top = -1)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<string>> HashList(string key, int top = -1)
        {
            throw new NotImplementedException();
        }

        public List<HashResponse<T>> HashList<T>(string key, int top = -1)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<string>> HashMultList(ICollection<string> keys, int top = -1)
        {
            throw new NotImplementedException();
        }

        public List<HashMultResponse<T>> HashMultList<T>(ICollection<string> keys, int top = -1)
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

        public bool HashDelete(string key, ICollection<string> hashKeys)
        {
            throw new NotImplementedException();
        }

        public bool HashRangeDelete(ICollection<BaseRequest<List<string>>> request)
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
    }
}
