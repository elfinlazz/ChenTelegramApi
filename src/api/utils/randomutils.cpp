#include <stdlib.h>
#include "randomutils.hpp"

void RandomUtils::nextBytes(uint8_t *input, int count)
{
    for (int i = 0; i < count; i++)
        input[i] = (uint8_t) (rand() % 256);
}