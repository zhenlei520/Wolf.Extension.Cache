// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.Extension.Cache.Redis.Common.Internal.IO;

namespace Wolf.Extension.Cache.Redis.Common.Internal.Commands
{
    class RedisInt : RedisCommand<long>
    {
        public RedisInt(string command, params object[] args)
            : base(command, args)
        { }

        public override long Parse(RedisReader reader)
        {
            return reader.ReadInt();
        }

        public class Nullable : RedisCommand<long?>
        {
            public Nullable(string command, params object[] args)
                : base(command, args)
            { }


            public override long? Parse(RedisReader reader)
            {
                RedisMessage type = reader.ReadType();
                if (type == RedisMessage.Int)
                    return reader.ReadInt(false);
                reader.ReadBulkString(false);
                return null;
            }
        }
    }
}
