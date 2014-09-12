#include "pq.hpp"

#include "../utils/streamingutils.hpp"
#include "../utils/randomutils.hpp"

PQ::PQ()
{
    RandomUtils::nextBytes(nonce, 16);
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
