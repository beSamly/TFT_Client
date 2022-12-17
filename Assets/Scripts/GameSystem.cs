using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class GameSystem : MonoBehaviour
    {
        public NetworkManager networkManager;
        private ThreadManager threadManager;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            networkManager = new NetworkManager();
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
                //packetController.HandlePacket(data);
            });
        }
    }
}
