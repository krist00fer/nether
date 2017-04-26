using System.Collections.Generic;

namespace Nether.Analytics
{
    public interface IDictionaryBasedMessage
    {
        Dictionary<string, string> Properties { get; }
    }
}