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

    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> *) override;

    virtual void deserializeBody(std::vector<uint8_t> *) override;

    PQ pq;
};

