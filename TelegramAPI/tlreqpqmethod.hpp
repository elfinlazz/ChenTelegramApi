#pragma once

#include "tlmethod.hpp"
#include "pqres.hpp"

class TLReqPQMethod : public TLMethod<PQRes>
{
public:
    TLReqPQMethod();
    ~TLReqPQMethod();
};
