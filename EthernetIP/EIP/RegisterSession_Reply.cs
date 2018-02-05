using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.EIP
{
    /// <summary>
    /// Register Session Reply
    /// </summary>
    public abstract class RegisterSession_Reply
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
        /// Implicitly convert from an EncapsReply to a RegisterSession_Reply
        /// </summary>
        /// <param name="reply">Reply to convert</param>
        /// <returns>Either RegisterSession_Success or RegisterSession_Failure</returns>
        public static implicit operator RegisterSession_Reply(EncapsReply reply)
        {
            //We are mostly interested in the reply data from our parent...
            if (reply.Status == 0)
            {
                RegisterSession_Success goodReply = new RegisterSession_Success();
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
                RegisterSession_Failure failReply = new RegisterSession_Failure();
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
    /// Successful RegisterSession Reply
    /// </summary>
    public class RegisterSession_Success : RegisterSession_Reply
    {
        public ushort ProtocolVersion { get; internal set; }
        public ushort Options { get; internal set; }

        internal void Expand(byte[] sourceArray)
        {
            ProtocolVersion = BitConverter.ToUInt16(sourceArray, 0);
            Options = BitConverter.ToUInt16(sourceArray, 2);
        }
    }

    /// <summary>
    /// Failure RegisterSession Reply
    /// </summary>
    public class RegisterSession_Failure : RegisterSession_Reply
    {
        //There really isn't a failure reply for this service...
    }
}
