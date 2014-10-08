#pragma once

#include "tlobject.hpp"

template<class TSend, class TRecv>
class TLMethod
{
public:
    virtual ~TLMethod()
    {
    }

    virtual void receiveObject(std::vector<uint8_t> *)
    {
    }

    TSend* sendObject;
    TRecv* recvObject;
};
