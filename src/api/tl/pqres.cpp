#include <stdio.h>
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
    StreamingUtils::readTLByteArray(pq, vector);

    TLVector tlVector;
    tlVector.deserialize(vector);
    this->vector = tlVector;
}

int PQRes::getClassId()
{
    return 0x05162463;
}