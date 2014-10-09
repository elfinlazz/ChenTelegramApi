#pragma once

#include <iostream>
#include <vector>

class TLObject
{
public:
    void serialize(std::vector<uint8_t> &);

    virtual void deserialize(std::vector<uint8_t> &);

private:
    virtual uint32_t getClassId() = 0;

    virtual void serializeBody(std::vector<uint8_t> &) = 0;

    virtual void deserializeBody(std::vector<uint8_t> &) = 0;
};

