#include "connectioninfo.hpp"

ConnectionInfo::ConnectionInfo(std::string host, std::string port)
{
    _host = host;
    _port = port;
}

ConnectionInfo::~ConnectionInfo()
{
}

std::string ConnectionInfo::getHost()
{
    return _host;
}
std::string ConnectionInfo::getPort()
{
    return _port;
}
