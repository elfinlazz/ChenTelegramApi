#include <stdlib.h>
#include "randomutils.hpp"

void RandomUtils::nextBytes(char *input, int count)
{
    for (int i = 0; i < count; i++)
        input[i] = (char) (rand() % 256);
}