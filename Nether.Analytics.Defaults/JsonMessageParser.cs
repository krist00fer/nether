// KEEP

using Nether.Analytics.EventHubs;
using System;
using System.Threading.Tasks;

namespace Nether.Analytics.Parsers
{
    public class JsonEventHubMessageParser : IMessageParser<EventHubListenerMessage, SimpleMessage>
    {
        public Task<SimpleMessage> ParseAsync(EventHubListenerMessage message)
        {
            throw new NotImplementedException();
        }
    }
}