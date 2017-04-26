using Newtonsoft.Json;

namespace Nether.Analytics
{
    public class DictionaryBasedMessageJsonSerializer : IMessageSerializer<DictionaryBasedMessage>
    {
        public string Serialize(DictionaryBasedMessage message)
        {
            string json = JsonConvert.SerializeObject(message.Properties);

            return json;
        }
    }
}
