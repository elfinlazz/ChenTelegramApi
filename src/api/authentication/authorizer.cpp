#include "authorizer.hpp"

#include <boost/multiprecision/miller_rabin.hpp>

#include "../connection/plainconnection.hpp"
#include "../tl/tlreqpqmethod.hpp"
#include "pqsolver.hpp"
#include "../tl/pqinnerdata.hpp"
#include "../utils/randomutils.hpp"
#include "../tl/tlreqdhmethod.hpp"

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

    TLReqPQMethod reqPQMethod;
    PQRes *res = connection.executeMethod(&reqPQMethod);
    PQSolver solver;
    uint64_t p, q;
    solver.solvePQ(res->pq, &p, &q);
    //TLReqDHMethod reqDHMethod;
    //reqDHMethod.sendObject->pq = res->pq;
    //reqDHMethod.sendObject->p = p;
    //reqDHMethod.sendObject->q = q;
    //reqDHMethod.sendObject->nonce = res->nonce;
    //reqDHMethod.sendObject->serverNonce = res->serverNonce;
    //RandomUtils::nextBytes(reqDHMethod.sendObject->newNonce, 32);
    //ReqDHParams params = connection.executeMethod(&reqDHMethod);

    return AuthenticationState::AUTHENTICATED;
}

Authorizer::~Authorizer()
{
}
