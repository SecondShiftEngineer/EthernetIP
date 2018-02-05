using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EIPNET.CIP
{
    internal class LinkConsumer
    {
        public byte State { get; set; }
        public ushort ConnectionId { get; set; }

        #region Services

        public static LinkConsumer Create()
        {
            LinkConsumer linkCons = new LinkConsumer();
            linkCons.State = 1;
            return linkCons;
        }

        public static void Delete(LinkConsumer linkCons)
        {
            linkCons.State = 0;
            linkCons = null;
        }

        #endregion

        #region Object Instance Services

        public void Send(Stream ioStream, byte[] data)
        {
            ioStream.Write(data, 0, data.Length);
        }

        public byte[] Get_Attribute(ushort attributeId)
        {
            return null;
        }

        public void Set_Attribute(ushort attributeId, byte[] value)
        {

        }

        #endregion
    }
}
