using System;
using System.Threading.Tasks;

namespace Nether.Analytics
{
    public class ConsoleOutputManager<T> : IOutputManager<T>
    {
        private IMessageSerializer<T> _serializer;

        public ConsoleOutputManager(IMessageSerializer<T> serializer)
        {
            _serializer = serializer;
        }

        public Task OutputMessageAsync(T msg)
        {
            var str = _serializer.Serialize(msg);

            Console.WriteLine(str);

            return Task.CompletedTask;
        }
    }
}
