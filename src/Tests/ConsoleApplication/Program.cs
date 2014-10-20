using System.Threading.Tasks;
using TelegramApi.MTProto.Authorization;
using TelegramApi.MTProto.Connection;
using TelegramApi.MTProto.Encryption;

namespace TelegramApi.ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConnectionInfo connectionInfo = new ConnectionInfo("149.154.167.40", 443);
            Authenticator authenticator = new Authenticator(new[] { connectionInfo }, new PqLopatinSolver(), new RsaCrypter());
            Task attempt = authenticator.AttemptAuthentication();
            attempt.Wait();
        }
    }
}