#pragma once

#include <boost/asio.hpp>
#include <boost/array.hpp>

using boost::asio::ip::tcp;

class SocketUtils
{
public:
    static uint8_t readByte(tcp::socket &);

    static void readByteArray(std::vector<uint8_t> &, int, tcp::socket &);
};