using System;
using System.IO;
using Newtonsoft.Json;
using TeslaCanBusInspector.Common;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector
{
    public class CanBusLogFileToJson
    {
        public static void ReadFileToJson(CarType carType, string fileName)
        {
            var parser = new CanBusLogLineParser();
            var messageFactory = new CanBusMessageFactory();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Converters = { new FormatValueTypesConverter() },
                Formatting = Formatting.None
            };

            using (var reader = File.OpenText(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;

                    var parsedLine = parser.TryParseLine(line);
                    if (parsedLine == null)
                    {
                        continue;
                    }

                    var message = messageFactory.Create(carType, parsedLine.MessageTypeId, parsedLine.Payload);
                    if (message is UnknownMessage) continue;

                    var json = JsonConvert.SerializeObject(message, jsonSerializerSettings);
                    Console.WriteLine(json);
                }
            }
        }

        private sealed class FormatValueTypesConverter : JsonConverter
        {
            public override bool CanRead => false;
            public override bool CanWrite => true;

            public override bool CanConvert(Type type)
            {
                return type.Namespace == typeof(Ampere).Namespace;
            }

            public override void WriteJson(
                JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
            }

            public override object ReadJson(
                JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
            {
                throw new NotSupportedException();
            }
        }
    }
}

