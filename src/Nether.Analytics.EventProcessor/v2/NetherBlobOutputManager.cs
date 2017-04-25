namespace Nether.Analytics.EventProcessor.Host
{
    internal class NetherBlobOutputManager : OutputManager
    {
        private string outputblobStorageConnectionString;

        public NetherBlobOutputManager(string outputblobStorageConnectionString)
        {
            this.outputblobStorageConnectionString = outputblobStorageConnectionString;
        }
    }
}