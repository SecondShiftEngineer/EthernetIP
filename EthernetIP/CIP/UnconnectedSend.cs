using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Unconnected Send Service Parameters
    /// </summary>
    public class UnconnectedSend : IPackable
    {
        public byte Service { get { return 0x52; } }

        public byte RequestPathSize { get; set; }

        public byte[] RequestPath { get; set; }

        /// <summary>
        /// Used to calculate request timeout information.
        /// </summary>
        public byte Priority_TimeTick
        {
            get;
            set;
        }

        /// <summary>
        /// Used to calculate request timeout information.
        /// </summary>
        public byte Timeout_Ticks
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies the number of bytes in the Message Request
        /// </summary>
        /// <remarks>
        /// This is automatically set when the UnconnectedSend request
        /// is packed. The Pad byte is also added if necessary.
        /// </remarks>
        public ushort MessageRequestSize
        {
            get;
            set;
        }

        /// <summary>
        /// Message Request
        /// </summary>
        public MR_Request MessageRequest
        {
            get;
            set;
        }

        /// <summary>
        /// Only present if Message Request Size is odd
        /// </summary>
        public byte Pad
        {
            get;
            set;
        }

        /// <summary>
        /// Number of 16 bit words in the route path.
        /// </summary>
        /// <remarks>
        /// This is automatically set based on the RoutePath when the
        /// UnconnectedSend request is packed.
        /// </remarks>
        public byte RoutePathSize
        {
            get;
            set;
        }

        /// <summary>
        /// Shall be zero
        /// </summary>
        public byte Reserved
        {
            get { return 0; }
        }

        /// <summary>
        /// Indicates the route to the remote device, padded to a 16 bit boundary.
        /// </summary>
        /// <remarks>
        /// This is automatically padded when the UnconnectedSend request is packed.
        /// </remarks>
        public byte[] RoutePath
        {
            get;
            set;
        }
    
        /// <summary>
        /// Packs the UnconnectedSend request into a byte array
        /// </summary>
        /// <returns>Array of bytes representing the UnconnectedSend request</returns>
        public byte[] Pack()
        {
            if (MessageRequest == null)
                throw new NullReferenceException("MessageRequest cannot be null");

            if (RoutePath == null)
                throw new NullReferenceException("RoutePath cannot be null");

            if (RequestPath == null)
                throw new NullReferenceException("RequestPath cannot be null");

            byte[] mrRequestBytes = MessageRequest.Pack();
            MessageRequestSize = (ushort)mrRequestBytes.Length;

            bool needsPad = (MessageRequestSize % 2 != 0);
            int padSize = (needsPad ? 1 : 0);

            RoutePathSize = (byte)((RoutePath.Length / 2) + (RoutePath.Length % 2));
            RequestPathSize = (byte)((RequestPath.Length / 2) + (RequestPath.Length % 2));

            byte[] retVal = new byte[4 + mrRequestBytes.Length + padSize + 2 + (2 * RoutePathSize) + 2 + (RequestPathSize * 2)];

            int offset = 0;

            retVal[offset] = Service;
            offset++;
            retVal[offset] = RequestPathSize;
            offset++;
            Buffer.BlockCopy(RequestPath, 0, retVal, offset, RequestPath.Length);
            offset += RequestPathSize * 2;

            retVal[offset] = Priority_TimeTick;
            offset++;
            retVal[offset] = Timeout_Ticks;
            offset++;

            Buffer.BlockCopy(BitConverter.GetBytes(MessageRequestSize), 0, retVal, offset, 2);
            offset += 2;
            Buffer.BlockCopy(mrRequestBytes, 0, retVal, offset, mrRequestBytes.Length);
            offset += mrRequestBytes.Length;
            if (needsPad)
                offset += 1;
            retVal[offset] = RoutePathSize;
            offset += 1;
            offset += 1;        //reserved
            Buffer.BlockCopy(RoutePath, 0, retVal, offset, RoutePath.Length);

            return retVal;
        }
    }
}
