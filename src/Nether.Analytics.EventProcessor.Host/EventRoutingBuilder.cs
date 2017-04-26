// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nether.Analytics.EventProcessor.Host
{
    internal class EventRoutingBuilder
    {
        private List<GameEventHandler> _globalHandlers = new List<GameEventHandler>();
        private List<EventPipelineBuilder> _eventPipelineBuilders = new List<EventPipelineBuilder>();
        private EventPipelineBuilder _unhandledEventBuilder;

        public EventRoutingBuilder()
        {
        }

        internal EventPipelineBuilder Event(string eventName)
        {
            var builder = new EventPipelineBuilder(eventName);
            _eventPipelineBuilders.Add(builder);

            return builder;
        }

        internal EventRoutingBuilder AddHandler(GameEventHandler eventHandler)
        {
            _globalHandlers.Add(eventHandler);

            return this;
        }

        internal EventPipelineBuilder UnhandledEvent()
        {
            _unhandledEventBuilder = new EventPipelineBuilder(null);

            return _unhandledEventBuilder;
        }

        internal GameEventRouter Build()
        {
            var unhandledEventPipeline = _unhandledEventBuilder.Build(_globalHandlers);
            var eventPipelines = _eventPipelineBuilders.Select(p => p.Build(_globalHandlers)).ToDictionary(p => p.MessageType);

            return new GameEventRouter(eventPipelines, unhandledEventPipeline);
        }
    }

    internal class GameEventRouter
    {
        private Dictionary<string, EventPipeline> _eventPipelines;
        private EventPipeline _unhandledEventPipeline;

        public GameEventRouter(Dictionary<string, EventPipeline> eventPipelines, EventPipeline unhandledEventPipeline)
        {
            _eventPipelines = eventPipelines;
            _unhandledEventPipeline = unhandledEventPipeline;
        }

        public void RouteMessage(GameMessage msg)
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

    internal class EventPipeline
    {
        private GameEventHandler[] _gameEventHandlers;
        private OutputManager[] _outputManagers;

        public EventPipeline(string messageType, GameEventHandler[] gameEventHandlers, OutputManager[] outputManagers)
        {
            MessageType = messageType;
            _gameEventHandlers = gameEventHandlers;
            _outputManagers = outputManagers;
        }

        internal void ProcessMessage(GameMessage msg)
        {
            foreach (var handler in _gameEventHandlers)
            {
                var result = handler.ProcessMessage(msg);
                if (result.StopProcessing)
                {
                    return;
                }
            }
            foreach (var outputManager in _outputManagers)
            {
                outputManager.OutputMessage(msg);
            }
        }

        public string MessageType { get; private set; }
    }

    public class GameMessage
    {
        public string MessageType { get; internal set; }
    }

    internal class EventPipelineBuilder
    {
        private string _eventName;
        private List<GameEventHandler> _handlers = new List<GameEventHandler>();
        private OutputManager[] _outputManagers;

        public EventPipelineBuilder(string eventName)
        {
            _eventName = eventName;
        }

        internal EventPipelineBuilder AddHandler(GameEventHandler eventHandler)
        {
            _handlers.Add(eventHandler);

            return this;
        }

        internal EventPipeline Build(List<GameEventHandler> globalHandlers)
        {
            return new EventPipeline(_eventName, globalHandlers.Concat(_handlers).ToArray(), _outputManagers);
        }

        internal void OutputTo(params OutputManager[] outputManagers)
        {
            _outputManagers = outputManagers;
        }
    }

    //GameEventHandler h1 = null, h2 = null, h3 = null;

    //h2.ProcessMessage(msg, h3.ProcessMessage);
    //h2.ProcessMessage(msg, (m,n) => h3.ProcessMessage(m, n));
    //h1.ProcessMessage(msg, (m, n) => h2.ProcessMessage(m, n));




    //MessageHandler nullHandler = (m, n) => { };
    //Action<GameMessage> handler = message => h1.ProcessMessage(msg,
    //    (m2,n2) => h2.ProcessMessage(m2,
    //        (m, n) => h3.ProcessMessage(m, nullHandler)
    //    )
    //);

    //public delegate void MessageHandler(GameMessage message, MessageHandler next);
    //internal abstract class GameEventHandler
    //{
    //    //public abstract void ProcessMessage(GameMessage message, GameEventHandler next);
    //    public abstract void ProcessMessage(GameMessage message, MessageHandler next);
    //}

    //internal abstract class GameEventHandler2
    //{
    //    private readonly GameEventHandler2 next;

    //    public GameEventHandler2(GameEventHandler2 next)
    //    {
    //        this.next = next;
    //    }
    //    public abstract void ProcessMessage(GameMessage message);
    //}


    internal abstract class GameEventHandler
    {
        public abstract GameHandlerResult ProcessMessage(GameMessage message);
    }

    internal class GameHandlerResult
    {
        public bool StopProcessing { get; set; }
    }
}