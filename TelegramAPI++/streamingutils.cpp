#include "streamingutils.hpp"
#include <iostream>

void StreamingUtils::writeVector(std::vector<char>* source, std::vector<char>* vector)
{
    std::vector<char>::iterator i = source->begin();
    for (;;)
    {
        writeByte(*i++, vector);
        if (i == source->end())
            break;
    }
}

void StreamingUtils::writeInteger(int i, std::vector<char>* vector)
{
    writeByte(i & 0xFF, vector);
    writeByte((i >> 8) & 0xFF, vector);
    writeByte((i >> 16) & 0xFF, vector);
    writeByte((i >> 24) & 0xFF, vector);
}

void StreamingUtils::writeLong(int64_t l, std::vector<char>* vector)
{
    writeByte(l & 0xFF, vector);
    writeByte((l >> 8) & 0xFF, vector);
    writeByte((l >> 16) & 0xFF, vector);
    writeByte((l >> 24) & 0xFF, vector);

    writeByte((l >> 32) & 0xFF, vector);
    writeByte((l >> 40) & 0xFF, vector);
    writeByte((l >> 48) & 0xFF, vector);
    writeByte((l >> 56) & 0xFF, vector);
}

void StreamingUtils::writeByteArray(char* buf, int length, std::vector<char>* vector)
{
    for (int i = 0; i < length; ++i)
    {
        writeByte(buf[i], vector);
    }
}

void StreamingUtils::writeByte(char c, std::vector<char>* vector)
{
    vector->insert(vector->end(), c);
}

unsigned char StreamingUtils::readByte(std::vector<char>* vector)
{
    std::vector<char>::iterator it = vector->begin();
    unsigned char c = *it;
    vector->erase(it);
    return c;
}

int StreamingUtils::readInteger(std::vector<char>* vector)
{
    int a = 0 | readByte(vector);
    int b = 0 | readByte(vector);
    int c = 0 | readByte(vector);
    int d = 0 | readByte(vector);

    int x = a;
    x += (b << 8);
    x += (c << 16);
    x += (d << 24);

    return a + (b << 8) + (c << 16) + (d << 24);
}

int64_t StreamingUtils::readLong(std::vector<char>* vector)
{
    int64_t a = readInteger(vector);
    int64_t b = readInteger(vector);

    return a + (b << 32);
}

void StreamingUtils::readByteArray(char* target, int count, std::vector<char>* vector)
{
    for (int i = 0; i < count; i++)
        target[i] = readByte(vector);
}