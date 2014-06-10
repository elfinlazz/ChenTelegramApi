#pragma once

#include <iostream>
#include <vector>

#include "tlobject.hpp"

class PQReq : public TLObject
{
public:
    PQReq();
    ~PQReq();

    void serializePQRequest(std::vector<char>*);
    virtual int getClassId();
    virtual void serializeBody(std::vector<char>*);
private:
    char _nonce[16];
};

