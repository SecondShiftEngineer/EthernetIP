using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using EIPNET.CIP;

namespace EIPNET.EIP
{

    public delegate void SocketErrorCallback(SocketException se);
    public delegate void PreOperationCallback();
    public delegate void PostOperationCallback();

    /// <summary>
    /// Contains Session Information
    /// </summary>
    public class SessionInfo
    {
        internal Socket SessionSocket { get; set; }
        public bool Connected { get; internal set; }
        public uint SessionHandle { get; internal set; }
        public string HostNameOrIp { get; internal set; }

        private byte[] _senderContext = new byte[8];
        public byte[] SenderContext
        {
            get { return _senderContext; }
            set
            {
                if (value == null)
                    _senderContext = new byte[8];
                if (value.Length > 8)
                    Buffer.BlockCopy(value, 0, _senderContext, 0, 8);
                if (value.Length <= 8)
                    Buffer.BlockCopy(value, 0, _senderContext, 0, value.Length);
            }
        }
        public int Port { get; internal set; }
        public string AccessPath { get; internal set; }
        public ConnectionParameters ConnectionParameters { get; internal set; }
        public IdentityItem IdentityInfo { get; internal set; }
        public bool Registered { get; internal set; }
        public int LastSessionError { get; internal set; }
        public string LastSessionErrorString { get; internal set; }
        internal string LastSessionErrorStack { get; set; }
        public int MillisecondTimeout { get; set; }
        public SocketErrorCallback OnSocketError { get; set; }
        public PreOperationCallback OnPreOperation { get; set; }
        public PostOperationCallback OnPostOperation { get; set; }

        byte[] _receiveBuffer = new byte[1024];

        public void SetConnectionParameters(ushort ConnectionSerialNumber,
            uint CIPTimeoutMs, uint T2O_ConnectionId, ushort VendorId,
            uint OriginatorSerialNumber)
        {
            ConnectionParameters cp = new ConnectionParameters();
            cp.ConnectionSerialNumber = ConnectionSerialNumber;
            byte[] tickInfo = CalculateTickTime(CIPTimeoutMs);
            if (tickInfo == null)
                throw new ApplicationException("Tick time could not be calculated");

            cp.PriorityAndTick = tickInfo[0];
            cp.ConnectionTimeoutTicks = tickInfo[1];
            cp.O2T_CID = 0;
            cp.T2O_CID = T2O_ConnectionId;
            cp.VendorID = VendorId;
            cp.OriginatorSerialNumber = OriginatorSerialNumber;

            ConnectionParameters = cp;
        }

        private byte[] CalculateTickTime(uint Milliseconds)
        {
            byte[] retVal = new byte[2];

            if (Milliseconds > 0x7F8000)
                Milliseconds = 0x7F8000;

            while (Milliseconds > 0xFF)
            {
                retVal[0]++;
                Milliseconds = (unchecked(Milliseconds >> 1));
            }

            retVal[1] = (byte)Milliseconds;

            return retVal;
        }

        internal int SendData(byte[] Data)
        {
            if (!Connected)
                return -1;

            try
            {
                return SessionSocket.Send(Data);
            }
            catch (SocketException se)
            {
                if (OnSocketError != null)
                    OnSocketError(se);
            }

            return -1;
        }

        internal byte[] ReceiveData()
        {
            try
            {

                //Orignally I thought about clearing the buffer here, but I changed
                //my mind since you should never get more bytes in the returned
                //buffer than what you receive.

                SessionSocket.ReceiveTimeout = MillisecondTimeout;
                int bytesRecvd = SessionSocket.Receive(_receiveBuffer);


                byte[] retVal = new byte[bytesRecvd];
                Buffer.BlockCopy(_receiveBuffer, 0, retVal, 0, bytesRecvd);

                return retVal;

            }
            catch (SocketException se)
            {
                //The connection has timed out probably
                if (OnSocketError != null)
                    OnSocketError(se);
            }

            return null;
        }

        private void ClearRecvBuffer()
        {
            _receiveBuffer.Initialize();
        }

        public byte[] SendData_WaitReply(byte[] Data)
        {
            if (SendData(Data) != -1)
            {
                return ReceiveData();
            }

            return null;
        }

        public EncapsReply SendRRData(CommonPacketItem AddressItem, CommonPacketItem DataItem, CommonPacketItem[] AdditionalItems = null)
		{
            if (OnPreOperation != null)
                OnPreOperation();

            if ((CommonPacketTypeId)AddressItem.TypeId == CommonPacketTypeId.ConnectionBased)
            {
                //The connection ID may have changed, so we have to reset it
                AddressItem.Data = BitConverter.GetBytes(ConnectionParameters.O2T_CID);
            }
            
            EncapsRRData rrData = new EncapsRRData();
            rrData.CPF = new CommonPacket();
            rrData.Timeout = (ushort)MillisecondTimeout;
            rrData.CPF.AddressItem = AddressItem;
            rrData.CPF.DataItem = DataItem;

            if (AdditionalItems != null)
            {
                for (int i = 0; i < AdditionalItems.Length; i++)
                    rrData.CPF.AddItem(AdditionalItems[i]);
            }

            EncapsPacket request = EncapsPacketFactory.CreateSendRRData(SessionHandle, 0, SenderContext, rrData.Pack());
            byte[] rawRequest = request.Pack();

            byte[] rawReply = SendData_WaitReply(rawRequest);

            if (OnPostOperation != null)
                OnPostOperation();

            if (rawReply == null)
                return null;

            EncapsReply reply = new EncapsReply();
            int temp = 0;
            reply.Expand(rawReply, 0, out temp);

            return reply;
        }
		
        public EncapsReply SendUnitData(CommonPacketItem AddressItem, CommonPacketItem DataItem, CommonPacketItem[] AdditionalItems = null)
		{
            if (OnPreOperation != null)
                OnPreOperation();

            if ((CommonPacketTypeId)AddressItem.TypeId == CommonPacketTypeId.ConnectionBased)
            {
                //The connection ID may have changed, so we have to reset it
                AddressItem.Data = BitConverter.GetBytes(ConnectionParameters.O2T_CID);
            }

            EncapsRRData rrData = new EncapsRRData();
            rrData.CPF = new CommonPacket();
            rrData.Timeout = (ushort)MillisecondTimeout;
            rrData.CPF.AddressItem = AddressItem;
            rrData.CPF.DataItem = DataItem;

            if (AdditionalItems != null)
            {
                for (int i = 0; i < AdditionalItems.Length; i++)
                    rrData.CPF.AddItem(AdditionalItems[i]);
            }

            EncapsPacket request = EncapsPacketFactory.CreateSendUnitData(SessionHandle, SenderContext, rrData.Pack());
            byte[] rawRequest = request.Pack();

            byte[] rawReply = SendData_WaitReply(rawRequest);

            if (OnPostOperation != null)
                OnPostOperation();

            if (rawReply == null)
                return null;

            EncapsReply reply = new EncapsReply();
            int temp = 0;
            reply.Expand(rawReply, 0, out temp);

            return reply;
        }


    }
}
