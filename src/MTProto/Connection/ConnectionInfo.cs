namespace TelegramApi.MTProto.Connection
{
    public class ConnectionInfo : IConnectionInfo
    {
        public ConnectionInfo(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public string Host { get; set; }
        public int Port { get; set; }
    }
}