using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET
{
    interface IExpandable
    {
        void Expand(byte[] DataArray, int Offset, out int NewOffset);
    }
}
