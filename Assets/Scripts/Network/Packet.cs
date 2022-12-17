using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class Packet
    {
        private byte[] buffer;
        private int packetId;
        private int prefix;

        public Packet(int pfix, int id)
        {
            prefix = pfix;
            packetId = id;
        }

        public void WriteData(byte[] data)
        {
            PacketHeader header;
            header.size = 0;
            header.id = 0;
            header.prefix = 0;

            int headerLength = Marshal.SizeOf(header);
            int dataLength = data.Length;
            header.id = packetId;
            header.prefix = prefix;
            header.size = (headerLength + dataLength);

            byte[] byteHeader = new byte[headerLength];
            IntPtr ptr = Marshal.AllocHGlobal(headerLength);
            Marshal.StructureToPtr(header, ptr, true);
            Marshal.Copy(ptr, byteHeader, 0, headerLength);
            Marshal.FreeHGlobal(ptr);

            byte[] returnByte = new byte[byteHeader.Length + data.Length];
            Buffer.BlockCopy(byteHeader, 0, returnByte, 0, byteHeader.Length);
            Buffer.BlockCopy(data, 0, returnByte, byteHeader.Length, data.Length);

            buffer = returnByte;
        }

        public void WriteData()
        {
            PacketHeader header;
            header.size = 0;
            header.id = 0;
            header.prefix = 0;

            int headerLength = Marshal.SizeOf(header);
            int dataLength = 0;
            header.id = packetId;
            header.prefix = prefix;
            header.size = (headerLength + dataLength);

            byte[] byteHeader = new byte[headerLength];
            IntPtr ptr = Marshal.AllocHGlobal(headerLength);
            Marshal.StructureToPtr(header, ptr, true);
            Marshal.Copy(ptr, byteHeader, 0, headerLength);
            Marshal.FreeHGlobal(ptr);

            byte[] returnByte = new byte[byteHeader.Length];
            Buffer.BlockCopy(byteHeader, 0, returnByte, 0, byteHeader.Length);

            buffer = returnByte;
        }

        public byte[] ToBuffer()
        {
            return buffer;
        }
    }
}
