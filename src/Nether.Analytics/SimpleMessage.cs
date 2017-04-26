using System;
using System.Collections.Generic;

namespace Nether.Analytics
{
    public class SimpleMessage : IMessageType
    {
        private Dictionary<string, string> _properties;

        public string MessageType { get; set; }
        public Dictionary<string, string> Properties => _properties;

        public SimpleMessage()
        {
            _properties = new Dictionary<string, string>();
        }
    }
}