#include "socketutils.hpp"

unsigned char SocketUtils::readByte(tcp::socket *socket)
{
    boost::array<char, 1> arr;
    boost::system::error_code ignored;
    socket->read_some(boost::asio::buffer(arr), ignored);
    return (unsigned char) arr[0];
}

void SocketUtils::readByteArray(std::vector<char> *dest, int len, tcp::socket *socket)
{
    for(int i = 0; i < len; i++)
        dest->push_back(readByte(socket));
}
