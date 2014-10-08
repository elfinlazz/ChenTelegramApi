#include "pqres.hpp"
#include "../utils/streamingutils.hpp"

PQRes::PQRes()
{
}

PQRes::~PQRes()
{
}

void PQRes::serializeBody(std::vector<uint8_t> *vector)
{

}

void PQRes::deserializeBody(std::vector<uint8_t> *vector)
{
    StreamingUtils::readByteArray(nonce, 16, vector);
    StreamingUtils::readByteArray(serverNonce, 16, vector);
    uint8_t arr[8];
    StreamingUtils::readTLByteArray(arr, vector);

    uint8_t shift = 0;
    for (int32_t i = 7; i >= 0; i--, shift++)
        pq += arr[i] << (shift * 8);

    TLVector tlVector;
    tlVector.deserialize(vector);
    this->vector = tlVector;
}

uint32_t PQRes::getClassId()
{
    return 0x05162463;
}