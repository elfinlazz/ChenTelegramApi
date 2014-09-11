#pragma once

#include <inttypes.h>
#include <ostream>
#include <vector>

class StreamingUtils
{
public:
    static void writeByte(char, std::vector<char> *);

    static void writeInteger(int, std::vector<char> *);

    static void writeLong(int64_t, std::vector<char> *);

    static void writeByteArray(char *, int, std::vector<char> *);

    static void writeVector(std::vector<char> *, std::vector<char> *);

    static unsigned char readByte(std::vector<char> *);

    static int readInteger(std::vector<char> *);

    static int64_t readLong(std::vector<char> *);

    static void readByteArray(char *, int, std::vector<char> *);

    static void readTLByteArray(char *target, std::vector<char> *vector);
};

