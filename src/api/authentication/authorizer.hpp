#pragma once

#include <string>
#include <vector>

#include "authenticationstate.hpp"
#include "../connection/connectioninfo.hpp"

class Authorizer
{
public:
    Authorizer(std::vector<ConnectionInfo> *);

    ~Authorizer();

    AuthenticationState attemptAuth();

private:
    AuthenticationState doAuth(ConnectionInfo *);

    std::vector<ConnectionInfo> *infoList;
};

