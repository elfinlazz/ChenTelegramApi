#include "plainconnection.hpp"

#include <stdio.h>
#include <sstream>
#include <iostream>
#include <vector>
#include <boost/array.hpp>

#include "streamingutils.hpp"

PlainConnection::PlainConnection(ConnectionInfo* info) : _socket(_io_service)
{
    _info = info;
}

PlainConnection::~PlainConnection()
{
}

int PlainConnection::connect()
{
    tcp::resolver resolver(_io_service);
    tcp::resolver::query query(_info->getHost(), _info->getPort());

    tcp::resolver::iterator it = resolver.resolve(query);
    tcp::resolver::iterator end;

    boost::system::error_code error = boost::asio::error::host_not_found;

    while (error && it != end)
    {
        _socket.close();
        _socket.connect(*it++, error);
    }

    if (error)
        return CONN_FAILED;

    // handshake with abridged protocol
    int buf[1] = { 0xEF };
    _socket.write_some(boost::asio::buffer(buf), error);

    return CONN_SUCCESS;
}

void PlainConnection::sendPQReq(PQReq* obj)
{
    std::vector<char> packet;
    std::vector<char> objPacket;
    obj->serializePQRequest(&objPacket);
    int len = objPacket.size() / 4;

    if (len >= 0x7F)
    {
        StreamingUtils::writeByte(0x7F, &packet);
        StreamingUtils::writeByte(len & 0xFF, &packet);
        StreamingUtils::writeByte((len >> 8) & 0xFF, &packet);
        StreamingUtils::writeByte((len >> 16) & 0xFF, &packet);
    }
    else
    {
        StreamingUtils::writeByte(len, &packet);
    }

    StreamingUtils::writeVector(&objPacket, &packet);
    boost::system::error_code error;

    write(_socket, boost::asio::buffer(packet), error);

    std::cout << error.message() << std::endl;

    for (;;)
    {
        boost::array<char, 256> buf;
        size_t len = _socket.read_some(boost::asio::buffer(buf));
    }
}

void PlainConnection::send(TLObject* obj)
{
    std::vector<char> packet;
    std::vector<char> objPacket;
    obj->serialize(&objPacket);
    int len = objPacket.size() / 4;

    if (len >= 0x7F)
    {
        StreamingUtils::writeByte(0x7F, &packet);
        StreamingUtils::writeByte(len & 0xFF, &packet);
        StreamingUtils::writeByte((len >> 8) & 0xFF, &packet);
        StreamingUtils::writeByte((len >> 16) & 0xFF, &packet);
    }
    else
    {
        StreamingUtils::writeByte(len, &packet);
    }

    StreamingUtils::writeVector(&objPacket, &packet);

    _socket.write_some(boost::asio::buffer(packet));

    for (;;)
    {
        boost::array<char, 256> buf;
        size_t len = _socket.read_some(boost::asio::buffer(buf));
    }
}
