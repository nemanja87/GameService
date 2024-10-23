using Moq;
using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Application.Commands.PlayGame;
using SimpleGame.GameService.Core.Application.Dtos;
using SimpleGame.GameService.Core.Domain.Enums;
using SimpleGame.GameService.Core.Domain.Interfaces;
using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Tests.CommandTests
{
    public class PlayGameCommandHandlerTests
    {
        private readonly Mock<IGameLogicService> _gameServiceMock;
        private readonly Mock<IComputerServiceClient> _computerServiceClientMock;
        private readonly Mock<IScoreboardServiceClient> _scoreboardServiceClientMock;
        private readonly PlayGameCommandHandler _handler;

        public PlayGameCommandHandlerTests()
        {
            // Mock the dependencies
            _gameServiceMock = new Mock<IGameLogicService>();
            _computerServiceClientMock = new Mock<IComputerServiceClient>();
            _scoreboardServiceClientMock = new Mock<IScoreboardServiceClient>();

            // Initialize the handler with the mocked services
            _handler = new PlayGameCommandHandler(
                _gameServiceMock.Object,
                _computerServiceClientMock.Object,
                _scoreboardServiceClientMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_Return_GameResultDto()
        {
            // Arrange
            var playerChoice = GameChoiceEnum.Rock;
            var computerChoice = GameChoiceEnum.Spock;
            var expectedTimestamp = DateTime.UtcNow;

            // Mock the computer service to return "Spock" as the computer's choice
            _computerServiceClientMock
                .Setup(c => c.GetComputerDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(new ComputerDetails { Id = (int)computerChoice, Name = computerChoice.ToString() });

            // Setup mock for GameService to return a specific GameResult based on the player and computer choices
            _gameServiceMock
                .Setup(g => g.Play(playerChoice, computerChoice))
                .Returns(new GameResult(playerChoice, computerChoice, GameResultEnum.Lose));

            // Setup mock for ScoreboardServiceClient to accept the result
            _scoreboardServiceClientMock
                .Setup(s => s.SaveGameResult(It.IsAny<GameResultDto>()))
                .Returns(Task.CompletedTask);  // Mock the save method to do nothing

            // Create the PlayGameCommand with a valid PlayerChoice
            var command = new PlayGameCommand { PlayerChoice = playerChoice };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal(playerChoice.ToString(), result.PlayerChoice);       // Expected: Rock
            Assert.Equal(computerChoice.ToString(), result.ComputerChoice);   // Expected: Spock
            Assert.Equal(GameResultEnum.Lose.ToString(), result.Result);      // Expected: Lose

            // Assert Timestamp is close to the expected time (with some allowance for test execution time)
            Assert.True((result.Timestamp - expectedTimestamp).TotalSeconds < 1, "Timestamp is not within the expected range");

            // Verify that SaveGameResult was called with the correct data
            _scoreboardServiceClientMock.Verify(s => s.SaveGameResult(It.Is<GameResultDto>(r =>
                r.PlayerChoice == result.PlayerChoice &&
                r.ComputerChoice == result.ComputerChoice &&
                r.Result == result.Result &&
                (r.Timestamp - expectedTimestamp).TotalSeconds < 1
            )), Times.Once);
        }
    }
}