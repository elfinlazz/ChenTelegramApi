#pragma once

#include <vector>

#include "tlobject.hpp"

class PQ : public TLObject
{
public:
    PQ();

    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> &) override;

    virtual void deserializeBody(std::vector<uint8_t> &) override;

    uint8_t nonce[16];
};

