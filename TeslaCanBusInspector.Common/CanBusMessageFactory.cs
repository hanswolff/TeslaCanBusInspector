using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TeslaCanBusInspector.Common.Messages;

namespace TeslaCanBusInspector.Common
{
    public class CanBusMessageFactory : ICanBusMessageFactory
    {
        private static readonly ConstructorInfo[] MessageConstructors = new ConstructorInfo[ushort.MaxValue];

        static CanBusMessageFactory()
        {
            var messageTypes = GetMessageTypesFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var messageType in messageTypes)
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

        private static IEnumerable<Type> GetMessageTypesFromAssembly(Assembly assembly)
        {
            return assembly.GetTypes().Where(type =>
                !type.IsAbstract &&
                typeof(ICanBusMessage).IsAssignableFrom(type) &&
                GetTypeConstructor(type) != null).Distinct();
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

            var constructor = MessageConstructors[messageTypeId] ?? MessageConstructors[UnknownMessage.TypeId];

            return (ICanBusMessage)constructor.Invoke(new object[] { payload });
        }
    }

    public interface ICanBusMessageFactory
    {
        ICanBusMessage Create(ushort messageTypeId, byte[] payload);
    }
}
