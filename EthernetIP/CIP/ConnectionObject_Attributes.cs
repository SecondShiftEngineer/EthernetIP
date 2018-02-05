
namespace EIPNET.CIP
{
    /// <summary>
    /// Connection Object Instance Attributes
    /// </summary>
    public enum ConnectionObject_Attributes : byte
    {
        /// <summary>
        /// State of the object
        /// </summary>
        State = 1,
        /// <summary>
        /// Indicates either I/O or Messaging Connection
        /// </summary>
        Instance_Type = 2,
        /// <summary>
        /// Defines behavior of the Connection
        /// </summary>
        TransportClass_Trigger = 3,
        /// <summary>
        /// Placed in CAN Identifier Field when the
        /// Connection transmits on a DeviceNet subnet
        /// </summary>
        DeviceNet_Produced_Connection_Id = 4,
        /// <summary>
        /// CAN Identifier Field value that denotes
        /// message to be received on a DeviceNet
        /// subnet.
        /// </summary>
        DeviceNet_Consumed_Connection_Id = 5,
        /// <summary>
        /// Defines the Message Group(s) across which
        /// productions and consumptions associated
        /// with this Connection occur on a DeviceNet
        /// subnet.
        /// </summary>
        DeviceNet_Initial_Communication_Characteristics = 6,
        /// <summary>
        ///Maximum number of bytes transmitted
        /// across this Connection
        /// </summary>
        Produced_Connection_Size = 7,
        /// <summary>
        /// Maximum number of bytes received across
        /// this Connection
        /// </summary>
        Consumed_Connection_Size = 8,
        /// <summary>
        /// Defines timing associated with this
        /// Connection
        /// </summary>
        Expected_Packet_Rate = 9,
        /// <summary>
        /// Identifies the message sent on the subnet by
        /// this connection.
        /// </summary>
        CIP_Produced_Connection_Id = 10,
        /// <summary>
        /// Identifies the message received from the
        /// subnet for this connection.
        /// </summary>
        CIP_Consumed_Connection_Id = 11,
        /// <summary>
        /// Defines how to handle Inactivity/Watchdog
        /// timeouts
        /// </summary>
        Watchdog_Timeout_Action = 12,
        /// <summary>
        /// Number of bytes in the
        /// produced_connection_path attribute
        /// </summary>
        Produced_Connection_Path_Length = 13,
        /// <summary>
        /// Specifies the Application Object(s) whose
        /// data is to be produced by this Connection
        /// Object. See CIP Common Specification, Appendix C.
        /// </summary>
        Produced_Connection_Path = 14,
        /// <summary>
        /// Number of bytes in the
        /// consumed_connection_path attribute
        /// </summary>
        Consumed_Connection_Path_Length = 15,
        /// <summary>
        /// Specifies the Application Object(s) that are to
        /// receive the data consumed by this
        /// Connection Object. See CIP Common Specification, Appendix C.
        /// </summary>
        Consumed_Connection_Path = 16,
        /// <summary>
        /// Defines minimum time between new data
        /// production. This attribute is required for all
        /// I/O Client connections, except those with a
        /// production trigger of Cyclic.
        /// </summary>
        Production_Inhibit_Time = 17
    }
}
