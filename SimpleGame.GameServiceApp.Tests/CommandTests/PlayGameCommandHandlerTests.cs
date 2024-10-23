using Moq;
using SimpleGame.GameServiceApp.Core.Application.Commands.PlayGame;
using SimpleGame.GameServiceApp.Core.Domain.Enums;
using SimpleGame.GameServiceApp.Core.Domain.Interfaces;
using SimpleGame.GameServiceApp.Core.Domain.Models;

namespace SimpleGame.GameServiceApp.Tests.CommandTests
{
    public class PlayGameCommandHandlerTests
    {
        private readonly Mock<IGameLogicService> _gameServiceMock;
        private readonly Mock<IRandomNumberService> _randomNumberServiceMock;
        private readonly PlayGameCommandHandler _handler;

        public PlayGameCommandHandlerTests()
        {
            // Mock the dependencies
            _gameServiceMock = new Mock<IGameLogicService>();
            _randomNumberServiceMock = new Mock<IRandomNumberService>();

            // Initialize the handler with the mocked services
            _handler = new PlayGameCommandHandler(_gameServiceMock.Object, _randomNumberServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_GameResultDto()
        {
            // Arrange
            var playerChoice = GameChoiceEnum.Rock;
            var computerChoice = GameChoiceEnum.Spock;

            // Setup mock for RandomNumberService to return Spock as the computer choice
            _randomNumberServiceMock
                .Setup(r => r.GetRandomNumber())
                .ReturnsAsync((int)computerChoice);  // Mocked computer choice

            // Setup mock for GameService to return a specific GameResult based on the player and computer choices
            _gameServiceMock
                .Setup(g => g.Play(playerChoice, computerChoice))
                .Returns(new GameResult(playerChoice, computerChoice, GameResultEnum.Lose));

            // Create the PlayGameCommand with a valid PlayerChoice
            var command = new PlayGameCommand { PlayerChoice = playerChoice };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal(playerChoice.ToString(), result.PlayerChoice);       // Expected: Rock
            Assert.Equal(computerChoice.ToString(), result.ComputerChoice);   // Expected: Spock
            Assert.Equal(GameResultEnum.Lose.ToString(), result.Result);      // Expected: Lose
        }
    }
}