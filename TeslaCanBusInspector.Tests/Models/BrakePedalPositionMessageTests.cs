using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
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