using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.ModelS;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.ModelS
{
    public class BrakePedalPositionMessageTests
    {
        [Fact(Skip = "add data here")]
        public void Zero()
        {
            // Act      
            var message = new BrakePedalPositionMessage(new byte[] { 0 } );

            // Assert
            message.BrakePedalPositionPercent.Should().Be(0);
        }
    }
}