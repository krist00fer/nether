using System;
using System.Collections.Generic;

namespace Nether.Analytics
{
    public class DictionaryBasedMessage : IParsedMessage
    {
        public string MessageType { get; set; }
        public Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();
    }
}