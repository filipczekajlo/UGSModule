using FluentAssertions;
using Xunit;
using NetworkUtility.Ping;

namespace NetworkUtility.Test.PingTests
{
    public class NetworkServiceTests
    {
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            // Arrange
            var pingService = new NetworkService();
            
            // Act
            var result = pingService.SenPing();

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("lol");
        }

        [Theory] // Theory is like fact but lets you input data.
        [InlineData(1, 2, 3)]
        [InlineData(3, 3, 6)]
        public void NetworkService_PingTimeout_Test(int a, int b, int expected)
        {
            // Arrange
            var pingservice = new NetworkService();
            
            // Act
            var result = pingservice.PingTimeout(a, b);
            
            // Assert
            result.Should().Be(expected);

        }
        
    }
}