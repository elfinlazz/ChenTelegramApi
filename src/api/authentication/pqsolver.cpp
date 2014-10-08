#include <stdlib.h>
#include <algorithm>
#include "pqsolver.hpp"

PQSolver::PQSolver()
{
}

PQSolver::~PQSolver()
{
}

uint64_t gcd(uint64_t a, uint64_t b)
{
    while (a != 0 && b != 0)
    {
        while ((b & 1) == 0) b >>= 1;
        while ((a & 1) == 0) a >>= 1;

        a > b ? (a -= b) : (b -= a);
    }

    return b == 0 ? a : b;
}

uint64_t randomNumber(uint64_t max)
{
    return rand() % max;
}

void PQSolver::solvePQ(uint64_t input, uint32_t *pPtr, uint32_t *qPtr)
{
    // Credit: https://github.com/ex3ndr/telegram-mt/blob/master/src/main/java/org/telegram/mtproto/secure/pq/PQLopatin.java
    uint64_t g = 0;
    int it = 0;

    for (int i = 0; i < 3; i++)
    {
        uint64_t q = (randomNumber(128) & 15) + 17;
        uint64_t x = randomNumber(1000000000) + 1, y = x;
        int lim = 1 << (i + 18);
        for (int j = 1; j < lim; j++)
        {
            ++it;
            uint64_t a = x, b = x, c = q;
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
            uint64_t z = x < y ? y - x : x - y;
            g = gcd(z, input);

            if (g != 1)
                break;

            if ((j & (j - 1)) == 0)
                y = x;
        }

        if (g > 1)
            break;
    }

    uint64_t p = input / g;
    *pPtr = (uint32_t) std::min(p, g);
    *qPtr = (uint32_t) std::max(p, g);
}