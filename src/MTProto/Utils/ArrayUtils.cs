using System.Linq;

namespace TelegramApi.MTProto.Utils
{
    public static class ArrayUtils
    {
        public static byte[] Concat(params byte[][] arrays)
        {
            return arrays.SelectMany(x => x).ToArray();
        }

        public static byte[] Xor(byte[] left, byte[] right)
        {
            byte[] val = new byte[left.Length];
            for (int i = 0; i < left.Length; i++)
                val[i] = (byte)(left[i] ^ right[i]);
            return val;
        }

        public static bool Equal(byte[] left, byte[] right)
        {
            if (left.Length != right.Length)
                return false;

            return !left.Where((t, i) => t != right[i]).Any();
        }
    }
}