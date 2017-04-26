namespace Nether.Analytics
{
    public interface IMessageRouter<ParsedMessageType> where ParsedMessageType : IMessageType
    {
        void RouteMessage(ParsedMessageType message);
    }
}