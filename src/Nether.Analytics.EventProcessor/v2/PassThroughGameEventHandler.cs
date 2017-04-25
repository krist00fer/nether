namespace Nether.Analytics.EventProcessor.Host
{
    internal class PassThroughGameEventHandler
    {
        private object[] outputManagers;

        public PassThroughGameEventHandler(params object[] outputManagers)
        {
            this.outputManagers = outputManagers;
        }
    }
}