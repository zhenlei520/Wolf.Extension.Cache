﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace Wolf.Extension.Cache.Redis.Common.Internal
{
    class MonitorListener : RedisListner<object>
    {
        public event EventHandler<RedisMonitorEventArgs> MonitorReceived;

        public MonitorListener(RedisConnector connection)
            : base(connection)
        { }

        public string Start()
        {
            string status = Call(RedisCommands.Monitor());
            Listen(x => x.Read());
            return status;
        }

        protected override void OnParsed(object value)
        {
            OnMonitorReceived(value);
        }

        protected override bool Continue()
        {
            return Connection.IsConnected;
        }

        void OnMonitorReceived(object message)
        {
            if (MonitorReceived != null)
                MonitorReceived(this, new RedisMonitorEventArgs(message));
        }
    }
}