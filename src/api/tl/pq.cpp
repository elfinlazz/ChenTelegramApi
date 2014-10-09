#include "pq.hpp"

#include "../utils/streamingutils.hpp"
#include "../utils/randomutils.hpp"

PQ::PQ()
{
    RandomUtils::nextBytes(nonce, 16);
}

void PQ::serializeBody(std::vector<uint8_t> &vector)
{
    StreamingUtils::writeByteArray(nonce, 16, vector);
}

void PQ::deserializeBody(std::vector<uint8_t> &vector)
{
    StreamingUtils::readByteArray(nonce, 16, vector);
}

uint32_t PQ::getClassId()
{
    return 0x60469778;
}