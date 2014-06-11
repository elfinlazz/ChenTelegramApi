#pragma once

#include <string>
#include <vector>

#include "connectioninfo.hpp"

#define AUTH_SUCCESS 0x00
#define AUTH_FAILED 0x01

class Authorizer
{
public:
    Authorizer(std::vector<ConnectionInfo>*);
    ~Authorizer();

    void attemptAuth();

private:
    int doAuth(ConnectionInfo*);

    std::vector<ConnectionInfo>* _infos;
};

