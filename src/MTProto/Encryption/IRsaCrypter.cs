using System;

namespace TelegramApi.MTProto.Encryption
{
    public interface IRsaCrypter
    {
        byte[] EncryptBytes(byte[] input, UInt64 fingerprint);

        byte[] DecryptBytes(byte[] input, byte[] aesKey, byte[] iv);
    }
}