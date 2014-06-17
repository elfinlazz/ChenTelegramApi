#include <stdio.h>

#include <regex>

#include <boost/array.hpp>
#include <boost/asio.hpp>
#include <boost/asio/ssl.hpp>
#include <boost/algorithm/string.hpp>
#include <boost/property_tree/ptree.hpp>
#include <boost/property_tree/json_parser.hpp>
#include <boost/foreach.hpp>
#include <sys/stat.h>

using boost::asio::ip::tcp;
namespace ssl = boost::asio::ssl;

void writeJson(boost::property_tree::ptree pt)
{
    BOOST_FOREACH(boost::property_tree::ptree::value_type &v, pt)
    {
        std::cout << v.first << std::endl;
        writeJson(v.second);
    }
}

int main(int argc, char* argv[])
{
    const int BUFSIZE = 512;
    const char* FILENAME = "schema.json";

    struct stat buffer;
    if (stat(FILENAME, &buffer) != 0)
    {
        boost::asio::io_service io_service;

        tcp::resolver resolver(io_service);
        tcp::resolver::query query("core.telegram.org", "443");

        tcp::resolver::iterator it = resolver.resolve(query);
        tcp::resolver::iterator end;

        ssl::context ctx(ssl::context::sslv23);
        ctx.set_verify_mode(ssl::verify_none);
        ssl::stream<tcp::socket> ssl_socket(io_service, ctx);
        tcp::socket::lowest_layer_type& sock = ssl_socket.lowest_layer();

        boost::system::error_code error = boost::asio::error::host_not_found;

        while (error && it != end)
        {
            sock.close();
            sock.connect(*it++, error);
        }

        if (error)
        {
            std::cout << error.message() << std::endl;
            return -1;
        }

        ssl_socket.handshake(ssl_socket.client);

        std::string req = "GET /schema/json HTTP/1.1\n";
        req += "Host: core.telegram.org\n";
        req += "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0\n";
        req += "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\n";
        req += "Cache-Control: max-age=0\n";
        req += "\n";
        ssl_socket.write_some(boost::asio::buffer(req), error);
        std::cout << error.message() << std::endl;

        boost::array<char, BUFSIZE> buf;
        int len;
        bool header = false;
        std::string json;

        std::ofstream file(FILENAME, std::ios::out | std::ios::trunc);

        do
        {
            len = ssl_socket.read_some(boost::asio::buffer(buf), error);
            std::string str(buf.c_array(), len);
            if (!header)
            {
                int p = str.find_last_of("\n");
                if (p++ >= 0)
                {
                    file << str.substr(p, str.length() - p);
                    header = true;
                    continue;
                }
            }
            file << str;
        } while (len == BUFSIZE);

        file.close();
    }

    boost::property_tree::ptree pt;
    read_json(FILENAME, pt);

    try
    {
        writeJson(pt);
    }
    catch (std::exception const& e)
    {
        std::cerr << e.what() << std::endl;
    }
}