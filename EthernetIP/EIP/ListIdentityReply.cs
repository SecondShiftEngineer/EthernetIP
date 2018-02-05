using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.EIP
{
    /// <summary>
    /// List Identity Reply
    /// </summary>
    public abstract class ListIdentityReply
    {
        /// <summary>
        /// Encapsulation Command, see <c cref="EIPNET.EncapsCommand"/>
        /// </summary>
        public ushort ReplyCommand { get; internal set; }
        /// <summary>
        /// Length of the data portion in bytes
        /// </summary>
        public ushort Length { get; internal set; }
        /// <summary>
        /// Session handle
        /// </summary>
        public uint SessionHandle { get; internal set; }
        /// <summary>
        /// Status Code, see <c cref="EIPNET.EncapsStatusCode"/>
        /// </summary>
        public uint Status { get; internal set; }

        private byte[] _senderContext = new byte[8];
        /// <summary>
        /// Sender Context
        /// </summary>
        /// <remarks>Information only pertinent to the sender of the encaps command. Must be 8 bytes.</remarks>
        public byte[] SenderContext
        {
            get { return _senderContext; }
            internal set
            {
                if (value == null)
                    _senderContext = new byte[8];

                if (value.Length > 8)
                {
                    _senderContext = new byte[8];
                    Array.Copy(value, _senderContext, 8);
                }

                if (value.Length < 8)
                {
                    _senderContext = new byte[8];
                    Array.Copy(value, _senderContext, value.Length);
                }
            }
        }
        /// <summary>
        /// Options Flags
        /// </summary>
        public uint OptionsFlags { get; internal set; }

        /// <summary>
        /// Implicitly convert from an EncapsReply to a ListIdentityReply
        /// </summary>
        /// <param name="reply">Reply to convert</param>
        /// <returns>Either ListIdentityReply_Success or ListIdentityReply_Failure</returns>
        public static implicit operator ListIdentityReply(EncapsReply reply)
        {
            //We are mostly interested in the reply data from our parent...
            if (reply.Status == 0)
            {
                ListIdentityReply_Success goodReply = new ListIdentityReply_Success();
                goodReply.ReplyCommand = reply.ReplyCommand;
                goodReply.Length = reply.Length;
                goodReply.SessionHandle = reply.SessionHandle;
                goodReply.Status = reply.Status;
                goodReply._senderContext = reply.SenderContext;
                goodReply.OptionsFlags = reply.OptionsFlags;
                goodReply.Expand(reply.EncapsData);

                return goodReply;
            }
            else
            {
                ListIdentityReply_Failure failReply = new ListIdentityReply_Failure();
                failReply.ReplyCommand = reply.ReplyCommand;
                failReply.Length = reply.Length;
                failReply.SessionHandle = reply.SessionHandle;
                failReply.Status = reply.Status;
                failReply._senderContext = reply.SenderContext;
                failReply.OptionsFlags = reply.OptionsFlags;

                return failReply;
            }
        }
    }

    /// <summary>
    /// Successful ListIdentity Reply
    /// </summary>
    public class ListIdentityReply_Success : ListIdentityReply
    {
        /// <summary>
        /// Number of IdentityItems to follow
        /// </summary>
        public ushort ItemCount { get; internal set; }

        /// <summary>
        /// List of IdentityItems
        /// </summary>
        public CIP.IdentityItem[] IdentityItems { get; internal set; }

        internal void Expand(byte[] sourceArray)
        {
            ItemCount = BitConverter.ToUInt16(sourceArray, 0);

            int offset = 1;
            if (ItemCount > 0)
                IdentityItems = new CIP.IdentityItem[ItemCount];
            else
                return;

            for (int i = 0; i < ItemCount; i++)
            {
                offset += 1;
                CIP.IdentityItem ii = new CIP.IdentityItem();
                ii.Expand(sourceArray, offset, out offset);
                IdentityItems[i] = ii;
            }
        }
    }

    /// <summary>
    /// Failed ListIdentity Reply
    /// </summary>
    public class ListIdentityReply_Failure : ListIdentityReply
    {
        //There really isn't a failure reply for this service...
    }

}
