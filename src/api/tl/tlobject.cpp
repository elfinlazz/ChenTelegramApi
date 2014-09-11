#include "tlobject.hpp"
#include "../utils/streamingutils.hpp"

TLObject::~TLObject()
{
}

void TLObject::serialize(std::vector<char> *vector)
{
    int classId = getClassId();
    if (classId != 0)
        StreamingUtils::writeInteger(classId, vector);

    serializeBody(vector);
}

void TLObject::deserialize(std::vector<char> *vector)
{
    if (getClassId() != 0)
    {
        int classId = StreamingUtils::readInteger(vector);
        if (classId != getClassId())
            return;
    }

    deserializeBody(vector);
}