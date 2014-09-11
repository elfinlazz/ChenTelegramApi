#pragma once

#include "tlobject.hpp"

class TLVector : public TLObject
{
public:
    TLVector();

    ~TLVector();

    std::vector<long> contents;
};