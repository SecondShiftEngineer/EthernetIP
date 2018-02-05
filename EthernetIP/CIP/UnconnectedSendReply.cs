using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.EIP;

namespace EIPNET.CIP
{
    /// <summary>
    /// Abstract class for the UnconnectedSendReply.
    /// </summary>
    /// <remarks>
    /// Check to see if this class is an UnconnectedSendReply_Success or
    /// UnconnectedSendReply_Failure to determine the validity of the response.
    /// </remarks>
    public abstract class UnconnectedSendReply
    {
        /// <summary>
        /// Reply Service Code
        /// </summary>
        public byte ReplyService { get; internal set; }
        /// <summary>
        /// Reserved
        /// </summary>
        public byte Reserved { get; internal set; }
        /// <summary>
        /// General Status
        /// </summary>
        public byte GeneralStatus { get; internal set; }

        /// <summary>
        /// Converts an EncapsReply to either an UnconnectedSendReply_Success or UnconnectedSendReply_Failure
        /// </summary>
        /// <param name="rhs">EncapsReply to convert</param>
        /// <returns>Either UnconnectedSendReply_Success or UnconnectedSendReply_Failure</returns>
        public static implicit operator UnconnectedSendReply(EncapsReply rhs)
        {
            EncapsRRData replyData = new EncapsRRData();
            int temp = 0;
            replyData.Expand(rhs.EncapsData, 0, out temp);

            //We should have an unconnected data item...
            if (replyData.CPF.DataItem.TypeId != (byte)CommonPacketTypeId.UnconnectedMessage)
                return new UnconnectedSendReply_Failure() { ErrorString = "Incorrect Packet Type", GeneralStatus = 0xFF };

            byte[] rawData = replyData.CPF.DataItem.Data;

            if (rawData.Length < 3)
                return null;

            byte replyService = rawData[0];
            byte reserved = rawData[1];
            byte generalStatus = rawData[2];

            if (generalStatus == 0 || generalStatus == 0x06)        //0x06 == partial transfer
            {
                UnconnectedSendReply_Success ucSucc = new UnconnectedSendReply_Success();
                ucSucc.ReplyService = replyService;
                ucSucc.Reserved = 0;
                ucSucc.GeneralStatus = generalStatus;
                ucSucc.Unpack(rawData);
                return ucSucc;
            }
            else
            {
                UnconnectedSendReply_Failure ucFail = new UnconnectedSendReply_Failure();
                ucFail.ReplyService = replyService;
                ucFail.Reserved = 0;
                ucFail.GeneralStatus = generalStatus;
                ucFail.Unpack(rawData);
                return ucFail;
            }
        }

    }

    /// <summary>
    /// Successful UnconnectedSendReply
    /// </summary>
    public class UnconnectedSendReply_Success : UnconnectedSendReply
    {
        /// <summary>
        /// Reserved
        /// </summary>
        public byte AdditionalStatusSize { get; internal set; }
        /// <summary>
        /// Service Response Data
        /// </summary>
        public byte[] ServiceResponse { get; internal set; }

        internal void Unpack(byte[] sourceArray)
        {
            AdditionalStatusSize = sourceArray[3];
            int bytesLeft = sourceArray.Length - 4;
            if (bytesLeft > 0)
            {
                byte[] temp = new byte[bytesLeft];
                Buffer.BlockCopy(sourceArray, 4, temp, 0, temp.Length);
                ServiceResponse = temp;
            }
        }
    }

    /// <summary>
    /// Failed UnconnectedSendReply
    /// </summary>
    public class UnconnectedSendReply_Failure : UnconnectedSendReply
    {
        /// <summary>
        /// Number of 16 bit words in the AdditionalStatus array
        /// </summary>
        public byte AdditionalStatusSize { get; internal set; }
        /// <summary>
        /// Additional Status information
        /// </summary>
        public byte[] AdditionalStatus { get; internal set; }
        /// <summary>
        /// Remaining path size, only present for routing errors.
        /// </summary>
        public byte RemainingPathSize { get; internal set; }
        /// <summary>
        /// String representation of the routing error.
        /// </summary>
        public string ErrorString { get; internal set; }

        internal void Unpack(byte[] sourceArray)
        {
            AdditionalStatusSize = sourceArray[3];
            byte[] temp = new byte[AdditionalStatusSize * 2];
            Buffer.BlockCopy(sourceArray, 4, temp, 0, temp.Length);
            AdditionalStatus = temp;
            
            if (sourceArray.Length > 4 + temp.Length)
                RemainingPathSize = sourceArray[sourceArray.Length - 1];

            SetErrorString();
        }

        internal void SetErrorString()
        {
            ushort addStatus = GetAdditionalStatus();

            switch (GeneralStatus)
            {
                case 0x01:
                    switch (addStatus)
                    {
                            //TODO: Localize the following strings
                        case 0x0204:
                            ErrorString = "Timeout";
                            break;
                        case 0x0311:
                            ErrorString = "Invalid Port ID specified in the Route_Path field";
                            break;
                        case 0x0312:
                            ErrorString = "Invalid Node Address specified in the Route_Path field";
                            break;
                        case 0x0315:
                            ErrorString = "Invalid segment type in the Route_Path field";
                            break;
                        default:
                            ErrorString = "Unknown routing error";
                            break;
                    }
                    break;
                case 0x02:
                    ErrorString = CIP.GeneralStatus.GetGeneralStatusDescription(0x02);
                    break;
                case 0x04:
                    ErrorString = CIP.GeneralStatus.GetGeneralStatusDescription(0x04);
                    break;
                default:
                    ErrorString = CIP.GeneralStatus.GetGeneralStatusDescription(GeneralStatus);
                    break;
            }
        }

        internal ushort GetAdditionalStatus()
        {
            if (AdditionalStatus.Length < 2)
                return 0xFFFF;

            return BitConverter.ToUInt16(AdditionalStatus, 0);
        }
    }
}
