﻿namespace TURAG.Feldbus.Types
{
    public class BusTransceiveResult
    {
        public BusTransceiveResult(bool success, BusResponse response)
        {
            this.success = success;
            this.response = response;
        }

        public bool Success => success;
        public BusResponse Response => response;

        readonly bool success;
        readonly BusResponse response;
    }
}
