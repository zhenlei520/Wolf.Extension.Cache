// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// set 存储异步
    /// </summary>
    public partial class CacheProvider
    {
        public Task<bool> SetAddAsync(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetAddAsync<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetAddRangeAsync(string key, params string[] values)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetAddRangeAsync<T>(string key, params T[] values)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> SetLengthAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<string[]> SetDiffAsync(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<T[]> SetDiffAsync<T>(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetDiffStoreAsync(string destination, params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<string>> SetInterAsync(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<T>> SetInterAsync<T>(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetInterStoreAsync(string destination, params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetExistsAsync(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetExistsAsync<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<string>> SetGetAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<T>> SetGetAsync<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetMoveAsync(string key, string optKey, string value)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetMoveAsync<T>(string key, string optKey, T value)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> SetPopAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> SetPopAsync<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<string[]> SetPopAsync(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public Task<T[]> SetPopAsync<T>(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> SetRandomGetAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> SetRandomGetAsync<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<string[]> SetRandomGetAsync(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public Task<T[]> SetRandomGetAsync<T>(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> SetRemAsync(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> SetRemAsync(string key, params string[] values)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> SetRemAsync<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> SetRemAsync<T>(string key, params T[] values)
        {
            throw new System.NotImplementedException();
        }

        public Task<string[]> SetUnionAsync(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<T[]> SetUnionAsync<T>(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> SetUnionStoreAsync(string optKey, params string[] keys)
        {
            throw new System.NotImplementedException();
        }
    }
}
