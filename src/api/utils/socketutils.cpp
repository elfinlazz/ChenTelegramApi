#include "socketutils.hpp"

uint8_t SocketUtils::readByte(tcp::socket *socket)
{
    boost::array<uint8_t, 1> arr;
    boost::system::error_code ignored;
    socket->read_some(boost::asio::buffer(arr), ignored);
    return arr[0];
}

void SocketUtils::readByteArray(std::vector<uint8_t> *dest, int len, tcp::socket *socket)
{
    for(int i = 0; i < len; i++)
        dest->push_back(readByte(socket));
}
