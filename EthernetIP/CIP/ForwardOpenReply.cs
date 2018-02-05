using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.EIP;

namespace EIPNET.CIP
{
    public abstract class ForwardOpenReply
    {
        /// <summary>
        /// Returns the same value recieved in the ForwardOpenRequest packet
        /// </summary>
        public ushort ConnectionSerialNumber { get; internal set; }
        /// <summary>
        /// Returns the same value recieved in the ForwardOpenRequest packet
        /// </summary>
        public ushort OriginatorVendorId { get; internal set; }
        /// <summary>
        /// Returns the same value recieved in the ForwardOpenRequest packet
        /// </summary>
        public uint OriginatorSerialNumber { get; internal set; }

        public static implicit operator ForwardOpenReply(EncapsReply lhs)
        {
            //First, there should be an EncapsRRData in the reply...
            EncapsRRData replyData = new EncapsRRData();
            int temp = 0;
            replyData.Expand(lhs.EncapsData, 0, out temp);

            //Now we have to dig out the MR packet from the Unconnected Data Item...
            MR_Response response = new MR_Response();
            response.Expand(replyData.CPF.DataItem.Data, 0, out temp);

            if (response.GeneralStatus == 0)
            {
                ForwardOpenReply_Success fSucc = new ForwardOpenReply_Success();
                fSucc.Expand(response);
                return fSucc;
            }
            else
            {
                ForwardOpenReply_Fail fFail = new ForwardOpenReply_Fail();
                fFail.Expand(response);
                return fFail;
            }
        }
    }

    public class ForwardOpenReply_Success : ForwardOpenReply
    {
        /// <summary>
        /// Network Connection ID to be used for the local link, originator to target.
        /// </summary>
        /// <remarks>
        /// If chosen by the originator, then the value is echoed back. This is the
        /// target's CIP Consumed Connection ID
        /// </remarks>
        public uint O2T_ConnectionId { get; internal set; }
        /// <summary>
        /// Network Connection ID to be used for the local link, target to originator.
        /// </summary>
        /// <remarks>
        /// If chosen by the originator, then the value is echoed back. This is the
        /// target's CIP Consumed Connection ID
        /// </remarks>
        public uint T2O_ConnectionId { get; internal set; }
        /// <summary>
        /// Acutal packet rate, originator to target.
        /// </summary>
        /// <remarks>
        /// A router shall use the lesser of this value and the T2O_API for
        /// the expected packet rate of the connection.
        /// </remarks>
        public uint O2T_API { get; internal set; }
        /// <summary>
        /// Acutal packet rate, target to originator.
        /// </summary>
        /// <remarks>
        /// A router shall use the lesser of this value and the O2T_API for
        /// the expected packet rate of the connection.
        /// </remarks>
        public uint T2O_API { get; internal set; }
        /// <summary>
        /// Number of 16 bit words in the ApplicationReply field
        /// </summary>
        public byte ApplicationReplySize { get; internal set; }
        /// <summary>
        /// Reserved
        /// </summary>
        public byte Reserved { get { return 0; } }
        /// <summary>
        /// Application specific data
        /// </summary>
        public byte[] ApplicationReply { get; internal set; }

        internal void Expand(MR_Response response)
        {
            O2T_ConnectionId = BitConverter.ToUInt32(response.ResponseData, 0);
            T2O_ConnectionId = BitConverter.ToUInt32(response.ResponseData, 4);
            ConnectionSerialNumber = BitConverter.ToUInt16(response.ResponseData, 8);
            OriginatorVendorId = BitConverter.ToUInt16(response.ResponseData, 10);
            OriginatorSerialNumber = BitConverter.ToUInt32(response.ResponseData, 12);
            O2T_API = BitConverter.ToUInt32(response.ResponseData, 16);
            T2O_API = BitConverter.ToUInt32(response.ResponseData, 20);
            ApplicationReplySize = response.ResponseData[24];

            if (ApplicationReplySize > 0)
            {
                byte[] temp = new byte[ApplicationReplySize * 2];
                Buffer.BlockCopy(response.ResponseData, 26, temp, 0, temp.Length);
            }
        }
    }

    public class ForwardOpenReply_Fail : ForwardOpenReply
    {
        public byte RemainingPathSize { get; internal set; }
        public byte Reserved { get; internal set; }

        internal void Expand(MR_Response response)
        {
            ConnectionSerialNumber = BitConverter.ToUInt16(response.ResponseData, 0);
            OriginatorVendorId = BitConverter.ToUInt16(response.ResponseData, 2);
            OriginatorSerialNumber = BitConverter.ToUInt32(response.ResponseData, 4);
            RemainingPathSize = response.ResponseData[8];
        }
    }
}
