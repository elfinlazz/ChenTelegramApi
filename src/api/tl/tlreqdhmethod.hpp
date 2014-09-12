#pragma once

#include "tlmethod.hpp"
#include "pqinnerdata.hpp"
#include "reqdhparams.hpp"

class TLReqDHMethod : TLMethod<PQInnerData, ReqDHParams>
{
public:
    TLReqDHMethod();

    ~TLReqDHMethod();
};