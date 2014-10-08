#include "streamingutils.hpp"
#include <iostream>

void StreamingUtils::writeVector(std::vector<uint8_t> *source, std::vector<uint8_t> *vector)
{
    std::vector<uint8_t>::iterator i = source->begin();
    for (; ;)
    {
        writeByte(*i++, vector);
        if (i == source->end())
            break;
    }
}

void StreamingUtils::writeInteger(uint32_t i, std::vector<uint8_t> *vector)
{
    writeByte((uint8_t) (i & 0xFF), vector);
    writeByte((uint8_t) ((i >> 8) & 0xFF), vector);
    writeByte((uint8_t) ((i >> 16) & 0xFF), vector);
    writeByte((uint8_t) ((i >> 24) & 0xFF), vector);
}

void StreamingUtils::writeLong(uint64_t l, std::vector<uint8_t> *vector)
{
    writeByte((uint8_t) (l & 0xFF), vector);
    writeByte((uint8_t) ((l >> 8) & 0xFF), vector);
    writeByte((uint8_t) ((l >> 16) & 0xFF), vector);
    writeByte((uint8_t) ((l >> 24) & 0xFF), vector);

    writeByte((uint8_t) ((l >> 32) & 0xFF), vector);
    writeByte((uint8_t) ((l >> 40) & 0xFF), vector);
    writeByte((uint8_t) ((l >> 48) & 0xFF), vector);
    writeByte((uint8_t) ((l >> 56) & 0xFF), vector);
}

void StreamingUtils::writeByteArray(uint8_t *buf, uint32_t length, std::vector<uint8_t> *vector)
{
    for (uint32_t i = 0; i < length; i++)
        writeByte(buf[i], vector);
}

void StreamingUtils::writeByte(uint8_t c, std::vector<uint8_t> *vector)
{
    vector->insert(vector->end(), c);
}

uint8_t StreamingUtils::readByte(std::vector<uint8_t> *vector)
{
    std::vector<uint8_t>::iterator it = vector->begin();
    uint8_t c = *it;
    vector->erase(it);
    return c;
}

uint32_t StreamingUtils::readInteger(std::vector<uint8_t> *vector)
{
    uint8_t a = readByte(vector);
    uint8_t b = readByte(vector);
    uint8_t c = readByte(vector);
    uint8_t d = readByte(vector);

    return a + (b << 8) + (c << 16) + (d << 24);
}

uint64_t StreamingUtils::readLong(std::vector<uint8_t> *vector)
{
    uint64_t a = readInteger(vector);
    uint64_t b = readInteger(vector);

    return a + (b << 32);
}

void StreamingUtils::readByteArray(uint8_t *target, uint32_t count, std::vector<uint8_t> *vector)
{
    for (uint32_t i = 0; i < count; i++)
        target[i] = readByte(vector);
}

void StreamingUtils::readTLByteArray(uint8_t *target, std::vector<uint8_t> *vector)
{
    uint8_t count = readByte(vector);
    int startOffset = 1;
    if (count > 254)
    {
        count = readByte(vector);
        count += readByte(vector) << 8;
        count += readByte(vector) << 16;
        startOffset = 4;
    }

    readByteArray(target, count, vector);

    int offset = (count + startOffset) % 4;
    if (offset != 0)
    {
        int offsetCount = 4 - offset;
        for (int i = 0; i < offsetCount; ++i)
            readByte(vector);
    }
}