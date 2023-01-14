using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using Protocol;
using Google.Protobuf;
using System.Runtime.InteropServices;

namespace Network
{
    public class NetworkManager
    {
        private Client client = new Client();
        private PacketController packetController = new PacketController();

        public void Init()
        {
            client.Init();
            client.ConnectToServer("127.0.0.1", 3000);
        }

        public void HandlePacket(byte[] data)  
        {
            packetController.HandlePacket(data);
        }

        public void SetRecvCallback(Action<byte[]> callback)
        {
            client.OnRecv(callback);
        }

        public void SetOnConnectCallback(Action callback)
        {
            client.OnConnect(callback);
        }

        public void SendLoginRequest(string email)
        {
            LoginRequest req = new LoginRequest();
            req.Email = email;
            req.Password = "password";

            Packet packet = new Packet((int)PacketId.Prefix.AUTH, (int)PacketId.Auth.LOGIN_REQ);
            packet.WriteData(req.ToByteArray());
            this.client.SendToServer(packet.ToBuffer());

            // deserialize
            //MessageParser<LoginRequest> parser = LoginRequest.Parser;
            //LoginRequest receivedRequest = parser.ParseFrom(byteData);
        }

        public void SendMatchRequest()
        {
            MatchRequest req = new MatchRequest();

            Packet packet = new Packet((int)PacketId.Prefix.MATCH, (int)PacketId.Match.MATCH_REQ);
            packet.WriteData(req.ToByteArray());
            this.client.SendToServer(packet.ToBuffer());
        }

        public void SendMatchAcceptRequest()
        {
            Packet packet = new Packet((int)PacketId.Prefix.MATCH, (int)PacketId.Match.MATCH_ACCEPT_REQ);
            packet.WriteData();
            this.client.SendToServer(packet.ToBuffer());
        }

        public void SendMatchCancelRequest()
        {
            Packet packet = new Packet((int)PacketId.Prefix.MATCH, (int)PacketId.Match.MATCH_CANCEL_REQ);
            packet.WriteData();
            this.client.SendToServer(packet.ToBuffer());
        }
    }
}
