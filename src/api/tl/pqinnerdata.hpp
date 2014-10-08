#pragma once

#include "tlobject.hpp"

class PQInnerData : public TLObject
{
public:
    PQInnerData();

    ~PQInnerData();

    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> *vector) override;

    int64_t pq;
    int64_t p;
    int64_t q;
    uint8_t nonce[16];
    uint8_t serverNonce[16];
    uint8_t newNonce[32];
};