// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nether.Analytics.Inputs.EventHubs;
using System;

namespace Nether.Analytics
{
    public class GameEventProcessor
    {
        public GameEventProcessor(EventHubListener listner, GameEventParserBase parser, GameEventRouter router)
        {
            listner.RegisterMessageHandler = ProcessAndBlock;
            router.RouteMessage(parser.ParseMessage())
        }

        public GameEventListener Listener { get; set; }
        public GameEventParserBase Parser { get; set; }
        public GameEventRouter Pipeline { get; set; }

        public void ProcessAndBlock()
        {
            Listener.StartListening();
        }
    }
}