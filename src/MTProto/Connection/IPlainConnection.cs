using System.Threading.Tasks;
using TelegramApi.TLCore;

namespace TelegramApi.MTProto.Connection
{
    public interface IPlainConnection
    {
        Task<ConnectionState> ConnectAsync();

        Task<TRecv> ExecuteMethodAsync<TSend, TRecv>(TLMethod<TSend, TRecv> method)
            where TSend : TLObject, new()
            where TRecv : TLObject, new();

        Task SendObjectAsync<T>(T obj)
            where T : TLObject, new();
    }
}