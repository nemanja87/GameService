using SimpleGame.ComputerServiceApp.Core.Application.Services;
using SimpleGame.ComputerServiceApp.Core.Domain.Enum;

namespace SimpleGame.ComputerServiceApp.Tests.ServiceTests
{
    public class ComputerChoiceServiceTests
    {
        private readonly ComputerChoiceService _computerChoiceService;

        public ComputerChoiceServiceTests()
        {
            _computerChoiceService = new ComputerChoiceService();  // No need to mock as it doesn't depend on external services
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
