namespace Nether.Analytics.EventProcessor.Host
{
    internal class NetherEventHubOutputManager
    {
        private string outputEventHubConnectionString;

        public NetherEventHubOutputManager(string outputEventHubConnectionString)
        {
            this.outputEventHubConnectionString = outputEventHubConnectionString;
        }
    }
}