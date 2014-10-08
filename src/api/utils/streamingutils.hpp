#pragma once

#include <inttypes.h>
#include <ostream>
#include <vector>

class StreamingUtils
{
public:
    static void writeByte(uint8_t, std::vector<uint8_t> *);

    static void writeInteger(uint32_t, std::vector<uint8_t> *);

    static void writeLong(uint64_t, std::vector<uint8_t> *);

    static void writeByteArray(uint8_t *, uint32_t, std::vector<uint8_t> *);

    static void writeVector(std::vector<uint8_t> *, std::vector<uint8_t> *);

    static uint8_t readByte(std::vector<uint8_t> *);

    static uint32_t readInteger(std::vector<uint8_t> *);

    static uint64_t readLong(std::vector<uint8_t> *);

    static void readByteArray(uint8_t *, uint32_t, std::vector<uint8_t> *);

    static void readTLByteArray(uint8_t *, std::vector<uint8_t> *);

    static void writeTLBytes(uint8_t *, uint32_t count, std::vector<uint8_t> *);

    static void writeLongAsTLBytes(uint64_t, std::vector<uint8_t> *);

    static void writeIntegerAsTLBytes(uint32_t, std::vector<uint8_t> *);

    static uint64_t readLongFromTLByteArray(std::vector<uint8_t> *);

    static uint32_t readIntegerFromTLByteArray(std::vector<uint8_t> *);

    static void DumpVector(std::string, std::vector<uint8_t> *);
};

