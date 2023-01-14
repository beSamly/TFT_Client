using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class GameSystem : MonoBehaviour
    {
        public NetworkManager networkManager;
        private ThreadManager threadManager;
        private Action[] actionList;
 
        private void Awake()
        {
            DontDestroyOnLoad(this);

            networkManager = new NetworkManager();
            threadManager = new ThreadManager();
        }

        private void Start()
        {
            networkManager.Init();
            networkManager.SetOnConnectCallback(OnConnectToServer);
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

        private void OnConnectToServer()
        {
            threadManager.ExecuteOnMainThread(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
            });
        }
    }
}
