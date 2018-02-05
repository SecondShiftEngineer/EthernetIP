using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace EIPNET.EIP
{
    public class SessionManager
    {

        /// <summary>
        /// Creates a new session and registers it with the device
        /// </summary>
        /// <param name="hostNameOrIp">Host name or IP to connect to</param>
        /// <param name="port">Port to connect to (default: 0xAF12)</param>
        /// <param name="senderContext">Sender Context</param>
        /// <returns>Session Info object, check the SessionInfo.Connected and SessionInfo.Registered values</returns>
        public static SessionInfo CreateAndRegister(string hostNameOrIp, int port = 0xAF12, byte[] senderContext = null)
		{
            SessionInfo si = new SessionInfo();
            si.SessionSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Unspecified);
            si.Connected = false;
            si.Registered = false;
            si.HostNameOrIp = hostNameOrIp;
            si.Port = port;
            si.SenderContext = senderContext;

            //First we connect...
            try
            {
                si.SessionSocket.Connect(hostNameOrIp, port);
                si.Connected = true;
            }
            catch (Exception e) { si.LastSessionErrorStack = e.StackTrace; si.LastSessionErrorString = e.Message; return si; }
            
            //Now we register the session
            try
            {
                EncapsPacket packet = EncapsPacketFactory.CreateRegisterSession(si.SenderContext);
                byte[] rawPacket = packet.Pack();

                byte[] rawReply = si.SendData_WaitReply(rawPacket);
                EncapsReply reply = new EncapsReply();
                int tmp = 0;
                reply.Expand(rawReply, 0, out tmp);

                //Now we need to figure out if we successfully registered...
                RegisterSession_Reply registerReply = reply;
                if (registerReply is RegisterSession_Success)
                {
                    si.SessionHandle = registerReply.SessionHandle;
                    si.Registered = true;
                }
                else
                {
                    si.Registered = false;
                    return si;
                }
            }
            catch (Exception e) { si.LastSessionErrorStack = e.StackTrace; si.LastSessionErrorString = e.Message; return si; }

            return si;
        }

        /// <summary>
        /// Unregisters the session with the device
        /// </summary>
        /// <param name="si">Session Info</param>
        public static void UnRegister(SessionInfo si)
        {
            if (!si.Connected)
                return;

            si.SessionSocket.Close();
            si.SessionSocket = null;
            si.Connected = false;
            si.Registered = false;
        }

        /// <summary>
        /// Verifies that the device supports CIP by searching for IdentityInfo item 0x0C
        /// </summary>
        /// <param name="si">Session Info</param>
        /// <returns>True if the device supports CIP messaging, false otherwise</returns>
        public static bool VerifyCIP(SessionInfo si)
        {
            if (!si.Connected)
                return false;

            EncapsPacket request = EncapsPacketFactory.CreateListIdentity();
            request.SessionHandle = si.SessionHandle;
            request.SenderContext = si.SenderContext;

            EncapsReply reply = new EncapsReply();
            byte[] rawReply = si.SendData_WaitReply(request.Pack());
            int temp = 0;
            reply.Expand(rawReply, 0, out temp);

            if (reply.Status != 0)
                return false;

            ListIdentityReply liReply = reply;

            if (liReply is ListIdentityReply_Failure)
                return false;

            ListIdentityReply_Success liSucc = (ListIdentityReply_Success)(liReply);

            for (int i = 0; i < liSucc.ItemCount; i++)
            {
                if (liSucc.IdentityItems[i].ItemTypeCode == 0x0C)
                {
                    si.IdentityInfo = liSucc.IdentityItems[i];
                    return true;
                }
            }

            return false;
        }

    }
}
