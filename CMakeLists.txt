cmake_minimum_required(VERSION 2.8.4)
project(TelegramApi)

set(SOURCE_FILES
# API #
src/api/authentication/authenticationstate.hpp
src/api/authentication/authorizer.hpp src/api/authentication/authorizer.cpp
src/api/connection/connectionstate.hpp
src/api/connection/connectioninfo.hpp
src/api/connection/plainconnection.hpp src/api/connection/plainconnection.cpp

# TL #
src/api/tl/pq.hpp src/api/tl/pq.cpp
src/api/tl/pqreq.hpp src/api/tl/pqreq.cpp
src/api/tl/pqres.hpp src/api/tl/pqres.cpp
src/api/tl/tlmethod.hpp
src/api/tl/tlobject.hpp src/api/tl/tlobject.cpp
src/api/tl/tlreqpqmethod.hpp src/api/tl/tlreqpqmethod.cpp
src/api/tl/tlvector.hpp src/api/tl/tlvector.cpp

# UTILS #
src/api/utils/streamingutils.hpp src/api/utils/streamingutils.cpp

src/main.cpp)

set(CMAKE_CXX_FLAGS "-D_WIN32_WINNT=0x0501 -g -std=c++11 -Wall -Wno-unused-local-typedefs")
set(CMAKE_EXE_LINKER_FLAGS "-static-libgcc -static-libstdc++")

# BOOST #
set(Boost_USE_STATIC_LIBS OFF)
set(Boost_USE_MULTITHREADED ON)
set(Boost_USE_STATIC_RUNTIME OFF)
find_package(Boost COMPONENTS system REQUIRED)

if(Boost_FOUND)
    include_directories(${Boost_INCLUDE_DIRS})
    add_executable(TelegramApi ${SOURCE_FILES})
    if(WIN32)
      target_link_libraries(TelegramApi wsock32 ws2_32)
    endif()
    target_link_libraries(TelegramApi ${Boost_LIBRARIES})
endif()
