#include "tlobject.hpp"

#pragma once

class ServerDHParams : public TLObject
{

public:
    virtual ~ServerDHParams();

private:
    virtual uint32_t getClassId() override;

    virtual void serializeBody(std::vector<uint8_t> *vector) override;

    virtual void deserializeBody(std::vector<uint8_t> *vector) override;
};