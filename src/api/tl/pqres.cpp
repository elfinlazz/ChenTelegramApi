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

    pq = StreamingUtils::readLongFromTLByteArray(vector);

    TLVector tlVector;
    tlVector.deserialize(vector);
    this->vector = tlVector;
}

uint32_t PQRes::getClassId()
{
    return 0x05162463;
}