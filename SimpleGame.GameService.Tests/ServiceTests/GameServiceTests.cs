using Microsoft.Extensions.Logging;
using Moq;
using SimpleGame.GameService.Core.Application.Services;
using SimpleGame.GameService.Core.Domain.Enums;

namespace SimpleGame.GameService.Tests.ServiceTests
{
    public class GameServiceTests
    {
        private readonly GameLogicService _gameService;
        private readonly Mock<ILogger<GameLogicService>> _loggerMock;

        public GameServiceTests()
        {
            _loggerMock = new Mock<ILogger<GameLogicService>>();
            _gameService = new GameLogicService(_loggerMock.Object);
        }

        [Theory]
        [InlineData(GameChoiceEnum.Rock, GameChoiceEnum.Scissors, GameResultEnum.Win)]
        [InlineData(GameChoiceEnum.Rock, GameChoiceEnum.Paper, GameResultEnum.Lose)]
        [InlineData(GameChoiceEnum.Rock, GameChoiceEnum.Rock, GameResultEnum.Tie)]
        [InlineData(GameChoiceEnum.Paper, GameChoiceEnum.Rock, GameResultEnum.Win)]
        [InlineData(GameChoiceEnum.Scissors, GameChoiceEnum.Lizard, GameResultEnum.Win)]
        public void PlayGame_Should_Return_Correct_Result(GameChoiceEnum playerChoice, GameChoiceEnum computerChoice, GameResultEnum expectedResult)
        {
            // Act
            var result = _gameService.Play(playerChoice, computerChoice);

            // Assert
            Assert.Equal(expectedResult, result.Result);

            // Verify that the correct log messages were written
            _loggerMock.Verify(
                x => x.Log<It.IsAnyType>(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Starting game logic. PlayerChoice: {playerChoice}, ComputerChoice: {computerChoice}")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            // Special case for Tie: the log message format is different
            if (expectedResult == GameResultEnum.Tie)
            {
                _loggerMock.Verify(
                    x => x.Log<It.IsAnyType>(
                        LogLevel.Information,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Game result: Tie. Both player and computer chose {playerChoice}")),
                        null,
                        It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                    Times.Once);
            }
            else
            {
                _loggerMock.Verify(
                    x => x.Log<It.IsAnyType>(
                        LogLevel.Information,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Game result: {expectedResult}. PlayerChoice: {playerChoice}, ComputerChoice: {computerChoice}")),
                        null,
                        It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                    Times.Once);
            }
        }
    }
}
