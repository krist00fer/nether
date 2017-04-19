namespace Nether.Analytics.EventProcessor.Host
{
    internal class Location_1_0_Google_GameEventHandler
    {
        private NetherEventHubOutputManager eventHubOutputManager;
        private NetherBlobOutputManager blobOutputManager;

        public Location_1_0_Google_GameEventHandler(NetherEventHubOutputManager eventHubOutputManager, NetherBlobOutputManager blobOutputManager)
        {
            this.eventHubOutputManager = eventHubOutputManager;
            this.blobOutputManager = blobOutputManager;
        }
    }
}