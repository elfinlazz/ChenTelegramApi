#pragma once

#include <iostream>
#include <vector>

#include "tlobject.hpp"
#include "pq.hpp"

class PQReq : public TLObject
{
public:
    PQReq();

    ~PQReq();

    virtual int getClassId();

    virtual void serializeBody(std::vector<char> *);

    virtual void deserializeBody(std::vector<char> *);

    PQ pq;
};

