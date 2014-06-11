#pragma once

#include "tlobject.hpp"

class PQRes : public TLObject
{
public:
    PQRes();
    ~PQRes();

    virtual int getClassId();
    virtual void serializeBody(std::vector<char>*);
};

