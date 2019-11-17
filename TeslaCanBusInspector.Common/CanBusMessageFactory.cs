using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TeslaCanBusInspector.Common.Messages;

namespace TeslaCanBusInspector.Common
{
    public class CanBusMessageFactory : ICanBusMessageFactory
    {
        private static readonly ConstructorInfo[] MessageConstructorsModelS = new ConstructorInfo[ushort.MaxValue];
        private static readonly ConstructorInfo[] MessageConstructorsModelX = new ConstructorInfo[ushort.MaxValue];
        private static readonly ConstructorInfo[] MessageConstructorsModel3 = new ConstructorInfo[ushort.MaxValue];

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

                if (model.CarType.HasFlag(CarType.ModelS))
                {
                    AssignMessageType(MessageConstructorsModelS, messageType, model);
                }

                if (model.CarType.HasFlag(CarType.ModelX))
                {
                    AssignMessageType(MessageConstructorsModelX, messageType, model);
                }

                if (model.CarType.HasFlag(CarType.Model3))
                {
                    AssignMessageType(MessageConstructorsModel3, messageType, model);
                }
            }
        }

        private static void AssignMessageType(ConstructorInfo[] constructors, Type messageType, ICanBusMessage model)
        {
            var existingConstructor = constructors[model.MessageTypeId];
            if (existingConstructor != null)
            {
                throw new InvalidOperationException($"Colliding message type IDs: {model.MessageTypeId:X}");
            }

            constructors[model.MessageTypeId] = GetPayloadConstructor(messageType);
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

        public ICanBusMessage Create(CarType carType, ushort messageTypeId, byte[] payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var array = GetConstructorInfos(carType);
            var constructor = array[messageTypeId] ?? array[UnknownMessage.TypeId];

            return (ICanBusMessage)constructor.Invoke(new object[] { payload });
        }

        private static ConstructorInfo[] GetConstructorInfos(CarType carType)
        {
            switch (carType)
            {
                case CarType.ModelS:
                    return MessageConstructorsModelS;
                case CarType.ModelX:
                    return MessageConstructorsModelX;
                case CarType.Model3:
                    return MessageConstructorsModel3;
            }

            throw new InvalidOperationException("Invalid car type: " + carType);
        }
    }

    public interface ICanBusMessageFactory
    {
        ICanBusMessage Create(CarType carType, ushort messageTypeId, byte[] payload);
    }
}
