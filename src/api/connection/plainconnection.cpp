#include <boost/array.hpp>
#include "plainconnection.hpp"

PlainConnection::PlainConnection(ConnectionInfo &pInfo)
        : info(pInfo), socket(io_service)
{
}

PlainConnection::~PlainConnection()
{
}

ConnectionState PlainConnection::connect()
{
    tcp::resolver resolver(io_service);
    tcp::resolver::query query(info.host, info.port);

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
    std::vector<uint8_t> packet;
    std::vector<uint8_t> objPacket;
    obj->serialize(objPacket);
    uint8_t len = (uint8_t) (objPacket.size() / 4);

    if (!fsent)
    {
        StreamingUtils::writeByte(0xEF, packet);
        fsent = true;
    }

    if (len >= 0x7F)
    {
        StreamingUtils::writeByte((uint8_t) 0x7F, packet);
        StreamingUtils::writeByte((uint8_t) (len & 0xFF), packet);
        StreamingUtils::writeByte((uint8_t) ((len >> 8) & 0xFF), packet);
        StreamingUtils::writeByte((uint8_t) ((len >> 16) & 0xFF), packet);
    }
    else
    {
        StreamingUtils::writeByte(len, packet);
    }

    StreamingUtils::writeVector(objPacket, packet);

    StreamingUtils::DumpVector("Sending", packet);

    boost::system::error_code error;
    socket.write_some(boost::asio::buffer(packet), error);
}
