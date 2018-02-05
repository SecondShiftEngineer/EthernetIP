using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public enum ClassCode : byte
    {
        Identity = 0x01,
        MessageRouter = 0x02,
        DeviceNet = 0x03,
        Assembly = 0x04,
        Connection = 0x05,
        ConnectionManager = 0x06,
        Register = 0x07,
        DiscreteInputPoint = 0x08,
        DiscreteOutputPoint = 0x09,
        AnalogInputPoint = 0x0A,
        AnalogOutputPoint = 0x0B,
        PresenceSensing = 0x0E,
        Parameter = 0x0F,
        ParameterGroup = 0x10,
        Group = 0x12,
        DiscreteInputGroup = 0x1D,
        DiscreteOutputGroup = 0x1E,
        DiscreteGroup = 0x1F,
        AnalogInputGroup = 0x20,
        AnalogOutputGroup = 0x21,
        AnalogGroup = 0x22,
        PositionSensorObject = 0x23,
        PositionControllerSupervisorObject = 0x24,
        PositionControllerObject = 0x25,
        BlockSequencerObject = 0x26,
        CommandBlockObject = 0x27,
        MotorDataObject = 0x28,
        ControlSupervisorObject = 0x29,
        AcDcDriveObject = 0x2A,
        AcknowledgeHandlerObject = 0x2B,
        OverloadObject = 0x2C,
        SoftStartObject = 0x2D,
        SelectionObject = 0x2E,
        SDeviceSupervisorObject = 0x30,
        SAnalogSensorObject = 0x31,
        SAnalogActuatorObject = 0x32,
        SSingleStageControllerObject = 0x33,
        SGasCalibrationObject = 0x34,
        TripPointObject = 0x35,
        ControlNetObject = 0xF0,
        ControlNetKeeperObject = 0xF1,
        ControlNetSchedulingObject = 0xF2,
        ConnectionConfigurationObject = 0xF3,
        PortObject = 0xF4,
        TCPIPInterfaceObject = 0xF5,
        EtherNetLinkObject = 0xF6
    }
}
