#include <time.h>
#include <sstream>
#include <math.h>

#include "pqreq.hpp"
#include "../utils/streamingutils.hpp"

PQReq::PQReq()
{
}

void PQReq::serializeBody(std::vector<uint8_t> &vector)
{
    uint64_t nixTime = (uint64_t) (time(nullptr) * pow(2, 32));
    StreamingUtils::writeLong(0, vector);
    StreamingUtils::writeLong(nixTime, vector);

    std::vector<uint8_t> pqrequest;
    pq.serialize(pqrequest);
    StreamingUtils::writeInteger(pqrequest.size(), vector);
    StreamingUtils::writeVector(pqrequest, vector);
}

void PQReq::deserializeBody(std::vector<uint8_t> &vector)
{
    // send only
}

uint32_t PQReq::getClassId()
{
    return 0;
}
