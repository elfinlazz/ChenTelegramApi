#include <time.h>
#include <sstream>
#include <math.h>

#include "pqreq.hpp"
#include "../utils/streamingutils.hpp"

PQReq::PQReq()
{
}

void PQReq::serializeBody(std::vector<char> *vector)
{
    time_t unix = time(nullptr);
    StreamingUtils::writeLong(0, vector);
    StreamingUtils::writeLong(unix * pow(2, 32), vector);

    std::vector<char> pqrequest;
    pq.serialize(&pqrequest);
    StreamingUtils::writeInteger(pqrequest.size(), vector);
    StreamingUtils::writeVector(&pqrequest, vector);
}

void PQReq::deserializeBody(std::vector<char> *vector)
{
    // send only
}

int PQReq::getClassId()
{
    return 0;
}

PQReq::~PQReq()
{
}
