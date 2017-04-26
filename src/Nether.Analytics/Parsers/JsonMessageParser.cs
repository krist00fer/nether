// KEEP

using System;
using System.Threading.Tasks;

namespace Nether.Analytics.Parsers
{
    public class JsonEventHubMessageParser : IMessageParser<EventHubListenerMessage, GenericMessage>
    {
        public Task<GenericMessage> ParseAsync(EventHubListenerMessage message)
        {
            throw new NotImplementedException();
        }
    }
}