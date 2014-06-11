#include "tlobject.hpp"
#include "streamingutils.hpp"

TLObject::~TLObject()
{
}

void TLObject::serialize(std::vector<char>* vector)
{
    int classId = getClassId();
    if (classId != 0)
        StreamingUtils::writeInteger(classId, vector);

    serializeBody(vector);
}