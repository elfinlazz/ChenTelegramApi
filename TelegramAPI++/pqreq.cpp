#include <time.h>
#include <sstream>

#include "pqreq.hpp"
#include "streamingutils.hpp"

PQReq::PQReq()
{
    for(int i = 0; i < 16; ++i)
        _nonce[i] = rand() % 256;
}

void PQReq::serializePQRequest(std::vector<char>* vector)
{
    time_t unix = time(nullptr);
    StreamingUtils::writeLong(0, vector);
    StreamingUtils::writeLong(unix * pow(2, 32), vector);

    std::vector<char> pqrequest;
    serialize(&pqrequest);
    StreamingUtils::writeInteger(pqrequest.size(), vector);
    StreamingUtils::writeVector(&pqrequest, vector);
}

void PQReq::serializeBody(std::vector<char>* vector)
{
    StreamingUtils::writeByteArray(_nonce, 16, vector);
}

int PQReq::getClassId()
{
    return 0x60469778;
}

PQReq::~PQReq()
{
}
