#include <sstream>
#include <iostream>
#include <vector>

#include <boost/asio.hpp>

#include "api/connection/connectioninfo.hpp"
#include "api/authentication/authorizer.hpp"

using boost::asio::ip::tcp;

int main(int argc, char *argv[])
{
    std::vector<ConnectionInfo> infos;
    ConnectionInfo info{"173.240.5.253", "25"};
    infos.push_back(info);

    Authorizer auth(infos);
    auth.attemptAuth();
}