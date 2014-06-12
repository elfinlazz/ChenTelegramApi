#pragma once

#include <string>

class ConnectionInfo
{
public:
    ConnectionInfo(std::string, std::string);
    ~ConnectionInfo();

    std::string getHost();
    std::string getPort();

private:
    std::string _host;
    std::string _port;
};

