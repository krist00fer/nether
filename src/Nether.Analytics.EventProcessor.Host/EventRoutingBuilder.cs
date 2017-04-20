using System;
using System.Collections.Generic;
using System.Linq;

namespace Nether.Analytics.EventProcessor.Host
{
    internal class EventRoutingBuilder
    {
        private List<GameEventHandler> globalHandlers = new List<GameEventHandler>();
        private List<EventPipelineBuilder> eventPipelineBuilders = new List<EventPipelineBuilder>();
        EventPipelineBuilder unhandledEventBuilder;

        public EventRoutingBuilder()
        {
        }

        internal EventPipelineBuilder Event(string eventName)
        {
            var builder = new EventPipelineBuilder(eventName);
            eventPipelineBuilders.Add(builder);

            return builder;
        }

        internal EventRoutingBuilder AddHandler(GameEventHandler eventHandler)
        {
            this.globalHandlers.Add(eventHandler);

            return this;
        }

        internal EventPipelineBuilder UnhandledEvent()
        {
            unhandledEventBuilder = new EventPipelineBuilder(null);

            return unhandledEventBuilder;
        }

        internal GameEventRouter Build()
        {
            var unhandledEventPipeline = unhandledEventBuilder.Build(globalHandlers);
            var eventPipelines = eventPipelineBuilders.Select(p => p.Build(globalHandlers)).ToDictionary(p => p.MessageType);

            return new GameEventRouter(eventPipelines, unhandledEventPipeline);
        }
    }

    internal class GameEventRouter
    {
        private Dictionary<string, EventPipeline> eventPipelines;
        private EventPipeline unhandledEventPipeline;

        public GameEventRouter(Dictionary<string, EventPipeline> eventPipelines, EventPipeline unhandledEventPipeline)
        {
            this.eventPipelines = eventPipelines;
            this.unhandledEventPipeline = unhandledEventPipeline;
        }

        public void RouteMessage(GameMessage msg)
        {
            if (eventPipelines.TryGetValue(msg.MessageType, out var pipeline))
            {
                pipeline.ProcessMessage(msg);
            }
            else
            {
                unhandledEventPipeline?.ProcessMessage(msg);
            }
        }
    }

    internal class EventPipeline
    {
        private GameEventHandler[] gameEventHandler;
        private OutputManager[] outputManagers;

        public EventPipeline(string messageType, GameEventHandler[] gameEventHandler, OutputManager[] outputManagers)
        {
            MessageType = messageType;
            this.gameEventHandler = gameEventHandler;
            this.outputManagers = outputManagers;
        }

        internal void ProcessMessage(GameMessage msg)
        {

            throw new NotImplementedException();
        }

        public string MessageType { get; private set; }
    }

    public class GameMessage
    {
        public string MessageType { get; internal set; }
    }

    internal class EventPipelineBuilder
    {
        private string eventName;
        private List<GameEventHandler> handlers;
        private OutputManager[] outputManagers;

        public EventPipelineBuilder(string eventName)
        {
            this.eventName = eventName;
        }

        internal EventPipelineBuilder AddHandler(GameEventHandler eventHandler)
        {
            this.handlers.Add(eventHandler);

            return this;
        }

        internal EventPipeline Build(List<GameEventHandler> globalHandlers)
        {
            return new EventPipeline(eventName, globalHandlers.Concat(handlers).ToArray(), outputManagers);
        }

        internal void OutputTo(params OutputManager[] outputManagers)
        {
            this.outputManagers = outputManagers;
        }
    }

    internal class GameEventHandler
    {
    }
}