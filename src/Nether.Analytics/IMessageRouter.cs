namespace Nether.Analytics
{
    public interface IMessageRouter<ParsedMessageType> where ParsedMessageType : IParsedMessage
    {
        //TODO: Make method Async
        void RouteMessage(ParsedMessageType message);
    }
}