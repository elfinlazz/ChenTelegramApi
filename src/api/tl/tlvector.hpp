#pragma once

#include "tlobject.hpp"

class TLVector : public TLObject
{
public:
    TLVector();

    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> &) override;

    virtual void deserializeBody(std::vector<uint8_t> &) override;

    std::vector<uint64_t> contents;
};