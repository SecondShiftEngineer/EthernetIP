using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.EIP;

namespace EIPNET.CIP
{
    public static class MessageRouter
    {

        public static MR_Request BuildMR_Request(byte Service, byte[] Path, byte RequestPathSize, byte[] RequestData)
        {
            MR_Request request = new MR_Request();
            request.Service = Service;
            request.Request_Path_Size = (byte)(RequestPathSize / 2 + (RequestPathSize % 2));
            request.Request_Path = Path;
            Buffer.BlockCopy(Path, 0, request.Request_Path, 0, Path.Length);
            request.Request_Data = RequestData;

            return request;
        }

        public static EncapsReply SendMR_Request(SessionInfo session, MR_Request request)
        {
            CommonPacketItem dataItem = CommonPacketItem.GetUnconnectedDataItem(request.Pack());

            EncapsReply response = session.SendRRData(CommonPacketItem.GetNullAddressItem(), dataItem);

            return response;
        }

        public static EncapsReply SendMR_Request(SessionInfo si, byte Service,
            byte[] Path, byte[] RequestBytes)
        {
            MR_Request request = BuildMR_Request(Service, Path, (byte)Path.Length, RequestBytes);

            return SendMR_Request(si, request);
        }

    }
}
