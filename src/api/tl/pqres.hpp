#pragma once

#include <string>
#include "tlobject.hpp"
#include "tlvector.hpp"

class PQRes : public TLObject
{
public:
    PQRes();

    ~PQRes();

    virtual int getClassId();

    virtual void serializeBody(std::vector<char> *);

    virtual void deserializeBody(std::vector<char> *);

    char nonce[16];
    char serverNonce[16];
    uint64_t pq = 0;
    TLVector vector;
};

