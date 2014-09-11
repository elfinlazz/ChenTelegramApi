#include "pqres.hpp"
#include "../utils/streamingutils.hpp"

PQRes::PQRes()
{
}

PQRes::~PQRes()
{
}

void PQRes::serializeBody(std::vector<char> *vector)
{

}

void PQRes::deserializeBody(std::vector<char> *vector)
{
    StreamingUtils::readByteArray(nonce, 16, vector);
    StreamingUtils::readByteArray(serverNonce, 16, vector);
    char arr[8];
    StreamingUtils::readTLByteArray(arr, vector);

    int shift = 0;
    for (int i = 7; i >= 0; i--, shift++)
        pq += ((uint64_t) (unsigned char) arr[i]) << shift * 8;

    TLVector tlVector;
    tlVector.deserialize(vector);
    this->vector = tlVector;
}

int PQRes::getClassId()
{
    return 0x05162463;
}