using Nether.Analytics.EventHubs;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Analytics.Parsers
{
    public class JsonEventHubMessageParser : IMessageParser<EventHubListenerMessage, DictionaryBasedMessage>
    {
        public Task<DictionaryBasedMessage> ParseAsync(EventHubListenerMessage unparsedMsg)
        {
            var data = Encoding.UTF8.GetString(unparsedMsg.Body.Array, unparsedMsg.Body.Offset, unparsedMsg.Body.Count);

            var json = JObject.Parse(data);
            var gameEventType = (string)json["type"];
            var version = (string)json["version"];

            if (gameEventType == null || version == null)
                throw new Exception("Unable to resolve Game Event Type, since game event doesn't contain type and/or version property");

            var parsedMsg = new DictionaryBasedMessage();
            parsedMsg.MessageType = $"{gameEventType}|{version}";

            foreach (var p in json)
            {
                //TODO: Replace below naive parse implementation with more sofisticated

                var key = p.Key;
                try
                {
                    var value = (string)json[p.Key];

                    parsedMsg.Properties[key] = value;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Unable to parse property:{key} as string on {parsedMsg.MessageType}");
                    Console.WriteLine("WARNING: Coninuing anyway!!! TODO: Fix this parsing problem!!!");
                }
            }

            return Task.FromResult(parsedMsg);
        }

    }
}
