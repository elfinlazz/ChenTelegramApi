#include "tlreqdhmethod.hpp"
#include "../utils/streamingutils.hpp"

TLReqDHMethod::TLReqDHMethod()
{
    sendObject = new ReqDHParams();
}

void TLReqDHMethod::receiveObject(std::vector<uint8_t> &vector)
{
    recvObject = new ServerDHParams();
    StreamingUtils::readLong(vector); // strip auth_key_id
    StreamingUtils::readLong(vector); // strip message_id
    StreamingUtils::readInteger(vector); // strip message_length

    recvObject->deserialize(vector);
}
