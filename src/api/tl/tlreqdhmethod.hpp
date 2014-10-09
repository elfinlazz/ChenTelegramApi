#pragma once

#include "tlmethod.hpp"
#include "pqinnerdata.hpp"
#include "reqdhparams.hpp"
#include "serverdhparams.hpp"

class TLReqDHMethod : public TLMethod<ReqDHParams, ServerDHParams>
{
public:
    TLReqDHMethod();

    virtual void receiveObject(std::vector<uint8_t> &) override;
};