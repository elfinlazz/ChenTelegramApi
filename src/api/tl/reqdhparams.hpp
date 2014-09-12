#pragma once

#include "tlobject.hpp"

class ReqDHParams : public TLObject
{
public:
    ReqDHParams();

    ~ReqDHParams();

    char nonce[16];
    char serverNonce[16];
    int64_t p;
    int64_t q;
    int64_t fingerprint;
    char encData[260];
};