using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Protocol;
using UnityEngine.SceneManagement;

namespace Network
{
    public abstract class BaseController
    {
        public abstract void HandlePacket(byte[] data);

        protected int headerSize = Marshal.SizeOf(typeof(PacketHeader));
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
