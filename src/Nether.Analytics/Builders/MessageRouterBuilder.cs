// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nether.Analytics
{
    public class MessageRouterBuilder<ParsedMessageType> where ParsedMessageType : IKnownMessageType
    {
        private List<IMessageHandler<ParsedMessageType>> _messageHandlers = new List<IMessageHandler<ParsedMessageType>>();
        private List<EventPipelineBuilder<ParsedMessageType>> _eventPipelineBuilders = new List<EventPipelineBuilder<ParsedMessageType>>();
        private EventPipelineBuilder<ParsedMessageType> _unhandledEventBuilder;

        public MessageRouterBuilder()
        {
        }

        public EventPipelineBuilder<ParsedMessageType> Event(string eventName)
        {
            var builder = new EventPipelineBuilder<ParsedMessageType>(eventName);
            _eventPipelineBuilders.Add(builder);

            return builder;
        }

        public MessageRouterBuilder<ParsedMessageType> AddMessageHandler(IMessageHandler<ParsedMessageType> messageHandler)
        {
            _messageHandlers.Add(messageHandler);

            return this;
        }

        public EventPipelineBuilder<ParsedMessageType> UnhandledEvent()
        {
            _unhandledEventBuilder = new EventPipelineBuilder<ParsedMessageType>(null);

            return _unhandledEventBuilder;
        }

        public GameEventRouter<ParsedMessageType> Build()
        {
            var unhandledEventPipeline = _unhandledEventBuilder.Build(_messageHandlers);
            var eventPipelines = _eventPipelineBuilders.Select(p => p.Build(_messageHandlers)).ToDictionary(p => p.MessageType);

            return new GameEventRouter<ParsedMessageType>(eventPipelines, unhandledEventPipeline);
        }
    }
}