using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Message Router Response Format
    /// </summary>
    public class MR_Response : IExpandable
    {
        /// <summary>
        /// Reply service code
        /// </summary>
        public byte ReplyService
        {
            get;
            internal set;
        }

        /// <summary>
        /// Shall be zero
        /// </summary>
        public byte Reserved
        {
            get;
            internal set;
        }

        /// <summary>
        /// See the <c cref="EIPNET.CIP.GeneralStatusCode"/> enumeration.
        /// </summary>
        public byte GeneralStatus
        {
            get;
            internal set;
        }

        /// <summary>
        /// Number of 16 bit words in the Additional Status array
        /// </summary>
        public byte AdditionalStatus_Size
        {
            get;
            internal set;
        }

        /// <summary>
        /// Additional Status
        /// </summary>
        public ushort[] AdditionalStatus
        {
            get;
            internal set;
        }

        /// <summary>
        /// Response data from request or additional error data if General Status indicated an error.
        /// </summary>
        public byte[] ResponseData
        {
            get;
            internal set;
        }

        /// <summary>
        /// Expands the ByteArray into a MR_Request format
        /// </summary>
        /// <param name="DataArray">Source array</param>
        /// <param name="Offset">Offset where the MR_Request packet starts</param>
        /// <param name="NewOffset">Offset where the MR_Request packet ends</param>
        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            ReplyService = DataArray[Offset];
            Offset += 1;

            //Skip reserved...
            Offset++;

            GeneralStatus = DataArray[Offset];
            Offset++;

            AdditionalStatus_Size = DataArray[Offset];
            Offset++;

            if (AdditionalStatus_Size > 0)
            {
                AdditionalStatus = new ushort[AdditionalStatus_Size];
                for (int i = 0; i < AdditionalStatus_Size; i++)
                {
                    AdditionalStatus[i] = BitConverter.ToUInt16(DataArray, Offset);
                    Offset += 2;
                }
            }

            //Whatever is left goes into the response data...
            int bytesLeft = DataArray.Length - Offset;
            if (bytesLeft > 0)
            {
                byte[] temp = new byte[bytesLeft];
                Buffer.BlockCopy(DataArray, Offset, temp, 0, bytesLeft);
                ResponseData = temp;
            }

            NewOffset = DataArray.Length;
        }
    }
}
