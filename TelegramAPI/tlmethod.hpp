#pragma once

#include "tlobject.hpp"

template<class T>
class TLMethod
{
public:
    virtual ~TLMethod() { }
    T getResponse() { return _response; }

protected:
    T _response;
};
