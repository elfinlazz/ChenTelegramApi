#pragma once

#include "tlobject.hpp"

class ReqDHParams : public TLObject
{
public:
    ReqDHParams();

    ~ReqDHParams();

    uint8_t nonce[16];
    uint8_t serverNonce[16];
    int64_t p;
    int64_t q;
    int64_t fingerprint;
    uint8_t encData[260];
};