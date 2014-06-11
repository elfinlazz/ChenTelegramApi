#pragma once

#include <vector>

#include "tlobject.hpp"

class PQ : public TLObject
{
public:
    PQ();
    ~PQ();

    virtual int getClassId();
    virtual void serializeBody(std::vector<char>*);
private:
    char _nonce[16];

};

