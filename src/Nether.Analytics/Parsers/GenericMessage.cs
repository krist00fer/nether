using System;
using System.Collections.Generic;

namespace Nether.Analytics.Parsers
{
    public class GenericMessage : IKnownMessageType
    {
        private Dictionary<string, string> _properties;

        public string MessageType { get; set; }
        public Dictionary<string, string> Properties => _properties;

        public GenericMessage()
        {
            _properties = new Dictionary<string, string>();
        }
    }
}