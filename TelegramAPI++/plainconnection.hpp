#pragma once

#include <boost/asio.hpp>

#include "connectioninfo.hpp"
#include "tlobject.hpp"
#include "pqreq.hpp"

using boost::asio::ip::tcp;

#define CONN_SUCCESS 0x00
#define CONN_FAILED 0x01

class PlainConnection
{
public:
    PlainConnection(ConnectionInfo*);
    ~PlainConnection();

    int connect();
    void send(TLObject*);

private:
    ConnectionInfo* _info;
    boost::asio::io_service _io_service;
    tcp::socket _socket;
    bool _fsent = false;
};

