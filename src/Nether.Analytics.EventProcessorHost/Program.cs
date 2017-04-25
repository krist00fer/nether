// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nether.Analytics.Bing;
using Nether.Analytics.Defaults;
using Nether.Analytics.Inputs.EventHubs;
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

            var blobOutputManager = new BlobOutputManager(outputblobStorageConnectionString);
            var eventHubOutputManager = new EventHubOutputManager(outputEventHubConnectionString);

            var builder = new GameEventRouterBuilder();

            builder.AddHandler(new GamerInfoEnricher());
            builder.UnhandledEvent().OutputTo(eventHubOutputManager);

            builder.Event("location|1.0")
                .AddHandler(new TransformLocationToNewFormatEventHandler())
                .AddHandler(new BingLocationLookupHandler())
                .OutputTo(eventHubOutputManager, blobOutputManager);

            GameEventRouter r = builder.Build();

            var listener = new EventHubProcessor
            {
                Parser = new NetherGameEventParser(),
                Router = builder.Build()
            };

            var gameEventProcessor = new GameEventProcessor
            {
                Listener = new EventHubListener(ingestEventHubConnectionString),
                Parser = new NetherGameEventParser(),
                Pipeline = builder.Build()
            };



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
