// Copyright (c) zhenlei520 All rights reserved.

using Wolf.Extension.Cache.Abstractions.Enum;

namespace Wolf.Extension.Cache.Abstractions.Configurations
{
    /// <summary>
    /// list键 策略
    /// </summary>
    public class ListPersistentOps
    {
        /// <summary>
        ///
        /// </summary>
        public ListPersistentOps()
        {
            this.SetStrategy = null;
        }

        /// <summary>
        /// 设置策略，默认全部设置，不区分缓存是否存在
        /// </summary>
        public SetStrategy? SetStrategy { get; set; }
    }
}
