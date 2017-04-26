// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nether.Analytics.Bing;
using Nether.Analytics.EventHubs;
using Nether.Analytics.Parsers;
using System;

namespace Nether.Analytics.EventProcessorHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ingestEventHubConnectionString = "...";
            var outputblobStorageConnectionString = "...";
            var outputEventHubConnectionString = "...";
            var unknownGameEventsEventHubConnectionString = "...";

            // Setup Listener
            var listenerConfig = new EventHubsListenerConfiguration
            {
                EventHubConnectionString = "Endpoint=sb://nether.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=h16jv6nc0bVfgdWrZp7f5Fpau4jaSu2YH+U2xg0YI14=",
                EventHubPath = "ingest",
                ConsumerGroupName = "nether",
                StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=netherdashboard;AccountKey=oT30a8/BSwTFg/4GGWLPCeGIHBfgDcMf9zEThKHlY4hjUNy3sYUTSWXWa3yJMoX2lvTnWSIrjtwU9kg9YaL0Qw==;EndpointSuffix=core.windows.net",
                LeaseContainerName = "eventhublease"
            };

            var listener = new EventHubsListener(listenerConfig);

            // Setup Message Parser
            var parser = new JsonEventHubMessageParser();

            // Setup Output Managers
            var blobOutputManager = new BlobOutputManager(outputblobStorageConnectionString);
            var eventHubOutputManager = new EventHubOutputManager(outputEventHubConnectionString);

            // Build up the Router Pipeline
            var builder = new MessageRouterBuilder<SimpleMessage>();

            builder.AddMessageHandler(new GamerInfoEnricher());
            builder.UnhandledEvent().OutputTo(eventHubOutputManager);

            builder.Event("location|1.0")
                .AddHandler(new TransformLocationToNewFormatEventHandler())
                .AddHandler(new BingLocationLookupHandler())
                .OutputTo(eventHubOutputManager, blobOutputManager);

            var router = builder.Build();

            var gameEventProcessor = new MessageProcessor<EventHubListenerMessage, SimpleMessage>(listener, parser, router);


            gameEventProcessor.ProcessAndBlock();
        }

        private static void Greet()
        {
            Console.WriteLine();
            Console.WriteLine(@" _   _      _   _               ");
            Console.WriteLine(@"| \ | | ___| |_| |__   ___ _ __ ");
            Console.WriteLine(@"|  \| |/ _ \ __| '_ \ / _ \ '__|");
            Console.WriteLine(@"| |\  |  __/ |_| | | |  __/ |   ");
            Console.WriteLine(@"|_| \_|\___|\__|_| |_|\___|_|   ");
            Console.WriteLine(@"- Analytics Event Processor Host -");
            Console.WriteLine();
        }
    }
}
