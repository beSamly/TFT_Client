using Google.Protobuf;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Network
{
    public class PacketController
    {
        private MatchController matchController;
        private AuthController authController;

        //private Dictionary<int, Action<byte[]>> packetHandler = new Dictionary<int, Action<byte[]>>();
        private Dictionary<int, BaseController> controllerDic= new Dictionary<int, BaseController>();
        private int headerSize = Marshal.SizeOf(typeof(PacketHeader));
        public PacketController()
        {
            controllerDic.Add((int)PacketId.Prefix.AUTH, new AuthController());
            controllerDic.Add((int)PacketId.Prefix.MATCH, new MatchController());
        }
        public void HandlePacket(byte[] data)
        {
            PacketHeader header = GetHeader(data);

            if (controllerDic.ContainsKey(header.prefix))
            {
                controllerDic[header.prefix].HandlePacket(data);
            }
            else
            {
                //log error
            }
        }

        protected PacketHeader GetHeader(byte[] data)
        {
            if (headerSize > data.Length)
            {
                throw new Exception();
            }

            IntPtr ptr = Marshal.AllocHGlobal(headerSize);

            Marshal.Copy(data, 0, ptr, headerSize);

            PacketHeader header = (PacketHeader)Marshal.PtrToStructure(ptr, typeof(PacketHeader));

            Marshal.FreeHGlobal(ptr);

            return header;
        }
    }
}
