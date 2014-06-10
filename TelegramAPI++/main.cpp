#include <stdio.h>
#include <sstream>
#include <iostream>
#include <vector>

#include "streamingutils.hpp"
#include "pqreq.hpp"

#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x0501
#endif 

#include <boost/asio.hpp>
#include <boost/array.hpp>

#include "connectioninfo.hpp"
#include "authorizer.hpp"

using boost::asio::ip::tcp;

int main(int argc, char* argv[])
{
    std::vector<ConnectionInfo> infos;
    ConnectionInfo info("173.240.5.253", "25");
    infos.insert(infos.end(), info);

    Authorizer auth(&infos);
    auth.attemptAuth();
}