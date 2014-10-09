#pragma once

#include <string>
#include "tlobject.hpp"
#include "tlvector.hpp"

class PQRes : public TLObject
{
public:
    PQRes();

    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> &) override;

    virtual void deserializeBody(std::vector<uint8_t> &) override;

    uint8_t nonce[16];
    uint8_t serverNonce[16];
    uint64_t pq = 0;
    TLVector vector;
};

