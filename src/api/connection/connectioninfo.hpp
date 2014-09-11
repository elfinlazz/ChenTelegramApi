#pragma once

#include <string>

struct ConnectionInfo
{
    ConnectionInfo(std::string h, std::string p) : host(h), port(p)
    {
    }

    std::string host = "";
    std::string port = "";
};

