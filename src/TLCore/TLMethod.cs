namespace TelegramApi.TLCore
{
    public abstract class TLMethod<TSend, TRecv>
        where TSend : TLObject, new()
        where TRecv : TLObject, new()
    {
        public TSend SendObject { get; set; }
        public TRecv ReceiveObject { get; set; }
    }
}