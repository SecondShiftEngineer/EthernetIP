using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EIPNET.CIP
{
    internal class LinkProducer
    {

        public byte State { get; set; }
        public ushort ConnectionId { get; set; }

        #region Services

        public static LinkProducer Create()
        {
            LinkProducer linkProd = new LinkProducer();
            linkProd.State = 1;
            return linkProd;
        }

        public static void Delete(LinkProducer linkProd)
        {
            linkProd.State = 0;
            linkProd = null;
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
