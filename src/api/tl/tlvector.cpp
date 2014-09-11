#include "tlvector.hpp"
#include "../utils/streamingutils.hpp"

TLVector::TLVector()
{
}

TLVector::~TLVector()
{
}

void TLVector::serializeBody(std::vector<char> *vector)
{
    StreamingUtils::writeInteger(contents.size(), vector);
    for (int64_t l : contents)
        StreamingUtils::writeLong(l, vector);
}

void TLVector::deserializeBody(std::vector<char> *vector)
{
    int len = StreamingUtils::readInteger(vector);
    for (int i = 0; i < len; i++)
        contents.push_back(StreamingUtils::readLong(vector));
}

int TLVector::getClassId()
{
    return 0x1cb5c415;
}
