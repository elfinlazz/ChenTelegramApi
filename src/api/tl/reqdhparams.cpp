#include "reqdhparams.hpp"
#include "../utils/streamingutils.hpp"

ReqDHParams::ReqDHParams()
{

}

ReqDHParams::~ReqDHParams()
{

}

uint32_t ReqDHParams::getClassId()
{
    return 0xd712e4be;
}

void ReqDHParams::deserializeBody(std::vector<uint8_t> *vector)
{
    StreamingUtils::readByteArray(nonce, 16, vector);
    StreamingUtils::readByteArray(serverNonce, 16, vector);
    p = StreamingUtils::readIntegerFromTLByteArray(vector);
    q = StreamingUtils::readLongFromTLByteArray(vector);
    fingerprint = StreamingUtils::readLong(vector);
    StreamingUtils::readByteArray(encData, 260, vector);
}

void ReqDHParams::serializeBody(std::vector<uint8_t> *vector)
{

}
