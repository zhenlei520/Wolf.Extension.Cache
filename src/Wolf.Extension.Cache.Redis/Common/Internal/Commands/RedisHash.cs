﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Wolf.Extension.Cache.Redis.Common.Internal.IO;
using Wolf.Extension.Cache.Redis.Common.Internal.Utilities;

namespace Wolf.Extension.Cache.Redis.Common.Internal.Commands
{
    class RedisHash : RedisCommand<Dictionary<string, string>>
    {
        public RedisHash(string command, params object[] args)
            : base(command, args)
        { }

        public override Dictionary<string, string> Parse(RedisReader reader)
        {
            return ToDict(reader);
        }

        static Dictionary<string, string> ToDict(RedisReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            long count = reader.ReadInt(false);
            var dict = new Dictionary<string, string>();
            string key = String.Empty;
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                    key = reader.ReadBulkString();
                else
                    dict[key] = reader.ReadBulkString();
            }
            return dict;
        }

        public class Generic<T> : RedisCommand<T>
            where T : class
        {
            public Generic(string command, params object[] args)
                : base(command, args)
            { }

            public override T Parse(RedisReader reader)
            {
                return Serializer<T>.Deserialize(ToDict(reader));
            }
        }
    }
}
