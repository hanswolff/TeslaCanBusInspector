﻿using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class RearTorqueMessage : IRearTorqueMessage
    {
        public CarType CarType => CarType.Model3;

        public const ushort TypeId = 0x108;
        public ushort MessageTypeId => TypeId;

        public NewtonMeter RearTorqueRequest { get; }
        public NewtonMeter RearTorque { get; }
        public RevolutionsPerMinute RearAxleRpm { get; }

        internal RearTorqueMessage()
        {
        }

        public RearTorqueMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            RearTorqueRequest = new NewtonMeter(BitArrayConverter.ToInt16(payload, 12, 13) * 2m);
            RearTorque = new NewtonMeter(BitArrayConverter.ToInt16(payload, 27, 13) * 2m);
            RearAxleRpm = new RevolutionsPerMinute(BitArrayConverter.ToInt16(payload, 40, 16) * 0.1m);
        }
    }

    public interface IRearTorqueMessage : ICanBusMessage
    {
        NewtonMeter RearTorqueRequest { get; }
        NewtonMeter RearTorque { get; }
        RevolutionsPerMinute RearAxleRpm { get; }
    }
}