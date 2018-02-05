using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public class InterfaceInformation : IExpandable
    {
        public ushort ItemTypeCode { get; set; }
        public ushort ItemLength { get; set; }
        public ushort EncapsVersion { get; set; }
        public ushort CapabilityFlags { get; set; }
        public string ServiceName { get; set; }


        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            NewOffset = Offset + 24;

            ItemTypeCode = BitConverter.ToUInt16(DataArray, Offset);
            ItemLength = BitConverter.ToUInt16(DataArray, Offset + 2);
            EncapsVersion = BitConverter.ToUInt16(DataArray, Offset + 4);
            CapabilityFlags = BitConverter.ToUInt16(DataArray, Offset + 6);
            ServiceName = System.Text.ASCIIEncoding.ASCII.GetString(DataArray, Offset + 8, 16);
        }
    }
}
