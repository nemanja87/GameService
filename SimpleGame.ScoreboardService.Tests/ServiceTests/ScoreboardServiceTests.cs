using SimpleGame.ScoreboardService.Core.Application.Dtos;

namespace SimpleGame.ScoreboardService.Tests.ServiceTests
{
    public class ScoreboardServiceTests
    {
        private readonly Core.Application.Services.ScoreboardService _scoreboardService;

        public ScoreboardServiceTests()
        {
            _scoreboardService = new Core.Application.Services.ScoreboardService();
        }

        [Fact]
        public void AddResult_Should_Add_Result_To_Queue()
        {
            // Arrange
            var gameResult = new GameResultDto { PlayerChoice = "Rock", ComputerChoice = "Scissors", Result = "Win" };

            // Act
            _scoreboardService.AddResult(gameResult);
            var results = _scoreboardService.GetLastResults();

            // Assert
            Assert.Single(results);
            Assert.Contains(gameResult, results);
        }

        [Fact]
        public void AddResult_Should_Remove_Oldest_If_Queue_Exceeds_Limit()
        {
            // Arrange
            for (int i = 0; i < 10; i++)
            {
                _scoreboardService.AddResult(new GameResultDto { PlayerChoice = "Choice" + i, ComputerChoice = "Computer" + i, Result = "Result" + i });
            }

            var newGameResult = new GameResultDto { PlayerChoice = "Rock", ComputerChoice = "Scissors", Result = "Win" };

            // Act
            _scoreboardService.AddResult(newGameResult);
            var results = _scoreboardService.GetLastResults();

            // Assert
            Assert.Equal(10, results.Count());  // Ensure the queue size stays at 10
            Assert.DoesNotContain(results, r => r.PlayerChoice == "Choice0");  // Oldest element (Choice0) should be removed
            Assert.Contains(newGameResult, results);
        }
    }
}
