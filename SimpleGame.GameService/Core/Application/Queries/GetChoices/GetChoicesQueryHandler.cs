using MediatR;
using SimpleGame.GameService.Core.Application.Dtos;
using SimpleGame.GameService.Core.Domain.Enums;

namespace SimpleGame.GameService.Core.Application.Queries.GetChoices
{
    public class GetChoicesQueryHandler : IRequestHandler<GetChoicesQuery, List<GameChoiceDto>>
    {
        private readonly ILogger<GetChoicesQueryHandler> _logger;

        public GetChoicesQueryHandler(ILogger<GetChoicesQueryHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<List<GameChoiceDto>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetChoicesQuery to fetch all game choices.");

            try
            {
                // Fetch all the choices from the enum
                var choices = Enum.GetValues(typeof(GameChoiceEnum))
                    .Cast<GameChoiceEnum>()
                    .Select(choice => new GameChoiceDto { Id = (int)choice, Name = choice.ToString() })
                    .ToList();

                _logger.LogInformation("Successfully fetched {Count} game choices.", choices.Count);

                return Task.FromResult(choices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching game choices.");
                throw new Exception("Failed to fetch game choices.", ex);
            }
        }
    }
}
