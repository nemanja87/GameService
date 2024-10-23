using Moq;
using SimpleGame.ComputerServiceApp.Core.Application.Queries;
using SimpleGame.ComputerServiceApp.Core.Application.Queries.AddComputerChoice;
using SimpleGame.ComputerServiceApp.Core.Domain.Enum;
using SimpleGame.ComputerServiceApp.Core.Domain.Interfaces;

namespace SimpleGame.ComputerServiceApp.Tests.QueryTests
{
    public class GetComputerChoiceCommandHandlerTests
    {
        private readonly Mock<IComputerChoiceService> _computerChoiceServiceMock;
        private readonly GetComputerChoiceCommandHandler _handler;

        public GetComputerChoiceCommandHandlerTests()
        {
            _computerChoiceServiceMock = new Mock<IComputerChoiceService>();
            _handler = new GetComputerChoiceCommandHandler(_computerChoiceServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Returns_Valid_ComputerChoiceDto()
        {
            // Arrange
            var expectedChoice = ComputerChoiceEnum.Rock; // Assume enum value for this example
            _computerChoiceServiceMock.Setup(service => service.GetRandomComputerChoiceAsync())
                                      .ReturnsAsync(expectedChoice);

            var command = new GetComputerChoiceCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expectedChoice, result.Id);
            Assert.Equal(expectedChoice.ToString(), result.Name);
        }
    }
}
