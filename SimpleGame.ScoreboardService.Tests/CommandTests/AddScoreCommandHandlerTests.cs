using Moq;
using SimpleGame.ScoreboardService.Core.Application.Commands.AddScore;
using SimpleGame.ScoreboardService.Core.Application.Dtos;
using SimpleGame.ScoreboardService.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardService.Tests.CommandTests
{
    public class AddScoreCommandHandlerTests
    {
        private readonly Mock<IScoreboardService> _scoreboardServiceMock;
        private readonly AddScoreCommandHandler _handler;

        public AddScoreCommandHandlerTests()
        {
            // Mock the IScoreboardService dependency
            _scoreboardServiceMock = new Mock<IScoreboardService>();

            // Initialize the command handler with the mocked service
            _handler = new AddScoreCommandHandler(_scoreboardServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Call_AddResult_On_ScoreboardService()
        {
            // Arrange
            var gameResult = new GameResultDto { PlayerChoice = "Rock", ComputerChoice = "Scissors", Result = "Win" };
            var command = new AddScoreCommand(gameResult);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _scoreboardServiceMock.Verify(s => s.AddResult(It.Is<GameResultDto>(r =>
                r.PlayerChoice == "Rock" &&
                r.ComputerChoice == "Scissors" &&
                r.Result == "Win"
            )), Times.Once);
        }
    }
}
