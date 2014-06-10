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
    for(int i = 0; i < length; ++i)
    {
        writeByte(buf[i], vector);
    }
}

void StreamingUtils::writeByte(char c, std::vector<char>* vector)
{
    vector->insert(vector->end(), c);
}