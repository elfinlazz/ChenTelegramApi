#pragma once

#include "tlobject.hpp"

class TLVector : public TLObject
{
public:
    TLVector();

    ~TLVector();

    std::vector<uint64_t> contents;

private:
    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> *vector) override;

    virtual void deserializeBody(std::vector<uint8_t> *vector) override;
};