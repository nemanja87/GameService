using Moq;
using SimpleGame.ScoreboardService.Core.Application.Dtos;
using SimpleGame.ScoreboardService.Core.Application.Queries.GetLastResults;
using SimpleGame.ScoreboardService.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardService.Tests.QueryTests
{
    public class GetLastResultsQueryHandlerTests
    {
        private readonly Mock<IScoreboardService> _scoreboardServiceMock;
        private readonly GetLastResultsQueryHandler _handler;

        public GetLastResultsQueryHandlerTests()
        {
            // Mock the IScoreboardService dependency
            _scoreboardServiceMock = new Mock<IScoreboardService>();

            // Initialize the query handler with the mocked service
            _handler = new GetLastResultsQueryHandler(_scoreboardServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Last_Results_From_ScoreboardService()
        {
            // Arrange
            var expectedResults = new List<GameResultDto>
            {
                new GameResultDto { PlayerChoice = "Rock", ComputerChoice = "Scissors", Result = "Win" },
                new GameResultDto { PlayerChoice = "Paper", ComputerChoice = "Rock", Result = "Win" }
            };

            _scoreboardServiceMock
                .Setup(s => s.GetLastResults())
                .Returns(expectedResults);

            var query = new GetLastResultsQuery();

            // Act
            var results = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResults.Count, results.Count());
            Assert.Equal(expectedResults, results);
        }
    }
}
