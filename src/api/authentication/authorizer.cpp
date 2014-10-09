#include "authorizer.hpp"

#include <boost/multiprecision/miller_rabin.hpp>

#include "../connection/plainconnection.hpp"
#include "../tl/tlreqpqmethod.hpp"
#include "pqsolver.hpp"
#include "../tl/pqinnerdata.hpp"
#include "../utils/randomutils.hpp"
#include "../tl/tlreqdhmethod.hpp"

Authorizer::Authorizer(std::vector<ConnectionInfo> &pInfoList)
        : infoList(pInfoList)
{
}

AuthenticationState Authorizer::attemptAuth()
{
    std::vector<ConnectionInfo>::iterator it = infoList.begin();
    AuthenticationState res = AuthenticationState::UNKNOWN;

    while (it != infoList.end() && res != AuthenticationState::AUTHENTICATED)
        res = doAuth(*it++);

    return res;
}

AuthenticationState Authorizer::doAuth(ConnectionInfo &info)
{
    PlainConnection connection(info);

    ConnectionState result = connection.connect();
    if (result == ConnectionState::FAILED)
        return AuthenticationState::REJECTED;

    TLReqPQMethod reqPQMethod;
    PQRes *res = connection.executeMethod(reqPQMethod);
    PQSolver solver;
    uint32_t p, q;
    solver.solvePQ(res->pq, p, q);
    TLReqDHMethod reqDHMethod;

    PQInnerData innerData(res->pq, p, q, res->nonce, res->serverNonce);
    RandomUtils::nextBytes(innerData.newNonce, 32);

    // TODO: encrypt innerData

    return AuthenticationState::AUTHENTICATED;
}

Authorizer::~Authorizer()
{
}
