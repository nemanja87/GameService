using Microsoft.Extensions.Logging;
using Moq;
using SimpleGame.ComputerService.Core.Application.Services;
using SimpleGame.ComputerService.Core.Domain.Enum;

namespace SimpleGame.ComputerService.Tests.QueryTests
{
    public class ComputerChoiceServiceTests
    {
        private readonly ComputerChoiceService _computerChoiceService;
        private readonly Mock<ILogger<ComputerChoiceService>> _loggerMock;

        public ComputerChoiceServiceTests()
        {
            // Mock the logger
            _loggerMock = new Mock<ILogger<ComputerChoiceService>>();

            // Inject the mock logger into the service
            _computerChoiceService = new ComputerChoiceService(_loggerMock.Object);
        }

        [Fact]
        public async Task GetRandomComputerChoiceAsync_ReturnsValidChoice()
        {
            // Act
            var result = await _computerChoiceService.GetRandomComputerChoiceAsync();

            // Assert
            Assert.True(result >= ComputerChoiceEnum.Rock && result <= ComputerChoiceEnum.Spock);
        }

        [Fact]
        public async Task GetRandomComputerChoiceAsync_ReturnsValuesWithinEnumRange()
        {
            // Act & Assert
            for (int i = 0; i < 100; i++)  // Perform the test multiple times to ensure all values are in range
            {
                var result = await _computerChoiceService.GetRandomComputerChoiceAsync();

                // Ensure that the random choice is a valid value in the ComputerChoiceEnum range
                Assert.True(Enum.IsDefined(typeof(ComputerChoiceEnum), result));
            }
        }
    }
}
