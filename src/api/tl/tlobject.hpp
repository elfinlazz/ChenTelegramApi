#pragma once

#include <iostream>
#include <vector>

class TLObject
{
public:
    virtual ~TLObject() = 0;

    void serialize(std::vector<char> *);

    void deserialize(std::vector<char> *);

private:
    virtual int getClassId() = 0;

    virtual void serializeBody(std::vector<char> *) = 0;

    virtual void deserializeBody(std::vector<char> *) = 0;
};

