// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.Extension.Cache.Redis.Common.Internal.IO;

namespace Wolf.Extension.Cache.Redis.Common.Internal.Commands
{
    class RedisBool : RedisCommand<bool>
    {
        public RedisBool(string command, params object[] args)
            : base(command, args)
        { }

        public override bool Parse(RedisReader reader)
        {
            return reader.ReadInt() == 1;
        }
    }
}
