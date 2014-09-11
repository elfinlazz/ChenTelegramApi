#include "tlreqpqmethod.hpp"
#include "../utils/streamingutils.hpp"

TLReqPQMethod::TLReqPQMethod()
{
    this->requiredLength = 85;
    this->sendObject = new PQReq();
}

TLReqPQMethod::~TLReqPQMethod()
{
}

void TLReqPQMethod::receiveObject(std::vector<char> *vector)
{
    recvObject = new PQRes();
    StreamingUtils::readByte(vector); // strip length
    StreamingUtils::readLong(vector); // strip auth_key_id
    StreamingUtils::readLong(vector); // strip message_id
    StreamingUtils::readInteger(vector); // strip message_length

    recvObject->deserialize(vector);
}
