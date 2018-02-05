
namespace EIPNET.CIP
{
    /// <summary>
    /// Common CIP Paths
    /// </summary>
    public static class CommonPaths
    {
        /// <summary>
        /// Path to the Connection Manager object
        /// </summary>
        public static byte[] ConnectionManager = new byte[] { 0x20, 0x06, 0x24, 0x01 };
        /// <summary>
        /// Path to the Router object
        /// </summary>
        public static byte[] Router = new byte[] { 0x20, 0x02, 0x24, 0x01 };
        /// <summary>
        /// Path to the Backplane Data object
        /// </summary>
        public static byte[] BackplaneData = new byte[] { 0x20, 0x66, 0x24, 0x01 };
        /// <summary>
        /// Path to the PCCC object
        /// </summary>
        public static byte[] PCCC = new byte[] { 0x20, 0x67, 0x24, 0x01 };
        /// <summary>
        /// Path to the Data Highway Plus Channel A Proxy object
        /// </summary>
        public static byte[] DHPA_Proxy = new byte[] { 0x20, 0xA6, 0x24, 0x01, 0x2C, 0x01 };
        /// <summary>
        /// Path to the Data Highway Plus Channel B Proxy object
        /// </summary>
        public static byte[] DHPB_Proxy = new byte[] { 0x20, 0xA6, 0x24, 0x02, 0x2C, 0x01 };
        /// <summary>
        /// Path to the Identity Object (for use in an MR Request)
        /// </summary>
        public static byte[] IdentityObject = new byte[] { 0x20, 0x01, 0x24, 0x01 };
    }
}
