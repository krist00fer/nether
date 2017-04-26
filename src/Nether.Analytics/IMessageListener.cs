using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nether.Analytics
{
    public interface IMessageListener<MessageType>
    {
        Task StartAsync(Func<IEnumerable<MessageType>, Task> messageHandler);
        Task StopAsync();
    }
}