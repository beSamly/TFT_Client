using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNetworkManager
{
    private Client clientA = new Client();
    private Client clientB = new Client();
    private PacketController packetController = new PacketController();
    public void Init()
    {
        clientA.Init();
        clientA.ConnectToServer("127.0.0.1", 5000);
        clientA.OnConnect(() =>
        {

        });

        clientB.Init();
        clientB.ConnectToServer("127.0.0.1", 5000);
        clientB.OnConnect(() =>
        {

        });
    }

    public void HandlePacket(byte[] data)
    {
        packetController.HandlePacket(data);
    }

    public void SetRecvCallback(Action<byte[]> callback)
    {
        clientA.OnRecv(callback);
        clientB.OnRecv(callback);
    }
}
