﻿namespace TeslaCanBusInspector.Models
{
    // ReSharper disable UnusedMember.Global
    public class PackInfoMessage : ICanBusMessage
    {
        public const ushort TypeId = 0x222;
        public ushort MessageTypeId => TypeId;

        internal PackInfoMessage()
        {
        }

        public PackInfoMessage(byte[] payload)
        {
            // TODO: add code here
        }
    }
}