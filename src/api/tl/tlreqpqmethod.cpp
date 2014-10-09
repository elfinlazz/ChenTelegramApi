#include "tlreqpqmethod.hpp"
#include "../utils/streamingutils.hpp"

TLReqPQMethod::TLReqPQMethod()
{
    this->sendObject = new PQReq();
}

void TLReqPQMethod::receiveObject(std::vector<uint8_t> &vector)
{
    recvObject = new PQRes();
    StreamingUtils::readLong(vector); // strip auth_key_id
    StreamingUtils::readLong(vector); // strip message_id
    StreamingUtils::readInteger(vector); // strip message_length

    recvObject->deserialize(vector);
}
