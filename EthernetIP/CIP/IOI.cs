using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public static class IOI
    {
        public static int BuildIOIString(byte[] IOI, string address, int size, int start = 0)
        {
            int addresslen = (address.Length < size ? address.Length : size);
            string tmpAddress = address.Substring(0, addresslen);

            if (addresslen <= 0)
                return 0;

            int pathLen = 2 + addresslen + (addresslen % 2);
            if (IOI == null)
                return pathLen;

            byte[] ioiAdd = new byte[pathLen];
            ioiAdd[0] = 0x91;
            ioiAdd[1] = (byte)addresslen;

            byte[] byteString = System.Text.ASCIIEncoding.ASCII.GetBytes(tmpAddress);
            Buffer.BlockCopy(byteString, 0, ioiAdd, 2, addresslen);
            Buffer.BlockCopy(ioiAdd, 0, IOI, start, ioiAdd.Length);

            return pathLen;
        }
		

        public static int BuildIOIArray(byte[] IOI, string address, int size, int start = 0)
        {
            int ioiLength = start;
            int p = 0;
            int l = (address.Length < size ? address.Length : size);
            int b = 0;
            int value = 0;

            while (p < l)
            {
                b = address.IndexOf(',', p);
                if (b == -1)
                    value = 0;
                else
                {
                    string subStr = address.Substring(b);
                    value = atoi(subStr);
                }

                if (IOI != null)
                {
                    ioiLength += AddSegmentValue(ref IOI, value, ioiLength);
                }
                else
                {
                    ioiLength += AddSegmentValue(ref IOI, value, ioiLength);
                }
                if (b == -1)
                    break;

                p = ++b;
            }

            return ioiLength;
        }

        public static int BuildIOI(byte[] IOI, string address)
        {
            int b = 0;
            int e = 0;
            int p = 0;
            int l = address.Length;
            int ioiLength = 0;

            while (p < l)
            {
                b = address.IndexOf('[', p);
                e = address.IndexOf('.', p);

                if ((b == -1) && (e == -1))
                {
                    ioiLength += BuildIOIString(IOI, address.Substring(p), l - p, ioiLength);
                    p = l;
                }
                if ((b != -1) && (((e != -1) && (b < e)) || (e == -1)))     //"[" before "."
                {
                    e = address.IndexOf("]", p);
                    if (b > p)
                    {
                        ioiLength += BuildIOIString(IOI, address.Substring(p), b - p, ioiLength);
                    }

                    ioiLength += BuildIOIArray(IOI, address.Substring(b), e - b, ioiLength);

                    p = e + 1;
                }
                if ((e != -1) && (((b != -1) && (b > e)) || (b == -1)))     //"." before "["
                {
                    ioiLength += BuildIOIString(IOI, address.Substring(p), e - p, ioiLength);
                    p = e + 1;
                }
            }

            return ioiLength;
        }

        private static int AddSegmentValue(ref byte[] IOI, int value, int startPos)
        {
            int valueLength = 0;

            if (value > 0xFFFF)
                valueLength = 4;
            else
            {
                if (value > 0xFF)
                    valueLength = 2;
                else
                    valueLength = 1;
            }

            switch (valueLength)
            {
                case 1:
                    if (IOI != null)
                    {
                        IOI[startPos] = 0x28;
                        IOI[startPos + 1] = (byte)value;
                    }
                    return 2;
                case 2:
                    if (IOI != null)
                    {
                        IOI[startPos] = 0x29;
                        IOI[startPos + 1] = 0x00;
                        Buffer.BlockCopy(BitConverter.GetBytes((ushort)value), 0, IOI, 2, 2);
                    }
                    return 4;
                case 4:
                    if (IOI != null)
                    {
                        IOI[startPos] = 0x2A;
                        IOI[startPos + 1] = 0x00;
                        Buffer.BlockCopy(BitConverter.GetBytes((ushort)value), 0, IOI, 2, 2);
                    }
                    return 6;
                default:
                    return 0;
            }
        }

        private static int atoi(string str)
        {
            string val = str.Trim();

            string iVal = "";

            for (int i = 0; i < val.Length; i++)
            {
                char c = val[i];

                if (char.IsDigit(c) || c == '+' || c == '-')
                    iVal += c;
                else
                    break;
            }

            return int.Parse(iVal);
        }

        private enum IOISegmentType : byte
        {
            Class = 0x20,
            Instance = 0x24,
            Element = 0x28,
            Symbolic = 0x91
        }

        private struct IOISegment
        {
            public IOISegmentType SegmentType { get; set; }
            public string SegmentString { get; set; }
        }

        public static byte[] BuildIOI(string ioiAddress)
        {
            //First get a list of segments...
            List<IOISegment> segments = SegmentConverter(ioiAddress);

            if (segments == null)
                return null;

            List<byte> retVal = new List<byte>();
            byte[] temp;

            for (int i = 0; i < segments.Count; i++)
            {
                switch (segments[i].SegmentType)
                {
                    case IOISegmentType.Symbolic:
                        temp = BuildSymbolicSegment(segments[i].SegmentString);
                        if (temp == null)
                            return null;
                        retVal.AddRange(temp);
                        break;
                    case IOISegmentType.Element:
                        temp = BuildElementSegment(segments[i].SegmentString);
                        if (temp == null)
                            return null;
                        retVal.AddRange(temp);
                        break;
                    default:
                        return null;        //Unknown segment
                }
            }

            return retVal.ToArray();
        }

        private static List<IOISegment> SegmentConverter(string ioiAddress)
        {
            List<IOISegment> retVal = new List<IOISegment>();
            string currentValue = "";
            char lastAddedChar = '\0';
            IOISegmentType currentSegmentType = IOISegmentType.Symbolic;

            for (int i = 0; i < ioiAddress.Length; i++)
            {
                if (currentSegmentType == IOISegmentType.Symbolic)
                {
                    //Hitting a '.' means starting a new segment
                    if (ioiAddress[i] == '.')
                    {
                        //If currentValue is an empty string, this is an error
                        if (string.IsNullOrEmpty(currentValue) && lastAddedChar != '\0')
                            return null;
                        retVal.Add(new IOISegment() { SegmentType = IOISegmentType.Symbolic, SegmentString = currentValue });
                        currentValue = "";/// +ioiAddress[i];
                        lastAddedChar = '\0';
                    }
                    else if (ioiAddress[i] == '[')
                    {
                        if (string.IsNullOrEmpty(currentValue))
                            return null;
                        retVal.Add(new IOISegment() { SegmentType = IOISegmentType.Symbolic, SegmentString = currentValue });
                        currentValue = "";
                        currentSegmentType = IOISegmentType.Element;
                        lastAddedChar = '\0';
                    }
                    else
                    {
                        //Add it to the current value
                        currentValue += ioiAddress[i];
                        lastAddedChar = ioiAddress[i];
                    }
                }
                else if (currentSegmentType == IOISegmentType.Element)
                {
                    //Keep adding values until we get to a ]
                    if (ioiAddress[i] == ']')
                    {
                        retVal.Add(new IOISegment() { SegmentType = IOISegmentType.Element, SegmentString = currentValue });
                        currentSegmentType = IOISegmentType.Symbolic;
                        lastAddedChar = ioiAddress[i];
                        currentValue = "";
                    }
                    else if (char.IsDigit(ioiAddress[i]) || ioiAddress[i] == ',')
                    {
                        currentValue += ioiAddress[i];
                        lastAddedChar = ioiAddress[i];
                    }
                    else
                    {
                        //Error
                        return null;
                    }
                }
                else
                    return null;            //Shouldn't ever happen
            }

            if (!string.IsNullOrEmpty(currentValue))
                retVal.Add(new IOISegment() { SegmentType = currentSegmentType, SegmentString = currentValue });

            return retVal;
        }

        private static byte[] BuildElementSegment(string elementSegment)
        {
            //Ok, basically we have a list of numbers separated by comma's...
            string[] elementNumbers = elementSegment.Split(',');
            List<byte> retVal = new List<byte>();
            byte[] temp;

            for (int i = 0; i < elementNumbers.Length; i++)
            {
                int myElement = 0;
                try
                {
                    myElement = Convert.ToInt32(elementNumbers[i]);
                }
                catch { return null; }

                if (myElement > 0xFFFF)
                {
                    temp = new byte[6];
                    temp[0] = 0x2A;
                    temp[1] = 0x00;
                    Buffer.BlockCopy(BitConverter.GetBytes(myElement), 0, temp, 2, 4);
                    retVal.AddRange(temp);
                }
                else if (myElement > 0xFF)
                {
                    temp = new byte[4];
                    temp[0] = 0x29;
                    temp[1] = 0x00;
                    Buffer.BlockCopy(BitConverter.GetBytes(myElement), 0, temp, 2, 2);
                    retVal.AddRange(temp);
                }
                else
                {
                    temp = new byte[2];
                    temp[0] = 0x28;
                    temp[1] = (byte)myElement;
                    retVal.AddRange(temp);
                }
            }

            return retVal.ToArray();
        }

        private static byte[] BuildSymbolicSegment(string stringSegment)
        {
            //This is an easy one...
            int len = stringSegment.Length;
            int arrayLen = len + (len % 2) + 2;     //Add pad if necessary
            byte[] retVal = new byte[arrayLen];
            retVal[0] = 0x91;
            retVal[1] = (byte)len;
            Buffer.BlockCopy(System.Text.ASCIIEncoding.ASCII.GetBytes(stringSegment), 0, retVal, 2, (byte)len);

            return retVal;
        }

        public static byte[] BuildIOI(uint ioiInstance)
        {
            //There are only two versions of this, but I suspect there is support for
            //a 4 byte version, so we'll add it...

            byte[] retVal;

            if (ioiInstance <= 0xFF)
            {
                retVal = new byte[2];
                retVal[0] = 0x24;
                retVal[1] = (byte)ioiInstance;
            }
            else if (ioiInstance <= 0xFFFF)
            {
                retVal = new byte[4];
                retVal[0] = 0x25;
                retVal[1] = 0x00;
                retVal[2] = (byte)(ioiInstance & 0xFF);
                retVal[3] = (byte)((ioiInstance & 0xFF00) >> 8);
            }
            else
            {
                retVal = new byte[6];
                retVal[0] = 0x26;
                retVal[1] = 0x00;
                retVal[2] = (byte)(ioiInstance & 0xFF);
                retVal[3] = (byte)((ioiInstance & 0xFF00) >> 8);
                retVal[4] = (byte)((ioiInstance & 0xFF0000) >> 16);
                retVal[5] = (byte)((ioiInstance & 0xFF000000) >> 24);
            }

            return retVal;
        }
    }
}
