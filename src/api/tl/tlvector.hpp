#pragma once

#include "tlobject.hpp"

class TLVector : public TLObject
{
public:
    TLVector();

    ~TLVector();

    std::vector<int64_t> contents;
private:
    virtual int getClassId() override;

    virtual void serializeBody(std::vector<char> *vector) override;

    virtual void deserializeBody(std::vector<char> *vector) override;
};