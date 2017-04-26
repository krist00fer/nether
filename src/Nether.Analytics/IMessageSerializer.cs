namespace Nether.Analytics
{
    public interface IMessageSerializer<T>
    {
        string Serialize(T message);
    }
}
