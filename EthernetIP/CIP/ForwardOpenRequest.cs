using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Forward Open Request (Service Code 0x54)
    /// </summary>
    public class ForwardOpenRequest : IPackable
    {
        /// <summary>
        /// Used to calculate request timeout information
        /// </summary>
        public byte Priority_TimeTick { get; set; }
        /// <summary>
        /// Used to calculate request timeout information
        /// </summary>
        public byte Timeout_Ticks { get; set; }
        /// <summary>
        /// Network connection ID to be used for the local link, originator to target.
        /// </summary>
        public uint O2T_ConnectionId { get; set; }
        /// <summary>
        /// Network connection ID to be used for the local link, target to originator.
        /// </summary>
        public uint T2O_ConnectionId { get; set; }
        /// <summary>
        /// Unique 16-bit value selected by the connection manager at the originator
        /// of the connection.
        /// </summary>
        /// <remarks>
        /// The originator shall make sure that the 16-bit value is unique for the device. There
        /// shall be no other significance placed on the number by any other node in the connection
        /// path. The connection serial numbers shall be unique but do not have to be sequential.
        /// For example, an operator interface may have a large number of connections open at the
        /// same time, each with a unique number. The same values could be repeated at other
        /// operator interface stations. A possible implementation woudl be to have a connection
        /// list which points to the descriptor for each connection, and the connection serial
        /// number could be the index into the table.
        /// </remarks>
        public ushort ConnectionSerialNumber { get; set; }
        /// <summary>
        /// The vendor number shall be a unique number assigned to the various vendors of products.
        /// This is obtained from the ODVA.org website.
        /// </summary>
        public ushort OriginatorVendorId { get; set; }
        /// <summary>
        /// A 32 bit value that is assigned to the device at the time of manufacture.
        /// </summary>
        public uint OriginatorSerialNumber { get; set; }
        /// <summary>
        /// Used to calculate request timeout information.
        /// </summary>
        public byte ConnectionTimeoutMultiplier { get; set; }
        /// <summary>
        /// 3 octet of zero's
        /// </summary>
        public byte[] Reserved { get { return new byte[3]; } }
        /// <summary>
        /// Originator to target requested packet rate in micoroseconds
        /// </summary>
        public uint O2T_RPI { get; set; }
        /// <summary>
        /// Originator to target connection parameters
        /// </summary>
        public short O2T_ConnectionParameters { get; set; }
        /// <summary>
        /// Target to originator requested packet rate in microseconds
        /// </summary>
        public uint T2O_RPI { get; set; }
        /// <summary>
        /// Originator to target connection parameters
        /// </summary>
        public short T2O_ConnectionParameters { get; set; }
        /// <summary>
        /// Transport Type/Trigger
        /// </summary>
        public byte TransportTrigger { get; set; }
        /// <summary>
        /// Number of 16 bit words in the Connection_Path
        /// </summary>
        /// <remarks>
        /// The Connection_Path_Size is automatically verified and set
        /// during the Pack() routine.
        /// </remarks>
        public byte Connection_Path_Size { get; set; }
        /// <summary>
        /// Indicates the route to the Remote Target Device
        /// </summary>
        /// <remarks>
        /// The Connection_Path is automatically padded to the 16 bit
        /// boundary during the Pack() routine.
        /// </remarks>
        public byte[] Connection_Path { get; set; }

        /// <summary>
        /// Creates a new ForwardOpenRequest
        /// </summary>
        public ForwardOpenRequest() { }

        /// <summary>
        /// Creates a new ForwardOpenRequest using the ConnectionParameters
        /// </summary>
        /// <param name="cParams">Connection Parameters</param>
        public ForwardOpenRequest(ConnectionParameters cParams)
        {
            Priority_TimeTick = cParams.PriorityAndTick;
            Timeout_Ticks = cParams.ConnectionTimeoutTicks;
            O2T_ConnectionId = cParams.O2T_CID;
            T2O_ConnectionId = cParams.T2O_CID;
            ConnectionSerialNumber = cParams.ConnectionSerialNumber;
            OriginatorVendorId = cParams.VendorID;
            OriginatorSerialNumber = cParams.OriginatorSerialNumber;
        }

        /// <summary>
        /// Packs the request into a byte array
        /// </summary>
        /// <returns>Byte array representing the request</returns>
        public byte[] Pack()
        {
            if (Connection_Path == null)
                throw new NullReferenceException("Connection_Path cannot be null");

            int cPathSize = Connection_Path.Length / 2 + (Connection_Path.Length % 2);
            Connection_Path_Size = (byte)cPathSize;

            List<byte> retval = new List<byte>();

            retval.Add(Priority_TimeTick);
            retval.Add(Timeout_Ticks);
            retval.AddRange(BitConverter.GetBytes(O2T_ConnectionId));
            retval.AddRange(BitConverter.GetBytes(T2O_ConnectionId));
            retval.AddRange(BitConverter.GetBytes(ConnectionSerialNumber));
            retval.AddRange(BitConverter.GetBytes(OriginatorVendorId));
            retval.AddRange(BitConverter.GetBytes(OriginatorSerialNumber));
            retval.Add(ConnectionTimeoutMultiplier);
            retval.AddRange(new byte[] { 0, 0, 0 });
            retval.AddRange(BitConverter.GetBytes(O2T_RPI));
            retval.AddRange(BitConverter.GetBytes(O2T_ConnectionParameters));
            retval.AddRange(BitConverter.GetBytes(T2O_RPI));
            retval.AddRange(BitConverter.GetBytes(T2O_ConnectionParameters));
            retval.Add(TransportTrigger);
            retval.Add(Connection_Path_Size);
            retval.AddRange(Connection_Path);

            return retval.ToArray();


        }
    }
}
