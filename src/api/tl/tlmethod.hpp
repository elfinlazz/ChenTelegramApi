#pragma once

#include "tlobject.hpp"

template<class TSend, class TRecv>
class TLMethod
{
public:
    virtual ~TLMethod()
    {
    }

    virtual void receiveObject(std::vector<char> *)
    {
    }

    TSend* sendObject;
    TRecv* recvObject;
};
