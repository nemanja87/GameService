using SimpleGame.GameServiceApp.Core.Application.Dtos;
using SimpleGame.GameServiceApp.Core.Application.Queries.GetChoices;
using SimpleGame.GameServiceApp.Core.Domain.Enums;

namespace SimpleGame.GameServiceApp.Tests.QueryTests
{
    public class GetChoicesQueryHandlerTests
    {
        private readonly GetChoicesQueryHandler _handler;

        public GetChoicesQueryHandlerTests()
        {
            _handler = new GetChoicesQueryHandler();
        }

        [Fact]
        public async Task Handle_Should_Return_All_GameChoices()
        {
            // Arrange: Ensure that GameChoiceEnum is an actual enum and passed correctly
            var expectedChoices = Enum.GetValues(typeof(GameChoiceEnum))
                                      .Cast<GameChoiceEnum>()
                                      .Select(c => new GameChoiceDto { Id = (int)c, Name = c.ToString() })
                                      .ToList();

            // Act
            var result = await _handler.Handle(new GetChoicesQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(expectedChoices.Count, result.Count);
            for (int i = 0; i < expectedChoices.Count; i++)
            {
                Assert.Equal(expectedChoices[i].Id, result[i].Id);
                Assert.Equal(expectedChoices[i].Name, result[i].Name);
            }
        }
    }
}
