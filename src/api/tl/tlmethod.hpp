#pragma once

#include "tlobject.hpp"

template<class TSend, class TRecv>
class TLMethod
{
public:
    virtual void receiveObject(std::vector<uint8_t> &)
    {
    }

    // TODO: reference possible?
    TSend *sendObject;
    TRecv *recvObject;
};
