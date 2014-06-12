#include "authorizer.hpp"

#include "plainconnection.hpp"
#include "pqreq.hpp"
#include "streamingutils.hpp"

Authorizer::Authorizer(std::vector<ConnectionInfo>* infos)
{
    _infos = infos;
}

void Authorizer::attemptAuth()
{
    std::vector<ConnectionInfo>::iterator it = _infos->begin();
    int res = AUTH_FAILED;

    while (it != _infos->end() && res != AUTH_SUCCESS)
         res = doAuth(&*it++);

    std::cout << "Auth succeeded" << std::endl;
}

int Authorizer::doAuth(ConnectionInfo* info)
{
    PlainConnection connection(info);
    
    int conres = connection.connect();
    PQReq req;

    connection.send(&req);

    return AUTH_SUCCESS;
}

Authorizer::~Authorizer()
{
}
