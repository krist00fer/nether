using System;

namespace Nether.Analytics.EventProcessor.Host
{
    internal class GameEventProcessor
    {
        public GameEventProcessor()
        {
        }

        public NetherEventHubListner Listner { get; internal set; }
        public NetherEventParser Parser { get; internal set; }
        public NetherEventRouter Pipeline { get; internal set; }

        internal void ProcessAndBlock()
        {
            throw new NotImplementedException();
        }
    }
}