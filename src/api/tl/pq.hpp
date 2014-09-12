#pragma once

#include <vector>

#include "tlobject.hpp"

class PQ : public TLObject
{
public:
    PQ();

    ~PQ();

    virtual int getClassId();

    virtual void serializeBody(std::vector<char> *);

    virtual void deserializeBody(std::vector<char> *);

    char nonce[16];
};

