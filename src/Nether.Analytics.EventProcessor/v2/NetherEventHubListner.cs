namespace Nether.Analytics.EventProcessor.Host
{
    internal class NetherEventHubListner
    {
        private string ingestEventHubConnectionString;

        public NetherEventHubListner(string ingestEventHubConnectionString)
        {
            this.ingestEventHubConnectionString = ingestEventHubConnectionString;
        }
    }
}