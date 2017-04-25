using System;

namespace Nether.Analytics.EventProcessor.Host
{

    /*

Each package is licensed to you by its owner. NuGet is not responsible for, nor does it grant any licenses to, third-party packages. Some packages may include dependencies which are governed by additional licenses. Follow the package source (feed) URL to determine any dependencies.

Package Manager Console Host Version 4.1.0.2427

Type 'get-help NuGet' to see all available NuGet commands.

PM> install-Package Nether.Analytics

......



                       +-------------------+
                       | Game Client       |
                       +---------+---------+
                                 |
                       +---------v---------+
                       | EventHub          |
                       +---------+---------+
                                 |
    +--------------------------------------------+
    |      GameEventProcessor    |               |
    |                            |               |
    |                            |               |
    |                            |               |
    |                  +---------v------------+  |
    |                  | GameEventListner     |  |
    |                  +---------+------------+  |
    |                            |               |
    |                  +---------v------------+  |
    |                  | GameEventParser      |  |
    |                  +---------+------------+  |
    |                            |               |
    |                  +---------v------------+  |
    |                  | GameEventRouter      |  |
    |                  +-------------+--+--+--+  |
    |                                |  |  |     |
    +--------------------------------------------+
                                     |  |  |
                                     |  |  |
    +--------------------------------------------+
    | GameEventHandlers              |  |  |     |
    |                                |  |  |     |
    |   +----------------------------v+ |  |     |
    |   | PassthroughGameEventHandler | |  |     |
    |   +-----------------------------+ |  |     |
    |                                   |  |     |
    |     +-----------------------------v+ |     |
    |     | SpecializedGameEventHandlers | |     |
    |     +------------------------------+ |     |
    |                                      |     |
    |        +-----------------------------v+    |
    |        | CustomGameEventHandlers      |    |
    |        +------------------------------+    |
    |                                            |
    +----------------------+---------------------+
                           |
                           |
    +----------------------v---------------------+
    | OutputManagers                             |
    |                                            |
    |   +------------------------------------+   |
    |   | NetherBlobOutputManager            |   |
    |   +------------------------------------+   |
    |                                            |
    |   +------------------------------------+   |
    |   | NetherEventHubOutputManager        |   |
    |   +------------------------------------+   |
    |                                            |
    |   +------------------------------------+   |
    |   | CustomOutputManager                |   |
    |   +------------------------------------+   |
    +--------------------------------------------+

    */


    class Program
    {
        static void Main(string[] args)
        {
            var ingestEventHubConnectionString = "...";
            var outputblobStorageConnectionString = "...";
            var outputEventHubConnectionString = "...";
            var unknownGameEventsEventHubConnectionString = "...";

            var blobOutputManager = new NetherBlobOutputManager(outputblobStorageConnectionString);
            var eventHubOutputManager = new NetherEventHubOutputManager(outputEventHubConnectionString);

            var builder = new GameEventPipelineBuilder();

            builder.AddHandler(new GamerInfoEnricher());
            builder.UnhandledEvent().OutputTo(eventHubOutputManager);

            builder.Event("location|1.0")
                .AddHandler(new TransformLocationToNewFormatEventHandler())
                .AddHandler(new Location_2_0_Google_GameEventHandler())
                .OutputTo(eventHubOutputManager, blobOutputManager);


            GameEventRouter r = builder.Build();

            var gameEventProcessor = new GameEventProcessor
            {
                Listner = new EventHubListner(ingestEventHubConnectionString),
                Parser = new JsonEventParser(),
                Pipeline = builder.Build()
            };



        //Greet();



        //// Define the Game Event Processor (this is the main ingest engine of Nether Analytics)
        //// If we were to leave the GameEventProcessor like this, it would fall back to default configuration and behavior
        //var gameEventProcessor = new GameEventProcessor();

        //// The below code shows how we can customize the behavior of Nether EventHubProcessor

        //// Define output managers (only needed if we do customizations of Nether GameEventProcessor)
        //// Output managers are responsible to efficiently write to some output storage in a correct way
        //// - NetherBlobOutputManager will append to BlockBlobs
        //// - NetherEventHubOutputManager will write to EventHub
        //var blobOutputManager = new NetherBlobOutputManager(outputblobStorageConnectionString);
        //var eventHubOutputManager = new NetherEventHubOutputManager(outputEventHubConnectionString);
        //var unknownEventsOutputManager = new NetherBlobOutputManager(unknownGameEventsEventHubConnectionString);

        //// Nether supports listening to events comming in from any listener class implementing the IGameEventListner interface
        //// By default that would be EventHub using the NetherEventHubListner class
        //var netherEventHubListner = new NetherEventHubListner(ingestEventHubConnectionString);
        //gameEventProcessor.GameEventListner = netherEventHubListner;

        //// The Game Event Parser attached to the Game Event Processor is responsible for figuring out what type of event(s) that
        //// has just arrived from the Game Event Listner. Since we are now making this class customizable/replacable we can now
        //// handle any kind of incomming event format: JSON, XML, binary, etc. as long as there is a corresponding Game Event Parser
        //// that can parse the events and their formats. By default Nether would use a JSON format.
        //var netherEventParser = new NetherEventParser();
        //gameEventProcessor.GameEventParser = netherEventParser;

        //// The GameEventRouter inside GameEventProcessor receives the parsed event and is responsible for sending the event to the
        //// correct Game Event Handler. Nether comes with a set of pre-defined game event handlers that handles events in a way that
        //// is compatible with the included end reports. The default Game Event Router, will route the events to an in memory handler
        //// corresponding to pre-defined routes. Now that we can replace the "router" out of the EventProcessor, we could easily
        //// implement scenarios such as having different "actors" in "Service Fabric" handle different event types.
        //var netherEventRouter = new NetherEventRouter();

        //// This example uses the default event router, but configures it to only handle 3 event types: sessionstart, heartbeat and location
        //// Notice that the location event is handled by a custom event handler that wouldn't be included by default in any Nether packages.
        //// PassThroughGameEventHandler can be used to pass events directly through to the output manager(s) without doing any enrichment of the event
        //netherEventRouter.GameEventHandlers.Clear();
        //netherEventRouter.GameEventHandlers.Add("sessionstart|1.0", new PassThroughGameEventHandler(blobOutputManager));
        //netherEventRouter.GameEventHandlers.Add("heartbeat|1.0", new PassThroughGameEventHandler(eventHubOutputManager));
        //netherEventRouter.GameEventHandlers.Add("location|1.0", new Location_1_0_Google_GameEventHandler(eventHubOutputManager, blobOutputManager));
        //netherEventRouter.UnknownGameEventHandler = new PassThroughGameEventHandler(unknownEventsOutputManager);

        //gameEventProcessor.GameEventRouter = netherEventRouter;

        //// There are more than one method to start the processing, this method will start processing and then block the thread indefinitively
        //gameEventProcessor.ProcessAndBlock();
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
