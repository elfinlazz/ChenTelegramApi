#include "tlobject.hpp"
#include "../utils/streamingutils.hpp"

TLObject::~TLObject()
{
}

void TLObject::serialize(std::vector<uint8_t> *vector)
{
    uint32_t classId = getClassId();
    if (classId != 0)
        StreamingUtils::writeInteger(classId, vector);

    serializeBody(vector);
}

void TLObject::deserialize(std::vector<uint8_t> *vector)
{
    if (getClassId() != 0)
    {
        uint32_t classId = StreamingUtils::readInteger(vector);
        if (classId != getClassId())
            return;
    }

    deserializeBody(vector);
}