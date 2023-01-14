using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameSystem : MonoBehaviour
{
    public DebugNetworkManager networkManager;
    private ThreadManager threadManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        networkManager = new DebugNetworkManager();
        threadManager = new ThreadManager();
    }

    private void Start()
    {
        networkManager.Init();
        networkManager.SetRecvCallback(OnRecvPacket);
    }

    private void Update()
    {
        threadManager.Update();
    }

    private void OnRecvPacket(byte[] data)
    {
        if (data.Length < 4)
        {
            //length should be bigger than header size
            return;
        }

        threadManager.ExecuteOnMainThread(() =>
        {
            networkManager.HandlePacket(data);
        });
    }
}
