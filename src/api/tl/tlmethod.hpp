#pragma once

#include "tlobject.hpp"

template<class T>
class TLMethod
{
public:
    virtual ~TLMethod()
    {
    }

    virtual void receiveObject(std::vector<char> *)
    {
    }

    TLObject* sendObject;
    T* recvObject;
    size_t requiredLength;
};
