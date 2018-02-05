using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.CIP;

namespace EIPNET.EIP
{
    public static class EncapsPacketFactory
    {
        /// <summary>
        /// Creates an Encaps Packet from a raw byte stream
        /// </summary>
        /// <param name="RawPacket">Raw bytes</param>
        /// <returns>Encaps Packet</returns>
        public static EncapsPacket CreatePacket(byte[] RawPacket)
        {
            EncapsPacket packet = new EncapsPacket();
            int temp = 0;
            packet.Expand(RawPacket, 0, out temp);

            return packet;
        }
		

        /// <summary>
        /// Create a NOP (No Operation) packet
        /// </summary>
        /// <param name="EncapsData">Data to be sent with the NOP packet, which is dropped</param>
        /// <returns>NOP EncapsPacket</returns>
        /// <remarks>The NOP command will not issue a reply on the device, and all the data is dropped.</remarks>
        public static EncapsPacket CreateNOP(byte[] EncapsData = null)
        {
            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.NOP;
            packet.Length = (ushort)(EncapsData == null ? 0 : EncapsData.Length);
            packet.SessionHandle = 0;
            packet.Status = 0;
            packet.SenderContext = new byte[8];
            packet.OptionsFlags = 0;
            packet.EncapsData = EncapsData;

            return packet;
        }

        /// <summary>
        /// Creates a ListIdentity packet
        /// </summary>
        /// <returns>ListIdentity EncapsPacket</returns>
        public static EncapsPacket CreateListIdentity()
        {
            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.ListIdentity;
            packet.Length = 0;
            packet.SessionHandle = 0;
            packet.Status = 0;
            packet.SenderContext = new byte[8];
            packet.OptionsFlags = 0;

            return packet;
        }

        /// <summary>
        /// Creates a ListInterfaces packet
        /// </summary>
        /// <returns>ListInterfaces EncapsPacket</returns>
        public static EncapsPacket CreateListInterfaces()
        {
            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.ListInterfaces;
            packet.Length = 0;
            packet.SessionHandle = 0;
            packet.Status = 0;
            packet.SenderContext = new byte[8];
            packet.OptionsFlags = 0;

            return packet;
        }

        /// <summary>
        /// Creates a RegisterSession packet
        /// </summary>
        /// <param name="SenderContext">Sender Context, array of 8 bytes</param>
        /// <returns>RegisterSession EncapsPacket</returns>
        public static EncapsPacket CreateRegisterSession(byte[] SenderContext)
        {
            byte[] data = new byte[4];
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)1), 0, data, 0, 2);

            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.RegisterSession;
            packet.Length = 4;
            packet.SessionHandle = 0;
            packet.Status = 0;
            packet.SenderContext = new byte[8];
            packet.OptionsFlags = 0;
            packet.EncapsData = data;

            return packet;
        }
        
        /// <summary>
        /// Creates an UnRegisterSession packet
        /// </summary>
        /// <param name="SessionHandle">Session handle given during the RegisterSession process</param>
        /// <param name="SenderContext">Sender Context, array of 8 bytes</param>
        /// <returns>UnRegisterSession EncapsPacket</returns>
        public static EncapsPacket CreateUnRegisterSession(uint SessionHandle, byte[] SenderContext)
        {
            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.UnRegisterSession;
            packet.Length = 0;
            packet.SessionHandle = SessionHandle;
            packet.Status = 0;
            packet.SenderContext = SenderContext;
            packet.OptionsFlags = 0;

            return packet;
        }
		
        /// <summary>
        /// Creates a ListServices packet
        /// </summary>
        /// <param name="SenderContext">Sender Context, array of 8 bytes</param>
        /// <returns>ListServices EncapsPacket</returns>
        public static EncapsPacket CreateListServices(byte[] SenderContext = null)
        {
            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.ListServices;
            packet.Length = 0;
            packet.SessionHandle = 0;
            packet.Status = 0;
            packet.SenderContext = SenderContext;
            packet.OptionsFlags = 0;

            return packet;
        }
		
        /// <summary>
        /// Creates a SendRRData packet
        /// </summary>
        /// <param name="SessionHandle">Session handle given during the RegisterSession process</param>
        /// <param name="Timeout">Timeout, if 0 it uses the protocol timeout mechanism</param>
        /// <param name="SenderContext">Sender Context, array of 8 bytes</param>
        /// <param name="EncapsData">Data to be sent in the SendRRData Packet</param>
        /// <returns>EncapsPacket ready to be sent</returns>
        public static EncapsPacket CreateSendRRData(uint SessionHandle, uint Timeout, byte[] SenderContext = null, byte[] EncapsData = null)
		{
            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.SendRRData;
            packet.Length = (ushort)(6 + EncapsData.Length);
            packet.SessionHandle = SessionHandle;
            packet.Status = 0;
            packet.SenderContext = SenderContext;
            packet.OptionsFlags = 0;
            byte[] temp = new byte[(EncapsData == null ? 0 : EncapsData.Length)];

            if (EncapsData != null)
                Buffer.BlockCopy(EncapsData, 0, temp, 0, EncapsData.Length);

            packet.EncapsData = temp;

            return packet;
        }

        /// <summary>
        /// Creates a SendUnitData packet
        /// </summary>
        /// <param name="SessionHandle">Session handle given during the RegisterSession process</param>
        /// <param name="SenderContext">Sender Context, array of 8 bytes</param>
        /// <param name="EncapsData">Data to be sent in the SendUnitData Packet</param>
        /// <returns>EncapsPacket ready to be sent</returns>
        public static EncapsPacket CreateSendUnitData(uint SessionHandle, byte[] SenderContext = null, byte[] EncapsData = null)
		{
            EncapsPacket packet = new EncapsPacket();
            packet.Command = (ushort)EncapsCommand.SendUnitData;
            packet.Length = (ushort)(EncapsData.Length);
            packet.SessionHandle = SessionHandle;
            packet.Status = 0;
            packet.SenderContext = SenderContext;
            packet.OptionsFlags = 0;
            packet.EncapsData = EncapsData;

            return packet;
        }

    }
}
