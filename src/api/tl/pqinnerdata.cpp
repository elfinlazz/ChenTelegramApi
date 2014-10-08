#include "pqinnerdata.hpp"
#include "../utils/streamingutils.hpp"
#include "../utils/randomutils.hpp"

PQInnerData::PQInnerData(uint64_t pq, uint32_t p, uint32_t q, uint8_t pNonce[], uint8_t pServerNonce[])
{
    this->pq = pq;
    this->p = p;
    this->q = q;
    RandomUtils::nextBytes(newNonce, 32);
}

PQInnerData::~PQInnerData()
{

}

void PQInnerData::deserializeBody(std::vector<uint8_t> *vector)
{

}

void PQInnerData::serializeBody(std::vector<uint8_t> *vector)
{
    StreamingUtils::writeLongAsTLBytes(pq, vector);
    StreamingUtils::writeIntegerAsTLBytes(p, vector);
    StreamingUtils::writeIntegerAsTLBytes(q, vector);

    StreamingUtils::writeByteArray(nonce, 16, vector);
    StreamingUtils::writeByteArray(serverNonce, 16, vector);
    StreamingUtils::writeByteArray(newNonce, 32, vector);
}

uint32_t PQInnerData::getClassId()
{
    return 0x83c95aec;
}
