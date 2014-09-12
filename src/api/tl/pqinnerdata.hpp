#pragma once

#include "tlobject.hpp"

class PQInnerData : public TLObject
{
public:
    PQInnerData();

    ~PQInnerData();

    virtual int getClassId() override;

    virtual void serializeBody(std::vector<char> *vector) override;

    int64_t pq;
    int64_t p;
    int64_t q;
    char nonce[16];
    char serverNonce[16];
    char newNonce[32];
};