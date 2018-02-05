
namespace EIPNET.CIP
{
    /// <summary>
    /// Connection Manager Object Instance Attributes
    /// </summary>
    public enum CMObject_Attributes
    {
        /// <summary>
        /// Number of Forward Open
        /// service requests received.
        /// </summary>
        OpenRequests = 1,
        /// <summary>
        /// Number of Forward Open
        /// service requests which were
        /// rejected due to bad format.
        /// </summary>
        OpenFormatRejects = 2,
        /// <summary>
        /// Number of Forward Open
        /// service requests which were
        /// rejected due to lack of
        /// resources.
        /// </summary>
        OpenResourceRejects = 3,
        /// <summary>
        /// Number of Forward Open
        /// service requests which were
        /// rejected for reasons other
        /// than bad format or lack of
        /// resources.
        /// </summary>
        OpenOtherRejects = 4,
        /// <summary>
        /// Number of Forward Close
        /// service requests received.
        /// </summary>
        CloseRequests = 5,
        /// <summary>
        /// Number of Forward Close
        /// service requests which were
        /// rejected due to bad format.
        /// </summary>
        CloseFormatRequests = 6,
        /// <summary>
        /// Number of Forward Open
        /// service requests which were
        /// rejected for reasons other
        /// than bad format.
        /// </summary>
        CloseOtherRequests = 7,
        /// <summary>
        /// Number of connection
        /// timeouts which have
        /// occurred.
        /// </summary>
        ConnectionTimeouts = 8,
        /// <summary>
        /// Defines timing associated
        /// with this Connection
        /// </summary>
        ConnectionEntries = 9,
        /// <summary>
        /// Range of 0 to 1000 representing 0% to 100%
        /// </summary>
        CPUUtilization = 11,
        /// <summary>
        /// Size in bytes
        /// </summary>
        MaxBuffSize = 12,
        /// <summary>
        /// Size in bytes
        /// </summary>
        BuffSizeRemaining = 13
    }
}
