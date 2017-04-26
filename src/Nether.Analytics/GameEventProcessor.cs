// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nether.Analytics.Listeners;
using Nether.Analytics.Parsers;
using System;
using System.Collections.Generic;

//KEEP

namespace Nether.Analytics
{
    public class GameEventProcessor<RawMessageFormat, ParsedMessageFormat>
    {
        public GameEventProcessor(IMessageListener<RawMessageFormat> listner, IMessageParser<RawMessageFormat, ParsedMessageFormat> parser, IMessageRouter<ParsedMessageFormat> router)
        {
            Listener = listner;
            Parser = parser;
            Router = router;
        }

        public IMessageListener<RawMessageFormat> Listener { get; set; }
        public IMessageParser<RawMessageFormat, ParsedMessageFormat> Parser { get; set; }
        public IMessageRouter<ParsedMessageFormat> Router { get; set; }

        public void ProcessAndBlock()
        {
            //Listener.StartAsync(HandleUnparsedMessages);
        }

        private void HandleUnparsedMessages(IEnumerable<EventHubListenerMessage> obj)
        {
            
        }
    }
}