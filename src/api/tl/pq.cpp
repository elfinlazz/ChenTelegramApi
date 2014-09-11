#include <stdlib.h>
#include "pq.hpp"

#include "../utils/streamingutils.hpp"

PQ::PQ()
{
    for (int i = 0; i < 16; ++i)
        nonce[i] = (char) (rand() % 256);
}

void PQ::serializeBody(std::vector<char> *vector)
{
    StreamingUtils::writeByteArray(nonce, 16, vector);
}

void PQ::deserializeBody(std::vector<char> *vector)
{
    StreamingUtils::readByteArray(nonce, 16, vector);
}

int PQ::getClassId()
{
    return 0x60469778;
}

PQ::~PQ()
{
}
