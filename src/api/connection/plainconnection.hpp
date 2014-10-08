#pragma once

#include <boost/asio.hpp>
#include <boost/array.hpp>

#include "connectioninfo.hpp"
#include "connectionstate.hpp"
#include "../tl/tlobject.hpp"
#include "../tl/pqreq.hpp"
#include "../tl/tlmethod.hpp"
#include "../utils/socketutils.hpp"

using boost::asio::ip::tcp;

class PlainConnection
{
public:
    PlainConnection(ConnectionInfo *);

    ~PlainConnection();

    ConnectionState connect();

    void send(TLObject *);

    template<class TSend, class TRecv>
    TRecv *executeMethod(TLMethod<TSend, TRecv> *method)
    {
        send(method->sendObject);
        std::vector<uint8_t> recvVector;

        int headerLen = (int) SocketUtils::readByte(&socket);
        if (headerLen == 0x7F)
        {
            headerLen = SocketUtils::readByte(&socket);
            headerLen += SocketUtils::readByte(&socket) << 8;
            headerLen += SocketUtils::readByte(&socket) << 16;
        }
        int len = headerLen * 4;
        SocketUtils::readByteArray(&recvVector, len, &socket);

        method->receiveObject(&recvVector);
        return method->recvObject;
    };

private:
    ConnectionInfo *info;
    boost::asio::io_service io_service;
    tcp::socket socket;
    bool fsent = false;
};

