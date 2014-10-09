#pragma once

#include "tlobject.hpp"

class ReqDHParams : public TLObject
{
public:
    ReqDHParams();

    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> &) override;

    virtual void deserializeBody(std::vector<uint8_t> &) override;

private:
    uint8_t nonce[16];
    uint8_t serverNonce[16];
    uint64_t p;
    uint64_t q;
    uint64_t fingerprint;
    uint8_t encData[260];
};