#include "pq.hpp"

#include "streamingutils.hpp"

PQ::PQ()
{
    for (int i = 0; i < 16; ++i)
        _nonce[i] = rand() % 256;
}

void PQ::serializeBody(std::vector<char>* vector)
{
    StreamingUtils::writeByteArray(_nonce, 16, vector);
}

void PQ::deserializeBody(std::vector<char>* vector)
{
    StreamingUtils::readByteArray(_nonce, 16, vector);
}

int PQ::getClassId()
{
    return 0x60469778;
}

PQ::~PQ()
{
}
