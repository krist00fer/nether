using System;
using System.Collections.Generic;
using System.Linq;

namespace Nether.Analytics.EventProcessor.Host
{
    internal class GameEventPipelineBuilder
    {
        private List<GameEventHandler> globalHandlers = new List<GameEventHandler>();
        private List<EventPipelineBuilder> eventPipelineBuilders = new List<EventPipelineBuilder>();
        EventPipelineBuilder unhandledEventBuilder;

        public GameEventPipelineBuilder()
        {
        }

        internal EventPipelineBuilder Event(string eventName)
        {
            var builder = new EventPipelineBuilder(eventName);
            eventPipelineBuilders.Add(builder);

            return builder;
        }

        internal GameEventPipelineBuilder AddHandler(GameEventHandler eventHandler)
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
        private GameEventHandler[] gameEventHandlers;
        private OutputManager[] outputManagers;

        public EventPipeline(string messageType, GameEventHandler[] gameEventHandlers, OutputManager[] outputManagers)
        {
            MessageType = messageType;
            this.gameEventHandlers = gameEventHandlers;
            this.outputManagers = outputManagers;
        }

        internal void ProcessMessage(GameMessage msg)
        {
            foreach (var handler in gameEventHandlers)
            {
                var result = handler.ProcessMessage(msg);
                if (result.StopProcessing)
                {
                    return;
                }
            }
            foreach (var outputManager in outputManagers)
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
        private string eventName;
        private List<GameEventHandler> handlers = new List<GameEventHandler>();
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

    class GameHandlerResult
    {
        public bool StopProcessing { get; set; }
    }



}