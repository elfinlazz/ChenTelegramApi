#include "tlvector.hpp"
#include "../utils/streamingutils.hpp"

TLVector::TLVector()
{
}

void TLVector::serializeBody(std::vector<uint8_t> &vector)
{
    StreamingUtils::writeInteger(contents.size(), vector);
    for (uint64_t l : contents)
        StreamingUtils::writeLong(l, vector);
}

void TLVector::deserializeBody(std::vector<uint8_t> &vector)
{
    int len = StreamingUtils::readInteger(vector);
    for (int i = 0; i < len; i++)
        contents.push_back(StreamingUtils::readLong(vector));
}

uint32_t TLVector::getClassId()
{
    return 0x1cb5c415;
}
