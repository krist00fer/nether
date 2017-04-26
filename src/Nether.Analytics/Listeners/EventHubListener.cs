﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

// KEEP

namespace Nether.Analytics.Listeners
{
    public class EventHubsListener : IEventProcessor, IMessageListener<EventHubListenerMessage>
    {
        private readonly EventProcessorHost _host;
        Action<IEnumerable<EventHubListenerMessage>> _messageHandler;

        public EventHubsListener(EventHubsListenerConfiguration config)
        {
            #region Assert arguments are provided
            if (string.IsNullOrWhiteSpace(config.EventHubPath))
                throw new ArgumentException("EventHubPath needs to be provided");

            if (string.IsNullOrWhiteSpace(config.EventHubConnectionString))
                throw new ArgumentException("EventHubConnectionString needs to be provided");

            if (string.IsNullOrWhiteSpace(config.StorageConnectionString))
                throw new ArgumentException("StorageConnectionString needs to be provided");

            if (string.IsNullOrWhiteSpace(config.LeaseContainerName))
                throw new ArgumentException("LeaseContainerName needs to be provided");

            // If ConsumerGroupName is left null or empty, then use default ConsumerGroupName
            var consumerGroupName = string.IsNullOrWhiteSpace(config.ConsumerGroupName) ? 
                PartitionReceiver.DefaultConsumerGroupName : config.ConsumerGroupName;
            #endregion

            _host = new EventProcessorHost(
                config.EventHubPath,
                consumerGroupName,
                config.EventHubConnectionString,
                config.StorageConnectionString,
                config.LeaseContainerName);
        }

        public async Task StartAsync(Action<IEnumerable<EventHubListenerMessage>> messageHandler)
        {
            _messageHandler = messageHandler;
            // Register this object as the processor of incomming EventHubMessages by using
            // a custom EventHubProcessorFactory
            await _host.RegisterEventProcessorFactoryAsync(new EventHubProcessorFactory(this));
        }

        public async Task StopAsync()
        {
            await _host.UnregisterEventProcessorAsync();
            _messageHandler = null;
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            var gameMessages = new List<EventHubListenerMessage>();
            var dequeuedTime = DateTime.UtcNow;

            foreach (var msg in messages)
            {
                gameMessages.Add(new EventHubListenerMessage
                {
                    Body = msg.Body,
                    EnqueuedTime = msg.SystemProperties.EnqueuedTimeUtc,
                    DequeuedTime = dequeuedTime
                });
            }

            _messageHandler(gameMessages);

            await context.CheckpointAsync();
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            throw new NotImplementedException();
        }


        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"EventHubProcessor.CloseAsync Owner:{context.Owner}, ParitionId:{context.PartitionId}");

            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"EventHubProcessor.OpenAsync Owner:{context.Owner}, ParitionId:{context.PartitionId}");

            return Task.CompletedTask;
        }


        /// <summary>
        /// Private factory class to make sure we use the current instance of EventHubProcessor as the
        /// instance for the EventProcessorHost
        /// </summary>
        private class EventHubProcessorFactory : IEventProcessorFactory
        {
            IEventProcessor _processor;

            public EventHubProcessorFactory(IEventProcessor processor)
            {
                _processor = processor;
            }

            public IEventProcessor CreateEventProcessor(PartitionContext context)
            {
                return _processor;
            }
        }
    }


    




}