namespace Nether.Analytics.EventProcessor.Host
{
    internal class NetherEventHubOutputManager : OutputManager
    {
        private string outputEventHubConnectionString;

        public NetherEventHubOutputManager(string outputEventHubConnectionString)
        {
            this.outputEventHubConnectionString = outputEventHubConnectionString;
        }
    }
}