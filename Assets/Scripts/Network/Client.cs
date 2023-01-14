using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

namespace Network
{
    public class Client
    {
        public TCP tcp;
     
        public void Init()
        {
            tcp = new TCP();
        }

        public void OnConnect(Action callback)
        {
            tcp.OnConnect(callback);
        }

        public void OnRecv(Action<byte[]> callback)
        {
            tcp.OnRecv(callback);
        }

        public void ConnectToServer(string address, int port)
        {
            tcp.Connect(address, port);
        }

        public void SendToServer(byte[] dataBuffer)
        {
            tcp.SendData(dataBuffer);
        }

        public class TCP
        {
            public int dataBufferSize = 4096;

            public TcpClient socket;

            private NetworkStream stream;
            private byte[] receiveBuffer;
            private Action<byte[]> OnRecvCallback;
            private Action OnConnectCallback;
            public void OnRecv(Action<byte[]> OnRecvCallback)
            {
                this.OnRecvCallback = OnRecvCallback;
            }

            public void OnConnect(Action callback)
            {
                this.OnConnectCallback = callback;
            }

            public void Connect(string address, int port)
            {
                socket = new TcpClient
                {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];
                socket.BeginConnect(address, port, ConnectCallback, socket);
            }

            private void ConnectCallback(IAsyncResult _result)
            {
                socket.EndConnect(_result);

                if (!socket.Connected)
                {
                    return;
                }

                stream = socket.GetStream();

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                OnConnectCallback();
            }

            public void SendData(byte[] dataBuffer)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(dataBuffer, 0, dataBuffer.Length, null, null);
                    }
                }
                catch (Exception _ex)
                {
                    Debug.Log($"Error sending data to server via TCP: {_ex}");
                }
            }

            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        // TODO: disconnect
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    HandleData(_data);
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch(Exception ex)
                {
                    Debug.LogError(ex);
                    // TODO: disconnect
                }
            }

            private bool HandleData(byte[] _data)
            {
                int _packetLength = 0;
               
                //TODO 여기 callback으로 바꾸기
                this.OnRecvCallback(_data);
                return true;
            }
        }

        //public class UDP
        //    {
        //        public UdpClient socket;
        //        public IPEndPoint endPoint;

        //        public UDP()
        //        {
        //            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        //        }

        //        public void Connect(int _localPort)
        //        {
        //            socket = new UdpClient(_localPort);

        //            socket.Connect(endPoint);
        //            socket.BeginReceive(ReceiveCallback, null);

        //            //TODO Connect 후 로그인 패킷을 날려보자
        //            using (Packet _packet = new Packet())
        //            {
        //                SendData(_packet);
        //            }
        //        }

        //        public void SendData(Packet _packet)
        //        {
        //            try
        //            {
        //                _packet.InsertInt(instance.myId);
        //                if (socket != null)
        //                {
        //                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
        //                }
        //            }
        //            catch (Exception _ex)
        //            {
        //                Debug.Log($"Error sending data to server via UDP: {_ex}");
        //            }
        //        }

        //        // 서버로 부터 데이터를 받았을 떄 호출되는 Callback
        //        private void ReceiveCallback(IAsyncResult _result)
        //        {
        //            try
        //            {
        //                byte[] _data = socket.EndReceive(_result, ref endPoint);
        //                socket.BeginReceive(ReceiveCallback, null);

        //                //PacketHeader보다 사이즈 작을 순 없음.
        //                if (_data.Length < 32)
        //                {
        //                    // TODO: disconnect
        //                    return;
        //                }

        //                HandleData(_data);
        //            }
        //            catch
        //            {
        //                // TODO: disconnect
        //            }
        //        }

        //        private void HandleData(byte[] _data)
        //        {
        //            //TODO 데이터에서 PacketHeader 부분 제거 후 바디 부분 가져오기.
        //            using (Packet _packet = new Packet(_data))
        //            {
        //                int _packetLength = _packet.ReadInt();
        //                _data = _packet.ReadBytes(_packetLength);
        //            }

        //            ThreadManager.ExecuteOnMainThread(() =>
        //            {
        //                using (Packet _packet = new Packet(_data))
        //                {
        //                    int _packetId = _packet.ReadInt();
        //                    //TODO PacketHandler로 데이터 넘기기
        //                    //PacketManager.OnPacket(_packetId, );
        //                    packetHandlers[_packetId](_packet);
        //                }
        //            });
        //        }
        //    }
    }
}
