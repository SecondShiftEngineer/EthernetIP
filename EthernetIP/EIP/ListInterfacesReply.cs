using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.EIP
{
    public abstract class ListInterfacesReply
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
        /// Implicitly convert from an EncapsReply to a ListInterfacesReply
        /// </summary>
        /// <param name="reply">Reply to convert</param>
        /// <returns>Either ListInterfacesReply_Success or ListInterfacesReply_Failure</returns>
        public static implicit operator ListInterfacesReply(EncapsReply reply)
        {
            //We are mostly interested in the reply data from our parent...
            if (reply.Status == 0)
            {
                ListInterfacesReply_Success goodReply = new ListInterfacesReply_Success();
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
                ListInterfacesReply_Failure failReply = new ListInterfacesReply_Failure();
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

    public class ListInterfacesReply_Success : ListInterfacesReply
    {
        /// <summary>
        /// Number of items to follow
        /// </summary>
        public ushort ItemCount { get; internal set; }

        /// <summary>
        /// Interface Information in the Common Packet Item format
        /// </summary>
        public CommonPacketItem[] Items { get; internal set; }


        internal void Expand(byte[] sourceArray)
        {
            ItemCount = BitConverter.ToUInt16(sourceArray, 0);

            if (ItemCount == 0)
                return;

            Items = new CommonPacketItem[ItemCount];
            int offset = 1;

            for (int i = 0; i < ItemCount; i++)
            {
                offset += 1;
                CommonPacketItem cpi = new CommonPacketItem();
                cpi.Expand(sourceArray, offset, out offset);
                Items[i] = cpi;
            }
        }
    }

    public class ListInterfacesReply_Failure : ListInterfacesReply
    {
        //No failure reply actually defined for this service
    }


}
