using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public class CIPKey : IPackable, IExpandable
    {
        //Fields
        private ushort _vendorId;
        private ushort _deviceType;
        private ushort _productCode;
        private byte _majorRev;
        private byte _minorRev;
        private short _compatibility;

        //Properties
        public ushort VendorId { get { return _vendorId; } set { _vendorId = value; } }
        public ushort DeviceType { get { return _deviceType; } set { _deviceType = value; } }
        public ushort ProductCode { get { return _productCode; } set { _productCode = value; } }
        public byte MajorRev { get { return _majorRev; } set { _majorRev = value; } }
        public byte MinorRev { get { return _minorRev; } set { _minorRev = value; } }
        public short Compatibility { get { return _compatibility; } set { _compatibility = value; } }

        //Methods
        public byte[] Pack()
        {
            byte[] retVal = new byte[10];

            int offset = 0;
            Buffer.BlockCopy(BitConverter.GetBytes(_vendorId), 0, retVal, offset, 2);
            offset += 2;

            Buffer.BlockCopy(BitConverter.GetBytes(_deviceType), 0, retVal, offset, 2);
            offset += 2;

            Buffer.BlockCopy(BitConverter.GetBytes(_productCode), 0, retVal, offset, 2);
            offset += 2;

            retVal[offset] = _majorRev;
            offset += 1;

            retVal[offset] = _minorRev;
            offset += 1;

            Buffer.BlockCopy(BitConverter.GetBytes(_compatibility), 0, retVal, offset, 2);

            return retVal;
        }

        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            int offset = Offset;

            _vendorId = BitConverter.ToUInt16(DataArray, offset);
            offset += 2;

            _deviceType = BitConverter.ToUInt16(DataArray, offset);
            offset += 2;

            _productCode = BitConverter.ToUInt16(DataArray, offset);
            offset += 2;

            _majorRev = DataArray[offset];
            offset += 1;

            _minorRev = DataArray[offset];
            offset += 1;

            _compatibility = BitConverter.ToInt16(DataArray, offset);
            offset += 2;

            NewOffset = offset - 1;
        }
    }
}
