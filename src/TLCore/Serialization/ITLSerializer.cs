namespace TelegramApi.TLCore.Serialization
{
    public interface ITLSerializer
    {
        byte[] Serialize(TLObject tlObject);

        T Deserialize<T>(byte[] array)
            where T : TLObject, new();
    }
}