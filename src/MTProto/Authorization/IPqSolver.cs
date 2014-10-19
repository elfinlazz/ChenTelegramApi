using System;

namespace TelegramApi.MTProto.Authorization
{
    public interface IPqSolver
    {
        PqData SolvePq(UInt64 pq);
    }
}