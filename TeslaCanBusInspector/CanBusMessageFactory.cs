using System;
using System.Linq;
using System.Reflection;
using TeslaCanBusInspector.Models;

namespace TeslaCanBusInspector
{
    public class CanBusMessageFactory : ICanBusMessageFactory
    {
        private static readonly ConstructorInfo[] MessageConstructors = new ConstructorInfo[ushort.MaxValue];

        static CanBusMessageFactory()
        {
            var messageTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                typeof(ICanBusMessage).IsAssignableFrom(type) && GetTypeConstructor(type) != null);

            foreach (var messageType in messageTypes.Distinct())
            {
                var typeConstructor = GetTypeConstructor(messageType);
                var payloadConstructor = GetPayloadConstructor(messageType);
                if (payloadConstructor == null)
                {
                    throw new InvalidOperationException($"Constructor of {messageType.Name} needs an overload with payload byte array");
                }

                var model = (ICanBusMessage)typeConstructor.Invoke(null);

                var existingConstructor = MessageConstructors[model.MessageTypeId];
                if (existingConstructor != null)
                {
                    throw new InvalidOperationException($"Colliding message type IDs: {model.MessageTypeId:X}");
                }

                MessageConstructors[model.MessageTypeId] = GetPayloadConstructor(messageType);
            }
        }

        private static ConstructorInfo GetTypeConstructor(Type type)
        {
            return type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(ci => ci.GetParameters().Length == 0);
        }

        private static ConstructorInfo GetPayloadConstructor(Type type)
        {
            return type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(ci => ci.GetParameters().Length == 1 && ci.GetParameters()[0].ParameterType == typeof(byte[]));
        }

        public ICanBusMessage Create(ushort messageTypeId, byte[] payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var constructor = MessageConstructors[messageTypeId];
            if (constructor == null)
            {
                return (ICanBusMessage)MessageConstructors[UnknownMessage.TypeId].Invoke(new [] { payload });
            }

            return (ICanBusMessage)constructor.Invoke(new [] { payload });
        }
    }

    public interface ICanBusMessageFactory
    {
        ICanBusMessage Create(ushort messageTypeId, byte[] payload);
    }
}
