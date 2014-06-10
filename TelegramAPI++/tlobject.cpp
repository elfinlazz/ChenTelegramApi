#include "tlobject.hpp"
#include "streamingutils.hpp"

TLObject::~TLObject()
{
}

void TLObject::serialize(std::vector<char>* vector)
{
    StreamingUtils::writeInteger(getClassId(), vector);
    serializeBody(vector);
}