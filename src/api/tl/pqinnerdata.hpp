#pragma once

#include "tlobject.hpp"

class PQInnerData : public TLObject
{
public:
    PQInnerData(uint64_t pq, uint32_t p, uint32_t q, uint8_t (&pNonce)[16], uint8_t (&pServerNonce)[16]);

    ~PQInnerData();

    virtual uint32_t getClassId() override;

    virtual void deserializeBody(std::vector<uint8_t> *vector) override;

    virtual void serializeBody(std::vector<uint8_t> *vector) override;

    uint64_t pq;
    uint32_t p;
    uint32_t q;
    uint8_t (&nonce)[16];
    uint8_t (&serverNonce)[16];
    uint8_t newNonce[32];
};