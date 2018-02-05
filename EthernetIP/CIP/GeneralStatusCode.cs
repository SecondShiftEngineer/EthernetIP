using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// CIP General Status Codes
    /// </summary>
    public enum GeneralStatusCode : byte
    {
        /// <summary>
        /// Service was successfully performed.
        /// </summary>
        Success = 0x00,
        /// <summary>
        /// A connection related service failed along the connection path.
        /// </summary>
        ConnectionFailure = 0x01,
        /// <summary>
        /// Resources needed for the object to perform the  requested service
        /// were unavailable.
        /// </summary>
        ResourceUnavailable = 0x02,
        /// <summary>
        /// See CIPStatusCodes.InvalidParameter, which is the preferred value
        /// to use for this condition.
        /// </summary>
        InvalidParameterValue = 0x03,
        /// <summary>
        /// The path segment identifier or the segment syntax was not understood
        /// by the processing node. Path processing shall stop when a path segment
        /// error is encountered.
        /// </summary>
        PathSegmentError = 0x04,
        /// <summary>
        /// The path is referencing an object class, instance, or structure element
        /// that is not known or is not contained in the processing node. Path 
        /// processing shall stop when a path destination unknown error is encountered.
        /// </summary>
        PathDestinationUnknown = 0x05,
        /// <summary>
        /// Only part of the expected data was transferred.
        /// </summary>
        PartialTransfer = 0x06,
        /// <summary>
        /// The messaging connection was lost.
        /// </summary>
        ConnectionLost = 0x07,
        /// <summary>
        /// The requested service was not implemented or was not defined for this
        /// object Class/Instance.
        /// </summary>
        ServiceNotSupported = 0x08,
        /// <summary>
        /// Invalid attribute data detected.
        /// </summary>
        InvalidAttributeValue = 0x09,
        /// <summary>
        /// An attribute in the Get_Attribute_List or Set_Attribute_List response
        /// has a non-zero status.
        /// </summary>
        AttributeListError = 0x0A,
        /// <summary>
        /// The object is already in the mode/state being requested by the service.
        /// </summary>
        AlreadyInRequestedModeOrState = 0x0B,
        /// <summary>
        /// The object cannot perform the requested service in its current state/mode.
        /// </summary>
        ObjectStateConflict = 0x0C,
        /// <summary>
        /// The requested instance of object to be created already exists.
        /// </summary>
        ObjectAlreadyExists = 0x0D,
        /// <summary>
        /// A request to modify a non-modifiable attribute was received.
        /// </summary>
        AttributeNotSettable = 0x0E,
        /// <summary>
        /// A permission/privilege check failed.
        /// </summary>
        PrivilegeViolation = 0x0F,
        /// <summary>
        /// The device's current mode/state prohibits the execution of the requested
        /// service.
        /// </summary>
        DeviceStateConflict = 0x10,
        /// <summary>
        /// The data to be transmitted in the response buffer is larger than the
        /// allocated response buffer.
        /// </summary>
        ReplyDataTooLarge = 0x11,
        /// <summary>
        /// The service specified an operation that is going to fragment a primitive
        /// data value, i.e. half a REAL data type.
        /// </summary>
        FragmentationOfPrimitiveValue = 0x12,
        /// <summary>
        /// The service did not supply enough data to perform the requested operation.
        /// </summary>
        NotEnoughData = 0x13,
        /// <summary>
        /// The attribute specified in the request is not supported.
        /// </summary>
        AttributeNotSupported = 0x14,
        /// <summary>
        /// The service was supplied with more data than was expected.
        /// </summary>
        TooMuchData = 0x15,
        /// <summary>
        /// The object specified does not exist on the device.
        /// </summary>
        ObjectDoesNotExist = 0x16,
        /// <summary>
        /// The fragmentation sequence for this service is not currently active for
        /// this data.
        /// </summary>
        SvcFrag_SeqncNotInProgress = 0x17,
        /// <summary>
        /// The attribute data of this object was not saved prior to the requested
        /// service.
        /// </summary>
        NoStoredAttributeData = 0x18,
        /// <summary>
        /// The attribute data of this object was not saved due to a failure following
        /// the attempt.
        /// </summary>
        StoreOperationFailure = 0x19,
        /// <summary>
        /// The service reqeust packet was too large for transmission on a network
        /// in the path to the destination. The routing device was forced to abort the
        /// service.
        /// </summary>
        RoutingFailure_RequestSize = 0x1A,
        /// <summary>
        /// The service response packet was too large for transmission on a network in the
        /// path from the destination. The routing device was forced to abort the service.
        /// </summary>
        RoutingFailure_ResponseSize = 0x1B,
        /// <summary>
        /// The service did not supply an attribute in a list of attributes that was needed by
        /// the service to perform the requested behavior.
        /// </summary>
        MissingAttributeListEntry = 0x1C,
        /// <summary>
        /// The service is returning the list of attributes supplied with status information
        /// for those attributes that were invalid.
        /// </summary>
        InvalidAttributeList = 0x1D,
        /// <summary>
        /// An embedded service resulted in an error.
        /// </summary>
        EmbeddedServiceError = 0x1E,
        /// <summary>
        /// A vendor specific error has been encountered. The Additional Code Field of
        /// the Error Response defines the particular error encountered. Use of this
        /// General Error Code should only be performed when none of the Error Codes
        /// presented in this table or within an Object Class definition accurately reflect
        /// the error.
        /// </summary>
        VendorSpecific = 0x1F,
        /// <summary>
        /// A parameter associated with the request was invalid. This code is used when a
        /// parameter does not meet the requirements of this specification and/or the
        /// requirements defined in an Application Object Specification.
        /// </summary>
        InvalidParameter = 0x20,
        /// <summary>
        /// An attempt was made to write to a write-once medium (e.g. WORM drive,
        /// PROM) that has already been written, or to modify a value that cannot be
        /// changed once established.
        /// </summary>
        WriteOnceWritten = 0x21,
        /// <summary>
        /// An invalid reply is received (e.g. reply service code does not match the request
        /// service code, or reply message is shorter than the minimum expected reply
        /// size). This status code can serve for other causes of invalid replies.
        /// </summary>
        InvalidReplyReceived = 0x22,
        /// <summary>
        /// The Key Segment that was included as the first segment in the path does not
        /// match the destination module. The object specific status shall indicate which
        /// part of the key check failed.
        /// </summary>
        KeyFailureInPath = 0x25,
        /// <summary>
        /// The size of the path which was sent with the Service Request is either not large
        /// enough to allow the Request to be routed to an object or too much routing data
        /// was included.
        /// </summary>
        PathSizeInvalid = 0x26,
        /// <summary>
        /// An attempt was made to set an attribute that is not able to be set at this time.
        /// </summary>
        UnexpectedAttribute = 0x27,
        /// <summary>
        /// The Member ID specified in the request does not exist in the specified
        /// Class/Instance/Attribute
        /// </summary>
        InvalidMemberID = 0x28,
        /// <summary>
        /// A request to modify a non-modifiable member was received
        /// </summary>
        MemberNotSettable = 0x29
    }
}
