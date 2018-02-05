using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public class PortSegmentR : IPackable, IExpandable
    {
        //Fields
        private ushort _linkAddressSize;
        private ushort _portId;
        private byte[] _linkAddress;

        //Properties
        public ushort LinkAddressSize { get { return _linkAddressSize; } set { _linkAddressSize = value; } }
        public ushort PortId { get { return _portId; } set { _portId = value; } }
        public byte[] LinkAddress { get { return _linkAddress; } set { _linkAddress = value; } }
        
        public byte[] Pack()
        {
            byte[] retVal = new byte[(2 * 2) + _linkAddress.Length];

            int offset = 0;
            Buffer.BlockCopy(BitConverter.GetBytes(_linkAddressSize), 0, retVal, offset, 2);
            offset += 2;

            Buffer.BlockCopy(BitConverter.GetBytes(_portId), 0, retVal, offset, 2);
            offset += 2;

            Buffer.BlockCopy(_linkAddress, 0, retVal, offset, _linkAddress.Length);

            return retVal;
        }

        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            int offset2 = Offset;

            _linkAddressSize = BitConverter.ToUInt16(DataArray, offset2);
            offset2 += 2;

            _portId = BitConverter.ToUInt16(DataArray, offset2);
            offset2 += 2;

            //The rest of the bytes get transferred into the LinkAddress field...
            _linkAddress = new byte[_linkAddressSize];
            Buffer.BlockCopy(DataArray, offset2, _linkAddress, 0, _linkAddress.Length);
            offset2 += _linkAddress.Length;

            NewOffset = offset2 - 1;
        }
    }
}
