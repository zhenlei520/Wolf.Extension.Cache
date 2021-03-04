// Copyright (c) zhenlei520 All rights reserved.

using System;
using Wolf.Extension.Cache.Abstractions.Enum;

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// 基本缓存策略
    /// </summary>
    public class BasePersistentOps
    {
        /// <summary>
        ///
        /// </summary>
        private BasePersistentOps()
        {
            this.OverdueTimeSpan=TimeSpan.Zero;
            this.SetStrategy = null;
        }

        /// <summary>
        /// 默认绝对过期，不确保原子性
        /// </summary>
        /// <param name="overdueTime">过期时间，单位：ms，永不过期：null，不支持负数</param>
        public BasePersistentOps(long? overdueTime = null) : this()
        {
            this.Strategy = OverdueStrategy.AbsoluteExpiration;

            if (overdueTime != null)
            {
                if (overdueTime < 0)
                {
                    throw new NotSupportedException(nameof(overdueTime));
                }

                this.OverdueTimeSpan = TimeSpan.FromMilliseconds(overdueTime.Value);
            }
        }

        /// <summary>
        /// 默认绝对过期，不确保原子性
        /// </summary>
        /// <param name="overdueTime">过期时间，永不过期：null，不得小于TimeSpan.Zero</param>
        public BasePersistentOps(TimeSpan overdueTime) : this()
        {
            this.Strategy = OverdueStrategy.AbsoluteExpiration;

            if (overdueTime < TimeSpan.Zero)
            {
                throw new NotSupportedException(nameof(overdueTime));
            }

            this.OverdueTimeSpan = overdueTime;
        }

        /// <summary>
        /// 过期策略
        /// </summary>
        public OverdueStrategy Strategy { get; set; }

        /// <summary>
        /// 过期时间，永久保存：TimeSpan.Zero
        /// </summary>
        public TimeSpan OverdueTimeSpan { get; private set; }

        /// <summary>
        /// 设置策略，默认全部设置，不区分缓存是否存在
        /// </summary>
        public SetStrategy? SetStrategy { get; set; }
    }
}
