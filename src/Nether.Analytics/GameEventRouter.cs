// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

using System;
using System.Collections.Generic;

namespace Nether.Analytics
{
    public class GameEventRouter<ParsedMessageType> : IMessageRouter<ParsedMessageType> where ParsedMessageType : IKnownMessageType
    {
        private Dictionary<string, GameEventPipeline<ParsedMessageType>> _eventPipelines;
        private GameEventPipeline<ParsedMessageType> _unhandledEventPipeline;

        public GameEventRouter(Dictionary<string, GameEventPipeline<ParsedMessageType>> eventPipelines, GameEventPipeline<ParsedMessageType> unhandledEventPipeline)
        {
            _eventPipelines = eventPipelines;
            _unhandledEventPipeline = unhandledEventPipeline;
        }

        public void RouteMessage(ParsedMessageType msg)
        {
            if (_eventPipelines.TryGetValue(msg.MessageType, out var pipeline))
            {
                pipeline.ProcessMessage(msg);
            }
            else
            {
                _unhandledEventPipeline?.ProcessMessage(msg);
            }
        }
    }
}