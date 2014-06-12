#pragma once

#include <boost/asio.hpp>

#include "connectioninfo.hpp"
#include "tlobject.hpp"
#include "pqreq.hpp"
#include "tlmethod.hpp"

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

    template<class T>
    T executeMethod(TLMethod<T>*);

private:
    ConnectionInfo* _info;
    boost::asio::io_service _io_service;
    tcp::socket _socket;
    bool _fsent = false;
};

