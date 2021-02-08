// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;

namespace Wolf.Extension.Cache.MemoryCache.Internal
{
    /// <summary>
    ///
    /// </summary>
    public class SortSetRequest<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public double Score { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortSetRequestComparer<T> : IComparer<SortSetRequest<T>>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(SortSetRequest<T> x, SortSetRequest<T> y)
        {
            if (x == null)
            {
                x = new SortSetRequest<T>();
            }

            if (y == null)
            {
                y = new SortSetRequest<T>();
            }

            var res = x.Score.CompareTo(y.Score);
            if (res == 0 && x.Data.Equals(y.Data))
            {
                return 0;
            }

            return res;
        }
    }
}
