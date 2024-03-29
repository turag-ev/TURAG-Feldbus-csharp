﻿using System;
using System.Threading.Tasks;

namespace TURAG.Feldbus.Transport
{
    /// <summary>
    /// This transport implementation defers the actual data exchange by providing
    /// three pairs of request event and SetResult-function for transmit, transceive
    /// buffer clear. The user needs to register to each event and, within its invocation,
    /// execute the required transport mechanism and call the SetResult function to
    /// supply the result of the operation.
    /// 
    /// This class is suitable to be used for integrating %TURAG %Feldbus device classes
    /// with LabView.
    /// 
    /// Async transport functions are currently not implemented. Consequently,
    /// only synchronous function variants of the device classes can be used.
    /// </summary>
    public class EventTransport : TransportAbstraction
    {
        /// <summary>
        /// Delegate for transmit operations.
        /// </summary>
        /// <param name="transmitData">Data to transmit.</param>
        public delegate void TransmitRequestHandler(byte[] transmitData);

        /// <summary>
        /// Delegate for transceive operations.
        /// </summary>
        /// <param name="data">Data to transmit.</param>
        /// <param name="bytesRequested">Number of bytes to receive.</param>
        public delegate void TransceiveRequesthandler(byte[] data, int bytesRequested);

        /// <summary>
        /// Delegate for clear buffer operations.
        /// </summary>
        public delegate void ClearBufferRequestHandler();

        /// <summary>
        /// Event which is raised when the device class requires
        /// data transmission without reception. In the context of
        /// the function which handles this event, SetTransmitResult() 
        /// has to be called.
        /// </summary>
        public event TransmitRequestHandler TransmitRequested;

        /// <summary>
        /// Event which is raised when the device class requires
        /// data transmission and reception. In the context of
        /// the function which handles this event, SetTransceiveResult() 
        /// has to be called.
        /// </summary>
        public event TransceiveRequesthandler TransceiveRequested;

        /// <summary>
        /// Event which is raised when the device class requires
        /// clearing the input buffer. In the context of
        /// the function which handles this event, SetClearBufferResult() 
        /// has to be called.
        /// </summary>
        public event ClearBufferRequestHandler ClearBufferRequested;

        /// <summary>
        /// Sets the result of a data transmission. This function has to be called
        /// in the context of the event handler processing a TransmitRequested event,
        /// after the data was actually transmitted on the transport channel.
        /// </summary>
        /// <param name="transmitSuccessful">True, if all data was successfully 
        /// transmitted, false otherwise.</param>
        public void SetTransmitResult(bool transmitSuccessful)
        {
            transmitResult = transmitSuccessful;
        }

        /// <summary>
        /// Sets the result of a data transceive operation. This function has to be called
        /// in the context of the event handler processing a TransceiveRequested event,
        /// after the data was actually transmitted and received on the transport channel.
        /// </summary>
        /// <param name="transceiveSuccessful">True, if all data was successfully 
        /// transmitted and the requested amount of data was successfully received,
        /// false otherwise.</param>
        /// <param name="receivedData">Array containing the received data.</param>
        public void SetTransceiveResult(bool transceiveSuccessful, byte[] receivedData)
        {
            transceiveResult = (transceiveSuccessful, receivedData);
        }

        /// <summary>
        /// Sets the result of a clear buffer operation. This function has to be called
        /// in the context of the event handler processing a ClearBufferRequested event,
        /// after the data in the input buffer was actually cleared.
        /// </summary>
        /// <param name="clearBufferSuccessful">True, if the input buffer was 
        /// successfully cleared, false otherwise.</param>
        public void SetClearBufferResult(bool clearBufferSuccessful)
        {
            clearBufferResult = clearBufferSuccessful;
        }


        /// <summary>
        /// Transmits to and afterwards receives data from the transport channel.
        /// This implementation causes the TransceiveRequested event to be fired and returns
        /// the data which was set with the SetTransceiveResult() function.
        /// </summary>
        /// <param name="data">Raw data frame to transmit (including address and checksum).</param>
        /// <param name="bytesRequested">Number of raw bytes to receive (including address and checksum).</param>
        /// <returns>True if transmission was successful and the requested number
        /// of bytes were received, false otherwise and the received data.</returns>
#if __DOXYGEN__
        protected override ValueTuple<bool, byte[]> DoTransceive(byte[] data, int bytesRequested)
#else
        protected override (bool success, byte[] receivedData) DoTransceive(byte[] data, int bytesRequested)
#endif

        {
            // reset the exchange variable
            transceiveResult = (false, new byte[0]);

            // invoke the event, which should call 
            // SetTransceiveResult(), updating transceiveResult
            TransceiveRequested?.Invoke(data, bytesRequested);

            // return the correct result
            return transceiveResult;
        }

        /// <summary>
        /// Async transceive. Not supported for this transport. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="bytesRequested"></param>
        /// <returns></returns>
        protected override Task<(bool success, byte[] receivedData)> DoTransceiveAsync(byte[] data, int bytesRequested)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Transmits the given data on the transport channel.
        /// This implementation causes the TransmitRequested event to be fired and returns
        /// the data which was set with the SetTransmitResult() function.
        /// </summary>
        /// <param name="data">Raw data frame to transmit (including address and checksum).</param>
        /// <returns>True if transmission was successful, false otherwise.</returns>
        protected override bool DoTransmit(byte[] data)
        {
            // reset the exchange variable
            transmitResult = false;

            // invoke the event, which should call 
            // SetTransmitResult(), updating transmitResult
            TransmitRequested?.Invoke(data);

            // return the correct result
            return transmitResult;
        }

        /// <summary>
        /// Async transmit. Not supported for this transport. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override Task<bool> DoTransmitAsync(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the input buffer of the transport channel.
        /// This implementation causes the ClearBufferRequested event to be fired and returns
        /// the result which was set with the SetClearBufferResult() function.
        /// </summary>
        /// <returns>True if the buffer was successfully cleared, false otherwise.</returns>
        protected override bool DoClearBuffer()
        {
            // reset the exchange variable
            clearBufferResult = false;

            // invoke the event, which should call 
            // SetClearBufferResult(), updating clearBufferResult
            ClearBufferRequested?.Invoke();

            // return the correct result
            return clearBufferResult;
        }

        /// <summary>
        /// Async input buffer clear. Not supported for this transport.
        /// </summary>
        /// <returns></returns>
        protected override Task<bool> DoClearBufferAsync()
        {
            throw new NotImplementedException();
        }

        private (bool, byte[]) transceiveResult = (false, new byte[0]);
        private bool transmitResult = false;
        private bool clearBufferResult = false;
    }
}
