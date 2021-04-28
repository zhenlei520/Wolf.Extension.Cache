// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Abstractions.Response.SortedSet
{
    /// <summary>
    ///
    /// </summary>
    public class SortedSetResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        public SortedSetResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="score">分值</param>
        /// <param name="data">值</param>
        public SortedSetResponse(decimal score, T data) : this()
        {
            Score = score;
            Data = data;
        }

        /// <summary>
        /// 分数
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
