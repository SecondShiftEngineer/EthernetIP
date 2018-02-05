using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.Resources;

namespace EIPNET.CIP
{
    /// <summary>
    /// Returns information about General Status Codes
    /// </summary>
    public static class GeneralStatus
    {
        /// <summary>
        /// Gets the name of the specified General Status Code
        /// </summary>
        /// <param name="GeneralStatusCode">General Status Code</param>
        /// <returns>Name of the associated General Status Code</returns>
        public static string GetGeneralStatusName(byte GeneralStatusCode)
        {
            switch (GeneralStatusCode)
            {
                case 0x00:
                    return strings.GeneralStatusName_0x00;
                case 0x01:
                    return strings.GeneralStatusName_0x01;
                case 0x02:
                    return strings.GeneralStatusName_0x02;
                case 0x03:
                    return strings.GeneralStatusName_0x03;
                case 0x04:
                    return strings.GeneralStatusName_0x04;
                case 0x05:
                    return strings.GeneralStatusName_0x05;
                case 0x06:
                    return strings.GeneralStatusName_0x06;
                case 0x07:
                    return strings.GeneralStatusName_0x07;
                case 0x08:
                    return strings.GeneralStatusName_0x08;
                case 0x09:
                    return strings.GeneralStatusName_0x09;
                case 0x0A:
                    return strings.GeneralStatusName_0x0A;
                case 0x0B:
                    return strings.GeneralStatusName_0x0B;
                case 0x0C:
                    return strings.GeneralStatusName_0x0C;
                case 0x0D:
                    return strings.GeneralStatusName_0x0D;
                case 0x0E:
                    return strings.GeneralStatusName_0x0E;
                case 0x0F:
                    return strings.GeneralStatusName_0x0F;
                case 0x10:
                    return strings.GeneralStatusName_0x10;
                case 0x11:
                    return strings.GeneralStatusName_0x11;
                case 0x12:
                    return strings.GeneralStatusName_0x12;
                case 0x13:
                    return strings.GeneralStatusName_0x13;
                case 0x14:
                    return strings.GeneralStatusName_0x14;
                case 0x15:
                    return strings.GeneralStatusName_0x15;
                case 0x16:
                    return strings.GeneralStatusName_0x16;
                case 0x17:
                    return strings.GeneralStatusName_0x17;
                case 0x18:
                    return strings.GeneralStatusName_0x18;
                case 0x19:
                    return strings.GeneralStatusName_0x19;
                case 0x1A:
                    return strings.GeneralStatusName_0x1A;
                case 0x1B:
                    return strings.GeneralStatusName_0x1B;
                case 0x1C:
                    return strings.GeneralStatusName_0x1C;
                case 0x1D:
                    return strings.GeneralStatusName_0x1D;
                case 0x1E:
                    return strings.GeneralStatusName_0x1E;
                case 0x1F:
                    return strings.GeneralStatusName_0x1F;
                case 0x20:
                    return strings.GeneralStatusName_0x20;
                case 0x21:
                    return strings.GeneralStatusName_0x21;
                case 0x22:
                    return strings.GeneralStatusName_0x22;
                case 0x25:
                    return strings.GeneralStatusName_0x25;
                case 0x26:
                    return strings.GeneralStatusName_0x26;
                case 0x27:
                    return strings.GeneralStatusName_0x27;
                case 0x28:
                    return strings.GeneralStatusName_0x28;
                case 0x29:
                    return strings.GeneralStatusName_0x29;
                case 0x2A:
                    return strings.GeneralStatusName_0x2A;
                default:
                    return strings.GeneralStatus_Unknown + " - 0x" + GeneralStatusCode.ToString("X2");
            }
        }

        /// <summary>
        /// Gets the description of the specified General Status Code
        /// </summary>
        /// <param name="GeneralStatusCode">General Status Code</param>
        /// <returns>Description of the associated General Status Code</returns>
        public static string GetGeneralStatusDescription(byte GeneralStatusCode)
        {
            switch (GeneralStatusCode)
            {
                case 0x00:
                    return strings.GeneralStatusDesc_0x00;
                case 0x01:
                    return strings.GeneralStatusDesc_0x01;
                case 0x02:
                    return strings.GeneralStatusDesc_0x02;
                case 0x03:
                    return strings.GeneralStatusDesc_0x03;
                case 0x04:
                    return strings.GeneralStatusDesc_0x04;
                case 0x05:
                    return strings.GeneralStatusDesc_0x05;
                case 0x06:
                    return strings.GeneralStatusDesc_0x06;
                case 0x07:
                    return strings.GeneralStatusDesc_0x07;
                case 0x08:
                    return strings.GeneralStatusDesc_0x08;
                case 0x09:
                    return strings.GeneralStatusDesc_0x09;
                case 0x0A:
                    return strings.GeneralStatusDesc_0x0A;
                case 0x0B:
                    return strings.GeneralStatusDesc_0x0B;
                case 0x0C:
                    return strings.GeneralStatusDesc_0x0C;
                case 0x0D:
                    return strings.GeneralStatusDesc_0x0D;
                case 0x0E:
                    return strings.GeneralStatusDesc_0x0E;
                case 0x0F:
                    return strings.GeneralStatusDesc_0x0F;
                case 0x10:
                    return strings.GeneralStatusDesc_0x10;
                case 0x11:
                    return strings.GeneralStatusDesc_0x11;
                case 0x12:
                    return strings.GeneralStatusDesc_0x12;
                case 0x13:
                    return strings.GeneralStatusDesc_0x13;
                case 0x14:
                    return strings.GeneralStatusDesc_0x14;
                case 0x15:
                    return strings.GeneralStatusDesc_0x15;
                case 0x16:
                    return strings.GeneralStatusDesc_0x16;
                case 0x17:
                    return strings.GeneralStatusDesc_0x17;
                case 0x18:
                    return strings.GeneralStatusDesc_0x18;
                case 0x19:
                    return strings.GeneralStatusDesc_0x19;
                case 0x1A:
                    return strings.GeneralStatusDesc_0x1A;
                case 0x1B:
                    return strings.GeneralStatusDesc_0x1B;
                case 0x1C:
                    return strings.GeneralStatusDesc_0x1C;
                case 0x1D:
                    return strings.GeneralStatusDesc_0x1D;
                case 0x1E:
                    return strings.GeneralStatusDesc_0x1E;
                case 0x1F:
                    return strings.GeneralStatusDesc_0x1F;
                case 0x20:
                    return strings.GeneralStatusDesc_0x20;
                case 0x21:
                    return strings.GeneralStatusDesc_0x21;
                case 0x22:
                    return strings.GeneralStatusDesc_0x22;
                case 0x25:
                    return strings.GeneralStatusDesc_0x25;
                case 0x26:
                    return strings.GeneralStatusDesc_0x26;
                case 0x27:
                    return strings.GeneralStatusDesc_0x27;
                case 0x28:
                    return strings.GeneralStatusDesc_0x28;
                case 0x29:
                    return strings.GeneralStatusDesc_0x29;
                case 0x2A:
                    return strings.GeneralStatusDesc_0x2A;
                default:
                    return strings.GeneralStatus_Unknown + " - 0x" + GeneralStatusCode.ToString("X2");
            }
        }

    }
}
