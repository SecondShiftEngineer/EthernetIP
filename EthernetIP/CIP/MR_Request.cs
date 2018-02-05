using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace EIPNET.CIP
{
    /// <summary>
    /// Message Router Request Format
    /// </summary>
    public class MR_Request : IPackable
    {
        /// <summary>
        /// Service code of the request
        /// </summary>
        public byte Service
        {
            get;
            set;
        }

        /// <summary>
        /// Number of 16 bit words in the Request_Path field
        /// </summary>
        /// <remarks>This field will automatically be set to the correct
        /// number of words in the Pack() routine.</remarks>
        public byte Request_Path_Size
        {
            get;
            set;
        }

        /// <summary>
        /// Request path, padded to 16 bit boundary
        /// </summary>
        /// <remarks>If the Request_Path is not padded to a 16 bit boundary,
        /// it will be automatically padded in the Pack() routine.</remarks>
        public byte[] Request_Path
        {
            get;
            set;
        }

        /// <summary>
        /// Service specific data to be delivered in the Explicit Messaging Request. If no additional data is to be sent with the Explicit Messaging Request, then this array will be empty.
        /// </summary>
        public byte[] Request_Data
        {
            get;
            set;
        }
    
        /// <summary>
        /// Packs the data into a byte array for encapsulation
        /// </summary>
        /// <returns>Byte array representing the MR_Request</returns>
        public byte[] Pack()
        {
            if (Request_Path == null)
                throw new NullReferenceException("Request_Path cannot be null");

            byte rPathSize = (byte)(Request_Path.Length / 2 + (Request_Path.Length % 2));
            Request_Path_Size = (byte)(rPathSize);

            byte[] retVal = new byte[2 + (rPathSize * 2) + (Request_Data == null ? 0 : Request_Data.Length)];

            retVal[0] = Service;
            retVal[1] = Request_Path_Size;

            Buffer.BlockCopy(Request_Path, 0, retVal, 2, Request_Path.Length);

            if (Request_Data != null)
            {
                int offset = retVal.Length - Request_Data.Length;
                Buffer.BlockCopy(Request_Data, 0, retVal, 2 + (Request_Path_Size * 2), Request_Data.Length);
            }

            return retVal;
        }
    }
}
