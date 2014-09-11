#include <boost/array.hpp>
#include "plainconnection.hpp"
#include "../utils/streamingutils.hpp"

PlainConnection::PlainConnection(ConnectionInfo *info) : socket(io_service)
{
    this->info = info;
}

PlainConnection::~PlainConnection()
{
}

ConnectionState PlainConnection::connect()
{
    tcp::resolver resolver(io_service);
    tcp::resolver::query query(info->host, info->port);

    tcp::resolver::iterator it = resolver.resolve(query);
    tcp::resolver::iterator end;

    boost::system::error_code error = boost::asio::error::host_not_found;

    while (error && it != end)
    {
        socket.close();
        socket.connect(*it++, error);
    }

    return error ? ConnectionState::FAILED : ConnectionState::CONNECTED;
}

void PlainConnection::send(TLObject *obj)
{
    std::vector<char> packet;
    std::vector<char> objPacket;
    obj->serialize(&objPacket);
    int len = objPacket.size() / 4;

    if (!fsent)
    {
        StreamingUtils::writeByte(0xEF, &packet);
        fsent = true;
    }

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
    socket.write_some(boost::asio::buffer(packet), error);
}
