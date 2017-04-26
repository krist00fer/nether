using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nether.Analytics.Listeners
{
    public interface IMessageListener<MessageType>
    {
        Task StartAsync(Action<IEnumerable<MessageType>> messageHandler);
        Task StopAsync();
    }
}