// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Wolf.Extension.Cache.MemoryCache
{
    /// <summary>
    /// set 存储
    /// </summary>
    public partial class CacheProvider
    {
        public bool SetAdd(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public bool SetAdd<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public bool SetAddRange(string key, params string[] values)
        {
            throw new System.NotImplementedException();
        }

        public bool SetAddRange<T>(string key, params T[] values)
        {
            throw new System.NotImplementedException();
        }

        public long SetLength(string key)
        {
            throw new System.NotImplementedException();
        }

        public string[] SetDiff(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public T[] SetDiff<T>(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public bool SetDiffStore(string destination, params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public List<string> SetInter(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public List<T> SetInter<T>(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public bool SetInterStore(string destination, params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public bool SetExists(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public bool SetExists<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public List<string> SetGet(string key)
        {
            throw new System.NotImplementedException();
        }

        public List<T> SetGet<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool SetMove(string key, string optKey, string value)
        {
            throw new System.NotImplementedException();
        }

        public bool SetMove<T>(string key, string optKey, T value)
        {
            throw new System.NotImplementedException();
        }

        public string SetPop(string key)
        {
            throw new System.NotImplementedException();
        }

        public T SetPop<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public string[] SetPop(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public T[] SetPop<T>(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public string SetRandomGet(string key)
        {
            throw new System.NotImplementedException();
        }

        public T SetRandomGet<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public string[] SetRandomGet(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public T[] SetRandomGet<T>(string key, int count)
        {
            throw new System.NotImplementedException();
        }

        public long SetRem(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public long SetRem(string key, params string[] values)
        {
            throw new System.NotImplementedException();
        }

        public long SetRem<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public long SetRem<T>(string key, params T[] values)
        {
            throw new System.NotImplementedException();
        }

        public string[] SetUnion(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public T[] SetUnion<T>(params string[] keys)
        {
            throw new System.NotImplementedException();
        }

        public long SetUnionStore(string optKey, params string[] keys)
        {
            throw new System.NotImplementedException();
        }
    }
}
