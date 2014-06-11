#pragma once
#include <iostream>
#include <vector>

class TLObject
{
public:
    virtual ~TLObject() = 0;
    void serialize(std::vector<char>*);

private:
    virtual int getClassId() = 0;
    virtual void serializeBody(std::vector<char>*) = 0;
};

