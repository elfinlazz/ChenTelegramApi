#include <stdlib.h>
#include <algorithm>
#include "pqsolver.hpp"

PQSolver::PQSolver()
{
}

PQSolver::~PQSolver()
{
}

int64_t gcd(int64_t a, int64_t b)
{
    while (a != 0 && b != 0)
    {
        while ((b & 1) == 0) b >>= 1;
        while ((a & 1) == 0) a >>= 1;

        a > b ? (a -= b) : (b -= a);
    }

    return b == 0 ? a : b;
}

int64_t randomNumber(int64_t max)
{
    return rand() % max;
}

void PQSolver::solvePQ(int64_t input, int64_t *pPtr, int64_t *qPtr)
{
    // Credit: https://github.com/ex3ndr/telegram-mt/blob/master/src/main/java/org/telegram/mtproto/secure/pq/PQLopatin.java
    int64_t g = 0;
    int it = 0;

    for (int i = 0; i < 3; i++)
    {
        int64_t q = (randomNumber(128) & 15) + 17;
        int64_t x = randomNumber(1000000000) + 1, y = x;
        int lim = 1 << (i + 18);
        for (int j = 1; j < lim; j++)
        {
            ++it;
            int64_t a = x, b = x, c = q;
            while (b != 0)
            {
                if ((b & 1) != 0)
                {
                    c += a;
                    if (c >= input)
                        c -= input;
                }
                a += a;
                if (a >= input)
                    a -= input;
                b >>= 1;
            }
            x = c;
            int64_t z = x < y ? y - x : x - y;
            g = gcd(z, input);

            if (g != 1)
                break;

            if ((j & (j - 1)) == 0)
                y = x;
        }

        if (g > 1)
            break;
    }

    int64_t p = input / g;
    *pPtr = std::min(p, g);
    *qPtr = std::max(p, g);
}