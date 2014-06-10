#include "authorizer.hpp"

#include "plainconnection.hpp"
#include "pqreq.hpp"

Authorizer::Authorizer(std::vector<ConnectionInfo>* infos)
{
    _infos = infos;
}

void Authorizer::attemptAuth()
{
    std::vector<ConnectionInfo>::iterator it = _infos->begin();

    for (;;)
    {
        int res = doAuth(&*it++);
        if (it == _infos->end())
            break;
    }
}

int Authorizer::doAuth(ConnectionInfo* info)
{
    PlainConnection connection(info);
    
    int conres = connection.connect();
    PQReq req;

    connection.sendPQReq(&req);

    return AUTH_SUCCESS;
}

Authorizer::~Authorizer()
{
}
