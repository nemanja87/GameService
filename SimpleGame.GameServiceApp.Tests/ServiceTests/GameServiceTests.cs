using SimpleGame.GameService.Core.Application.Services;
using SimpleGame.GameService.Core.Domain.Enums;

namespace SimpleGame.GameService.Tests.ServiceTests
{
    public class GameServiceTests
    {
        private readonly GameLogicService _gameService;

        public GameServiceTests()
        {
            _gameService = new GameLogicService();
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
        }

    }
}
