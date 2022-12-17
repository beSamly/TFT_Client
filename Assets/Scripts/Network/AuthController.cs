using Google.Protobuf;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Network
{
    public class AuthController : BaseController
    {
        private Dictionary<int, Action<byte[]>> packetHandler = new Dictionary<int, Action<byte[]>>();

        public AuthController()
        {
            packetHandler.Add((int)PacketId.Auth.LOGIN_RES, HandleLoginRes);
        }
        public override void HandlePacket(byte[] data)
        {
            PacketHeader header = GetHeader(data);

            if (packetHandler.ContainsKey(header.id))
            {
                packetHandler[header.id].Invoke(data);
            }
            else
            {
                //log error
            }
        }
        private void HandleLoginRes(byte[] data)
        {
            MessageParser<LoginResponse> parser = LoginResponse.Parser;
            LoginResponse response = parser.ParseFrom(data, headerSize, data.Length - headerSize);
            if (response.Result == true)
            {
                //TODO : move to next lobby
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                //error
            }
        }

    }
}
