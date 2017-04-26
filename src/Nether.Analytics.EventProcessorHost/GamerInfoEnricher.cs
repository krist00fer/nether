// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

using Nether.Analytics.Parsers;
using System;

namespace Nether.Analytics.EventProcessorHost
{
    public class GamerInfoEnricher : IMessageHandler<SimpleMessage>
    {
        public MessageHandlerResluts ProcessMessage(SimpleMessage message)
        {
            message.Properties.Add("Greeting", "Event was enriched");

            return new MessageHandlerResluts { StopProcessing = false };
        }
    }
}