// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
