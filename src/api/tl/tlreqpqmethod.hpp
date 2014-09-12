#pragma once

#include "tlmethod.hpp"
#include "pqreq.hpp"
#include "pqres.hpp"

class TLReqPQMethod : public TLMethod<PQReq, PQRes>
{
public:
    TLReqPQMethod();

    ~TLReqPQMethod();

    virtual void receiveObject(std::vector<char> *vector) override;
};
