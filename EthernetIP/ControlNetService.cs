using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET
{
    /// <summary>
    /// ControlNet Services
    /// </summary>
    public enum ControlNetService
    {
        /// <summary>
        /// Returns the contents of all attributes of an object or class.
        /// </summary>
        GetAttributeAll = 0x01,
        /// <summary>
        /// Modifies the contents of the attributes of the class or object.
        /// </summary>
        GetAttributeAll_Request = 0x02,
        /// <summary>
        /// The Get_Attribute_List service shall return the contents of the
        /// selected gettable attributes of the specified object class or
        /// instance.
        /// </summary>
        GetAttributeList = 0x03,
        /// <summary>
        /// The Set_Attribute_List service shall set the contents of selected
        /// attributes of the specified object class or instance.
        /// </summary>
        SetAttributeList = 0x04,
        /// <summary>
        /// Invokes the Reset service of the specified Class/Object. Typically
        /// this would cause a transition to a default state or mode.
        /// </summary>
        Reset = 0x05,
        /// <summary>
        /// Invokes the Start service of the specified Class/Object. Typically
        /// this would place an object into a running state/mode.
        /// </summary>
        Start = 0x06,
        /// <summary>
        /// Invokes the Stop service of the specified Class/Object. Typically
        /// this would place an object into a stopped or idle state/mode.
        /// </summary>
        Stop = 0x07,
        /// <summary>
        /// Results in the instantiation of a new object within the specified
        /// class.
        /// </summary>
        Create = 0x08,
        /// <summary>
        /// Deletes an object instance of the specified class.
        /// </summary>
        Delete = 0x09,
        /// <summary>
        /// Performs a set of services as an autonomous sequence.
        /// </summary>
        MultipleServicePacket = 0x0A,
        /// <summary>
        /// Causes attribute values whos use is pending to become actively
        /// used.
        /// </summary>
        ApplyAttributes = 0x0D,
        /// <summary>
        /// Gets the contents of the specified attribute.
        /// </summary>
        GetAttributeSingle = 0x0E,
        /// <summary>
        /// Modifies an attribute value.
        /// </summary>
        SetAttributeSingle = 0x10,
        /// <summary>
        /// This service is supported at the class level only. It causes the
        /// specified Class to search for adn return a list of Instance IDs
        /// associated with existing Object Instances. Existing objects are
        /// those that are currently accessible from the CIP subnet.
        /// </summary>
        FindNextObjectInstance = 0x11,
        DN_ErrorResponse = 0x14,
        /// <summary>
        /// Restores the contents of a class/object's attributes from a
        /// storage location accessible by the Save service. Attribute data
        /// is copied from a storage area to the currently active memory
        /// area used by the class/object.
        /// </summary>
        Restore = 0x15,
        /// <summary>
        /// Copies the contents of a class/object's attributes to a location
        /// accessible by the Restore service.
        /// </summary>
        Save = 0x16,
        /// <summary>
        /// This service merely causes the receiving object to return a No
        /// Operation response. The receiving object does not carry out any
        /// other internal action. This service can be used to test whether
        /// or not a particular object is still present and responding
        /// without causing a state change.
        /// </summary>
        NOP = 0x17,
        /// <summary>
        /// Returns member(s) information from within an attribute.
        /// </summary>
        GetMember = 0x18,
        /// <summary>
        /// Sets member(s) information within an attribute.
        /// </summary>
        SetMember = 0x19,
        /// <summary>
        /// Inserts member(s) into an attribute.
        /// </summary>
        InsertMember = 0x1A,
        /// <summary>
        /// Removes member(s) from an attribute.
        /// </summary>
        RemoveMember = 0x1B,
        /// <summary>
        /// Used to read Allen-Bradley ControlLogix Tags
        /// </summary>
        CIP_ReadData = 0x4C,
        /// <summary>
        /// Used to write Allen-Bradley ControlLogix Tags
        /// </summary>
        CIP_WriteData = 0x4D,
        /// <summary>
        /// Used to read fragmented Allen-Bradley ControlLogix Tags
        /// </summary>
        CIP_ReadDataFragmented = 0x52,
        /// <summary>
        /// Used to write fragmented Allen-Bradley ControlLogix Tags
        /// </summary>
        CIP_WriteDataFragmented = 0x53
    }
}
