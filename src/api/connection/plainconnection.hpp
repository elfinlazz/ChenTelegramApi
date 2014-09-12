#pragma once

#include <boost/asio.hpp>
#include <boost/array.hpp>

#include "connectioninfo.hpp"
#include "connectionstate.hpp"
#include "../tl/tlobject.hpp"
#include "../tl/pqreq.hpp"
#include "../tl/tlmethod.hpp"

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
        size_t reqLen = method->requiredLength;
        std::vector<char> recvVector;

        for (; ;)
        {
            boost::system::error_code error;
            boost::array<char, 256> buf;
            size_t len = socket.read_some(boost::asio::buffer(buf), error);
            for (int i = 0; i < len; i++)
                recvVector.push_back(buf[i]);

            if ((error && error == boost::asio::error::eof) || len == reqLen)
                break;
            else if (error)
                std::cout << error.message() << std::endl;

        }

        method->receiveObject(&recvVector);
        return method->recvObject;
    };

private:
    ConnectionInfo *info;
    boost::asio::io_service io_service;
    tcp::socket socket;
    bool fsent = false;
};

