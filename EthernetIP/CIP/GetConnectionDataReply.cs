using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Get Connection Data - Reply
    /// </summary>
    public class GetConnectionDataReply : IExpandable
    {
        /// <summary>
        /// Connection Number
        /// </summary>
        public ushort ConnectionNumber
        {
            get;
            internal set;
        }

        /// <summary>
        /// Connection State
        /// </summary>
        public ushort ConnectionState
        {
            get;
            internal set;
        }

        /// <summary>
        /// Originator Port
        /// </summary>
        public ushort OriginatorPort
        {
            get;
            internal set;
        }

        /// <summary>
        /// Target Port
        /// </summary>
        public ushort TargetPort
        {
            get;
            internal set;
        }

        /// <summary>
        /// Connection serial number of established connection
        /// </summary>
        public ushort ConnectionSerialNumber
        {
            get;
            internal set;
        }

        /// <summary>
        /// Vendor ID of the originating node.
        /// </summary>
        public ushort OriginatorVendorID
        {
            get;
            internal set;
        }

        /// <summary>
        /// Serial number of the originating node.
        /// </summary>
        public uint OriginatorSerialNumber
        {
            get;
            internal set;
        }

        /// <summary>
        /// Originator O->T Connection Id
        /// </summary>
        public uint Orignator_O2T_ConnectionId
        {
            get;
            internal set;
        }

        /// <summary>
        /// Target O->T Connection Id
        /// </summary>
        public uint Target_O2T_ConnectionId
        {
            get;
            internal set;
        }

        /// <summary>
        /// Connection Timeout Multiplier
        /// </summary>
        public byte ConnectionTimeoutMultiplier
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reserved - Shall be Zero
        /// </summary>
        public byte Reserved
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reserved - Shall be Zero
        /// </summary>
        public byte Reserved2
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reserved - Shall be Zero
        /// </summary>
        public byte Reserved3
        {
            get;
            internal set;
        }

        /// <summary>
        /// Originator Requested Packet Interval O->T
        /// </summary>
        public uint Originator_RPI_O2T
        {
            get;
            internal set;
        }

        /// <summary>
        /// Originator Actual Packet Interval O->T
        /// </summary>
        public uint Originator_API_O2T
        {
            get;
            internal set;
        }

        /// <summary>
        /// Originator T->O Connection Id
        /// </summary>
        public uint Originator_T2O_ConnectionId
        {
            get;
            internal set;
        }

        /// <summary>
        /// Target T->O Connection Id
        /// </summary>
        public uint Target_T2O_ConnectionId
        {
            get;
            internal set;
        }

        /// <summary>
        /// Connection Timeout Multiplier 2
        /// </summary>
        public byte ConnectionTimeoutMultiplier2
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reserved - Shall be Zero
        /// </summary>
        public byte Reserved4
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reserved - Shall be Zero
        /// </summary>
        public byte Reserved5
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reserved - Shall be Zero
        /// </summary>
        public byte Reserved6
        {
            get;
            internal set;
        }

        /// <summary>
        /// Originator Requested Packet Interval T->O
        /// </summary>
        public uint Originator_RPI_T2O
        {
            get;
            internal set;
        }

        /// <summary>
        /// Originator Actual Packet Interval T->O
        /// </summary>
        public uint Originator_API_T2O
        {
            get;
            internal set;
        }
    
        /// <summary>
        /// Expands a byte array into the GetConnectionDataReply
        /// </summary>
        /// <param name="DataArray">Source Array</param>
        /// <param name="Offset">Offset where the data begins</param>
        /// <param name="NewOffset">[Out] Offset where the data ends</param>
        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            ConnectionNumber = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;
            ConnectionState = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;
            OriginatorPort = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;
            TargetPort = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;
            ConnectionSerialNumber = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;
            OriginatorVendorID = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;
            OriginatorSerialNumber = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            Orignator_O2T_ConnectionId = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            Target_O2T_ConnectionId = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            ConnectionTimeoutMultiplier = DataArray[Offset];
            Offset += 1;
            Offset += 3;        //Skip reserved
            Originator_RPI_O2T = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            Originator_API_O2T = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            Originator_T2O_ConnectionId = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            Target_T2O_ConnectionId = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            ConnectionTimeoutMultiplier2 = DataArray[Offset];
            Offset += 1;
            Offset += 3;        //Skip reserved
            Originator_RPI_T2O = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            Originator_API_T2O = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 3;        //This is because it points to the END of the data, not one past it

            NewOffset = Offset;
        }
    }
}
