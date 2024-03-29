﻿using System.ComponentModel;

namespace TURAG.Feldbus.Types
{
    /// <summary>
    /// Error codes used by all classes in this library. Use 
    /// Devices.Device.ErrorString() to get the description of an error
    /// code.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// No Error.
        /// </summary>
        [Description("No Error")]
        Success,

        /// <summary>
        /// Unspecified Error.
        /// </summary>
        [Description("Unspecified Error")]
        Unspecified,

        /// <summary>
        /// Invalid argument.
        /// </summary>
        [Description("Invalid argument.")]
        InvalidArgument,

        /// <summary>
        /// The device does not support the requested operation.
        /// </summary>
        [Description("Unsupported operation.")]
        NotSupported,


        /// <summary>
        /// A checksum mismatch was detected in the received packet.
        /// </summary>
        [Description("A checksum mismatch was detected in the received packet.")]
        TransportChecksumError,

        /// <summary>
        /// The received response was shorter then expected.
        /// </summary>
        [Description("The received response was shorter then expected.")]
        TransportReceptionMissingDataError,

        /// <summary>
        /// No data was received.
        /// </summary>
        [Description("No data was received.")]
        TransportReceptionNoAnswerError,

        /// <summary>
        /// An error occured when sending the request/broadcast.
        /// </summary>
        [Description("An error occured when sending the request/broadcast.")]
        TransportTransmissionError,


        /// <summary>
        /// A device function was called before its initialization.
        /// </summary>
        [Description("A device function was called before its initialization.")]
        DeviceNotInitialized,

        /// <summary>
        /// This device does not support querying its transmission statistics.
        /// </summary>
        [Description("This device does not support querying its transmission statistics.")]
        DeviceStatisticsNotSupported,

        /// <summary>
        /// This device does not support querying its uptime.
        /// </summary>
        [Description("This device does not support querying its uptime.")]
        DeviceUptimeNotSupported,

        /// <summary>
        /// The device did not accept to set the given bus address.
        /// </summary>
        [Description("The device did not accept to set the given bus address.")]
        DeviceRejectedBusAddress,

        /// <summary>
        /// No device did assert the bus in response to a RequestBusAssertion broadcast.
        /// </summary>
        [Description("No device did assert the bus in response to a RequestBusAssertion broadcast.")]
        NoAssertionDetected,

        /// <summary>
        /// The supplied address or size was rejected when reading/writing the static storage.
        /// </summary>
        [Description("The supplied address or size was rejected when reading/writing the static storage.")]
        DeviceStaticStorageAddressSizeError,

        /// <summary>
        /// Writing to the device static storage failed.
        /// </summary>
        [Description("Writing to the device static storage failed.")]
        DeviceStaticStorageWriteError,


        /// <summary>
        /// The given key is invalid for this Stellantrieb device.
        /// </summary>
        [Description("The given key is invalid for this Stellantrieb device.")]
        StellantriebInvalidKey,

        /// <summary>
        /// The device reported a command set length of 0, which is unsupported.
        /// </summary>
        [Description("The device reported a command set length of 0, which is unsupported.")]
        StellantriebCommandLengthZero,

        /// <summary>
        /// The value for the given key cannot be written.
        /// </summary>
        [Description("The value for the given key cannot be written.")]
        StellantriebValueReadOnly,
    }

    /// <summary>
    /// Extension class for ErrorCode enumeration.
    /// </summary>
#if __DOXYGEN__
    public class ErrorCodeExt
#else
    public static class ErrorCodeExt
#endif
    {
        /// <summary>
        /// Bool conversion for ErrorCode enumeration.
        /// </summary>
        /// <param name="error">Implicit extension method parameter.</param>
        /// <returns>True if error equals ErrorCode.Success, false otherwise.</returns>
        public static bool Success(this ErrorCode error)
        {
            return error == ErrorCode.Success;
        }
    }
}
