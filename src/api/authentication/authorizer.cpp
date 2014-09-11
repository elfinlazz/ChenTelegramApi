#include "authorizer.hpp"

#include "../connection/plainconnection.hpp"
#include "../tl/tlreqpqmethod.hpp"

Authorizer::Authorizer(std::vector<ConnectionInfo> *infoList)
{
    this->infoList = infoList;
}

AuthenticationState Authorizer::attemptAuth()
{
    std::vector<ConnectionInfo>::iterator it = infoList->begin();
    AuthenticationState res = AuthenticationState::UNKNOWN;

    while (it != infoList->end() && res != AuthenticationState::AUTHENTICATED)
        res = doAuth(&*it++);

    return res;
}

AuthenticationState Authorizer::doAuth(ConnectionInfo *info)
{
    PlainConnection connection(info);

    ConnectionState result = connection.connect();
    if (result == ConnectionState::FAILED)
        return AuthenticationState::REJECTED;

    TLReqPQMethod method;
    PQRes *res = connection.executeMethod(&method);

    return AuthenticationState::AUTHENTICATED;
}

Authorizer::~Authorizer()
{
}
