using System.Threading.Tasks;
using TelegramApi.MTProto.Authorization;
using TelegramApi.MTProto.Connection;

namespace TelegramApi.ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConnectionInfo connectionInfo = new ConnectionInfo("173.240.5.253", 25);
            Authenticator authenticator = new Authenticator(new[] { connectionInfo });
            Task attempt = authenticator.AttemptAuthentication();
            attempt.Wait();
        }
    }
}