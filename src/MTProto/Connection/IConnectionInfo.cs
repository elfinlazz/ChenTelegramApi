namespace TelegramApi.MTProto.Connection
{
    public interface IConnectionInfo
    {
        string Host { get; set; }
        int Port { get; set; }
    }
}