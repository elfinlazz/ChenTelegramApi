﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using TelegramApi.MTProto.Utils;
using TelegramApi.TLCore;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.MTProto.Connection
{
    public sealed class PlainConnection : IPlainConnection, IDisposable
    {
        private readonly IConnectionInfo _connectionInfo;
        private TcpClient _tcpClient;
        private bool _efSent;

        public PlainConnection(IConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;
            _tcpClient = new TcpClient();
        }

        public async Task<ConnectionState> ConnectAsync()
        {
            await _tcpClient.ConnectAsync(_connectionInfo.Host, _connectionInfo.Port);
            return _tcpClient.Connected ? ConnectionState.Connected : ConnectionState.Failed;
        }

        public async Task<TRecv> ExecuteMethodAsync<TSend, TRecv>(TLMethod<TSend, TRecv> method)
            where TSend : TLObject, new()
            where TRecv : TLObject, new()
        {
            await SendObjectAsync(method.SendObject);

            int headerLen = _tcpClient.GetStream().ReadByte();
            if (headerLen == 0x7F)
            {
                headerLen = _tcpClient.GetStream().ReadByte();
                headerLen += _tcpClient.GetStream().ReadByte() << 8;
                headerLen += _tcpClient.GetStream().ReadByte() << 16;
            }
            headerLen *= 4;

            Console.WriteLine("....receiving....");
            byte[] recvArr = new byte[headerLen];
            await _tcpClient.GetStream().ReadAsync(recvArr, 0, headerLen);
            DebugUtils.DumpBytes(recvArr);
            Console.WriteLine("\n....done....");

            method.ReceiveObject = TLRootSerializer.Deserialize<TRecv>(recvArr.ToList());

            return method.ReceiveObject;
        }

        public async Task SendObjectAsync<T>(T obj)
            where T : TLObject, new()
        {
            List<byte> packet = new List<byte>();
            byte[] objArr = TLRootSerializer.Serialize(obj).ToArray();
            byte len = (byte)(objArr.Length / 4);

            if (!_efSent)
            {
                packet.Add(0xEF);
                _efSent = true;
            }

            if (len >= 0x7F)
            {
                packet.Add(0x7F);
                packet.Add((byte)(len & 0xFF));
                packet.Add((byte)((len >> 8) & 0xFF));
                packet.Add((byte)((len >> 16) & 0xFF));
            }
            else
            {
                packet.Add(len);
            }

            packet.AddRange(objArr);

            Console.WriteLine("....sending....");
            DebugUtils.DumpBytes(packet.ToArray());
            await _tcpClient.GetStream().WriteAsync(packet.ToArray(), 0, packet.Count);
            Console.WriteLine("\n....done....");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_tcpClient != null)
                {
                    ((IDisposable)_tcpClient).Dispose();
                    _tcpClient = null;
                }
            }
        }
    }

    public enum ConnectionState
    {
        Connected,
        Failed
    }
}