using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TeslaCanBusInspector.Common.Messages;

namespace TeslaCanBusInspector.Common
{
    public class CanBusMessageFactory : ICanBusMessageFactory
    {
        private static readonly MessageInfo[] MessageConstructorsModelS = new MessageInfo[ushort.MaxValue];
        private static readonly MessageInfo[] MessageConstructorsModelX = new MessageInfo[ushort.MaxValue];
        private static readonly MessageInfo[] MessageConstructorsModel3 = new MessageInfo[ushort.MaxValue];

        static CanBusMessageFactory()
        {
            var messageTypes = GetMessageTypesFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var messageType in messageTypes)
            {
                var typeConstructor = GetTypeConstructor(messageType);
                var payloadConstructor = GetPayloadConstructor(messageType);
                if (payloadConstructor == null)
                {
                    throw new InvalidOperationException(
                        $"Constructor of {messageType.Name} needs an overload with payload byte array");
                }

                var model = (ICanBusMessage) typeConstructor.Invoke(null);

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

        private static void AssignMessageType(MessageInfo[] messageInfo, Type messageType, ICanBusMessage model)
        {
            var existing = messageInfo[model.MessageTypeId];
            if (existing != null)
            {
                throw new InvalidOperationException($"Colliding message type IDs: {model.MessageTypeId:X}");
            }

            messageInfo[model.MessageTypeId] = new MessageInfo
            {
                Constructor = GetPayloadConstructor(messageType),
                RequireBytes = model.RequireBytes
            };
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
                .FirstOrDefault(ci =>
                    ci.GetParameters().Length == 1 && ci.GetParameters()[0].ParameterType == typeof(byte[]));
        }

        public ICanBusMessage Create(CarType carType, ushort messageTypeId, byte[] payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var messageInfos = GetMessageInfosForCarType(carType);

            if (payload.Length < messageInfos[messageTypeId]?.RequireBytes)
            {
                return (ICanBusMessage) messageInfos[UnknownMessage.TypeId].Constructor.Invoke(new object[] {payload});
            }

            var constructor = messageInfos[messageTypeId]?.Constructor ??
                              messageInfos[UnknownMessage.TypeId].Constructor;

            return (ICanBusMessage) constructor.Invoke(new object[] {payload});
        }

        private static MessageInfo[] GetMessageInfosForCarType(CarType carType)
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

        private class MessageInfo
        {
            public ConstructorInfo Constructor;
            public byte RequireBytes;
        }
    }

    public interface ICanBusMessageFactory
    {
        ICanBusMessage Create(CarType carType, ushort messageTypeId, byte[] payload);
    }
}