// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nether.Analytics
{
    public class GameEventRouterBuilder
    {
        private List<GameEventHandler> _globalHandlers = new List<GameEventHandler>();
        private List<GameEventPipelineBuilder> _eventPipelineBuilders = new List<GameEventPipelineBuilder>();
        private GameEventPipelineBuilder _unhandledEventBuilder;

        public GameEventRouterBuilder()
        {
        }

        public GameEventPipelineBuilder Event(string eventName)
        {
            var builder = new GameEventPipelineBuilder(eventName);
            _eventPipelineBuilders.Add(builder);

            return builder;
        }

        public GameEventRouterBuilder AddHandler(GameEventHandler eventHandler)
        {
            _globalHandlers.Add(eventHandler);

            return this;
        }

        public GameEventPipelineBuilder UnhandledEvent()
        {
            _unhandledEventBuilder = new GameEventPipelineBuilder(null);

            return _unhandledEventBuilder;
        }

        public GameEventRouter Build()
        {
            var unhandledEventPipeline = _unhandledEventBuilder.Build(_globalHandlers);
            var eventPipelines = _eventPipelineBuilders.Select(p => p.Build(_globalHandlers)).ToDictionary(p => p.MessageType);

            return new GameEventRouter(eventPipelines, unhandledEventPipeline);
        }
    }
}