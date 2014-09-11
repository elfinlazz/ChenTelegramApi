#pragma once

#include <string>
#include "tlobject.hpp"

class PQRes : public TLObject
{
public:
    PQRes();

    ~PQRes();

    virtual int getClassId();

    virtual void serializeBody(std::vector<char> *);

    virtual void deserializeBody(std::vector<char> *);

private:
    char nonce[16];
    char serverNonce[16];
    std::string pq;
    //TLLongVector vector;
};

