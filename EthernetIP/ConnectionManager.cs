using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.EIP;
using EIPNET.CIP;

namespace EIPNET
{
    /// <summary>
    /// Represents services of the Connection Manager Class Object
    /// </summary>
    public static class ConnectionManager
    {

        /// <summary>
        /// Forward Open Request
        /// </summary>
        /// <param name="si">Session Info</param>
        /// <param name="Path">Path to the target</param>
        /// <param name="O2T_Params">O2T_Params, leave as zero to automatically select</param>
        /// <returns>True if the ForwardOpen request was successful, false otherwise</returns>
        public static bool ForwardOpen(SessionInfo si, byte[] Path, short O2T_Params = 0)
		{
            ForwardOpenRequest fwdRequest = new ForwardOpenRequest(si.ConnectionParameters);

            fwdRequest.ConnectionTimeoutMultiplier = 0;

            if (O2T_Params == 0)
            {
                fwdRequest.O2T_ConnectionParameters = unchecked((short)(NetworkConnectionParams.Owned |               //NA
                                                              NetworkConnectionParams.Point2Point |           //Point2Point
                                                              NetworkConnectionParams.HighPriority |        //LowPriority
                                                              NetworkConnectionParams.Variable |               //Variable
                                                              508));         //508 uS
                fwdRequest.T2O_ConnectionParameters = fwdRequest.O2T_ConnectionParameters;
            }
            else
            {
                fwdRequest.O2T_ConnectionParameters = O2T_Params;
                fwdRequest.T2O_ConnectionParameters = O2T_Params;
            }

            //fwdRequest.O2T_RPI = 10000000;
            fwdRequest.O2T_RPI = 2000;
            fwdRequest.T2O_RPI = fwdRequest.O2T_RPI;
            fwdRequest.TransportTrigger = NetworkConnectionParams.CM_Transp_IsServer |      //CM_Transp_IsServer
                                          3 |
                                          NetworkConnectionParams.CM_Trig_App;
            fwdRequest.TransportTrigger = 0xA3;
            fwdRequest.Connection_Path = Path;

            EncapsReply response = MessageRouter.SendMR_Request(si, (byte)ConnectionManagerService.ForwardOpen, CommonPaths.ConnectionManager,
                fwdRequest.Pack());

            if (response == null)
                return false;

            //Need to check the last codes...
            si.LastSessionError = (int)response.Status;

            //We actually need to find out if this is a success or failure...
            ForwardOpenReply fReply = response;

            if (fReply is ForwardOpenReply_Fail)
                return false;

            //Need to copy some data into the connection parameters...
            ForwardOpenReply_Success fSucc = (ForwardOpenReply_Success)fReply;

            ConnectionParameters cParams = si.ConnectionParameters;
            cParams.O2T_CID = fSucc.O2T_ConnectionId;
            cParams.T2O_CID = fSucc.T2O_ConnectionId;
            cParams.O2T_API = fSucc.O2T_API;
            cParams.T2O_API = fSucc.T2O_API;
            si.ConnectionParameters = cParams;
            

            if (response.Status == 0)
                return true;

            return false;
        }
		
        /// <summary>
        /// Connects to a device over ControlNet
        /// </summary>
        /// <param name="si">Session Info</param>
        /// <param name="Path">Path to the target</param>
        /// <param name="O2T_Params">O2T_Params, leave as zero to automatically select</param>
        /// <returns>True if the request was successful, false otherwise</returns>
        public static bool ConnectOverControlNet(SessionInfo si, byte[] Path, short O2T_Params = 0)
		{
            byte[] tPath = new byte[Path.Length + CommonPaths.Router.Length];
            Buffer.BlockCopy(Path, 0, tPath, 0, Path.Length);
            Buffer.BlockCopy(CommonPaths.Router, 0, tPath, Path.Length, CommonPaths.Router.Length);

            return ForwardOpen(si, tPath, O2T_Params);
        }

        /// <summary>
        /// Closes a connection that was open with ForwardOpen or ConnectOverControlNet
        /// </summary>
        /// <param name="si">Session Info</param>
        /// <param name="Path">Path to the target</param>
        /// <returns>True if the request was successful, false otherwise</returns>
        public static bool ForwardClose(SessionInfo si, byte[] Path)
        {
            ForwardCloseRequest request = new ForwardCloseRequest();
            request.ConnectionSerialNumber = si.ConnectionParameters.ConnectionSerialNumber;
            request.VendorId = si.ConnectionParameters.VendorID;
            request.OriginatorSerialNumber = si.ConnectionParameters.OriginatorSerialNumber;
            byte[] tPath = new byte[Path.Length + CommonPaths.Router.Length];
            Buffer.BlockCopy(Path, 0, tPath, 0, Path.Length);
            Buffer.BlockCopy(CommonPaths.Router, 0, tPath, Path.Length, CommonPaths.Router.Length);
            request.Path = tPath;

            EncapsReply response = MessageRouter.SendMR_Request(si, (byte)ConnectionManagerService.ForwardClose, CommonPaths.ConnectionManager,
                request.Pack());

            if (response == null)
                return false;

            if (response.Status == 0)
                return true;

            return false;
        }

    }
}
