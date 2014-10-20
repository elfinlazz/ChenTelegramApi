using System;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using TelegramApi.MTProto.Utils;

namespace TelegramApi.MTProto.Encryption
{
    public class RsaCrypter : IRsaCrypter
    {
        public byte[] EncryptBytes(byte[] input, ulong fingerprint)
        {
            IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/ECB/NoPadding");
            AsymmetricKeyParameter publicKey = ServerKeys.GetKey(fingerprint);
            cipher.Init(true, publicKey);
            return cipher.DoFinal(input);
        }

        public byte[] DecryptBytes(byte[] input, byte[] aesKey, byte[] iv)
        {
            byte[] iv1 = new byte[iv.Length / 2];
            byte[] iv2 = new byte[iv.Length / 2];
            Array.Copy(iv, 0, iv1, 0, iv1.Length);
            Array.Copy(iv, iv.Length / 2, iv2, 0, iv2.Length);

            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.KeySize = aesKey.Length * 8;
                aesProvider.Padding = PaddingMode.None;
                aesProvider.IV = iv1;
                aesProvider.Key = aesKey;

                int blockSize = aesProvider.BlockSize / 8;

                byte[] xPrev = new byte[blockSize];
                Buffer.BlockCopy(iv1, 0, xPrev, 0, blockSize);
                byte[] yPrev = new byte[blockSize];
                Buffer.BlockCopy(iv2, 0, yPrev, 0, blockSize);

                MemoryStream plainStream = new MemoryStream();
                using (ICryptoTransform dec = aesProvider.CreateDecryptor())
                {
                    BinaryWriter plainWriter = new BinaryWriter(plainStream);

                    byte[] x = new byte[blockSize];
                    for (int i = 0; i < input.Length; i += blockSize)
                    {
                        Buffer.BlockCopy(input, i, x, 0, blockSize);
                        byte[] y = ArrayUtils.Xor(dec.TransformFinalBlock(ArrayUtils.Xor(x, yPrev), 0, blockSize), xPrev);

                        Buffer.BlockCopy(x, 0, xPrev, 0, blockSize);
                        Buffer.BlockCopy(y, 0, yPrev, 0, blockSize);

                        plainWriter.Write(y);
                    }
                }

                return plainStream.ToArray();
            }
        }
    }
}