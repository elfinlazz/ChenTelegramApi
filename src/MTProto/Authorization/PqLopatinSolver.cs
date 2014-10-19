using System;

namespace TelegramApi.MTProto.Authorization
{
    public class PqLopatinSolver : IPqSolver
    {
        // Algorithm from: https://github.com/ex3ndr/telegram-mt/blob/4a1e6fd6e87401eddbf3258c0fd01c13994c5ac2/src/main/java/org/telegram/mtproto/secure/pq/PQLopatin.java
        private UInt64 GreatestCommonDivisor(UInt64 a, UInt64 b)
        {
            while (a != 0 && b != 0)
            {
                while ((b & 1) == 0)
                    b >>= 1;
                while ((a & 1) == 0)
                    a >>= 1;

                if (a > b)
                    a -= b;
                else
                    b -= a;
            }

            return b == 0 ? a : b;
        }

        public PqData SolvePq(UInt64 pq)
        {
            Random r = new Random();
            UInt64 g = 0;
            Int32 it = 0;

            for (int i = 0; i < 3; i++)
            {
                UInt64 q = (UInt64)((r.Next(128) & 15) + 17);
                UInt64 x = (UInt64)(r.Next(1000000000) + 1), y = x;
                Int32 lim = 1 << (i + 18);

                for (int j = 0; j < lim; j++)
                {
                    it++;
                    UInt64 a = x, b = x, c = q;
                    while (b != 0)
                    {
                        if ((b & 1) != 0)
                        {
                            c += a;
                            if (c >= pq)
                                c -= pq;
                        }

                        a += a;
                        if (a >= pq)
                            a -= pq;

                        b >>= 1;
                    }

                    x = c;
                    UInt64 z = x < y ? y - x : x - y;
                    g = GreatestCommonDivisor(z, pq);

                    if (g != 1)
                        break;

                    if ((j & (j - 1)) == 0)
                        y = x;
                }
            }

            UInt64 f = pq / g;
            PqData data = new PqData
                {
                    P = (UInt32)Math.Min(f, g),
                    Q = (UInt32)Math.Max(f, g)
                };

            return data;
        }
    }
}