namespace Nether.Analytics
{
    public interface IMessageRouter<MessageType>
    {
        void RouteMessage(MessageType message);
    }
}