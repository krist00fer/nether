// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Nether.Analytics.EventProcessor.Host
{
    internal class NetherEventRouter
    {
        public NetherEventRouter()
        {
            GameEventHandlers = new Dictionary<string, object>();
        }

        public Dictionary<string, object> GameEventHandlers { get; internal set; }
        public PassThroughGameEventHandler UnknownGameEventHandler { get; internal set; }
    }
}