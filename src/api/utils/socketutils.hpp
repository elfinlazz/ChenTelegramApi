#pragma once

#include <boost/asio.hpp>
#include <boost/array.hpp>

using boost::asio::ip::tcp;

class SocketUtils
{
public:
    static unsigned char readByte(tcp::socket *);

    static void readByteArray(std::vector<char> *, int, tcp::socket *);
};