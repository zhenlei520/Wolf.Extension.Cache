using Wolf.Extension.Cache.Redis.Common.Internal.IO;

namespace Wolf.Extension.Cache.Redis.Common.Internal.Commands
{
    class RedisObject : RedisCommand<object>
    {
        public RedisObject(string command, params object[] args)
            : base(command, args)
        { }

        public override object Parse(RedisReader reader)
        {
            return reader.Read();
        }

        public class Strings : RedisCommand<object>
        {
            public Strings(string command, params object[] args)
                : base(command, args)
            { }

            public override object Parse(RedisReader reader)
            {
                return reader.Read(true);
            }
        }
    }
}
