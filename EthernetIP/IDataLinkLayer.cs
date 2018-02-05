using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET
{
    internal interface IDataLinkLayer
    {
        void SendData(byte[] data);
        byte[] ReceiveData();
    }
}
