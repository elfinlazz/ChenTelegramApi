#include <time.h>
#include <sstream>

#include "pqreq.hpp"
#include "streamingutils.hpp"

PQReq::PQReq()
{
}

void PQReq::serializeBody(std::vector<char>* vector)
{
    time_t unix = time(nullptr);
    StreamingUtils::writeLong(0, vector);
    StreamingUtils::writeLong(unix * pow(2, 32), vector);

    std::vector<char> pqrequest;
    _pq.serialize(&pqrequest);
    StreamingUtils::writeInteger(pqrequest.size(), vector);
    StreamingUtils::writeVector(&pqrequest, vector);
}

void PQReq::deserializeBody(std::vector<char>* vector)
{
    int64_t authId = StreamingUtils::readLong(vector);
    int64_t unix = StreamingUtils::readLong(vector);
    int pqsize = StreamingUtils::readInteger(vector);

    PQ pq;
    pq.deserialize(vector);
}

int PQReq::getClassId()
{
    return 0;
}

PQReq::~PQReq()
{
}
