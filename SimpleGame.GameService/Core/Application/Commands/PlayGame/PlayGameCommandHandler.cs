using MediatR;
using Polly.Retry;
using Polly;
using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Application.Dtos;
using SimpleGame.GameService.Core.Domain.Enums;
using SimpleGame.GameService.Core.Domain.Interfaces;

namespace SimpleGame.GameService.Core.Application.Commands.PlayGame
{
    public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, GameResultDto>
    {
        private readonly IGameLogicService _gameService;
        private readonly IComputerServiceClient _computerServiceClient;
        private readonly IScoreboardServiceClient _scoreboardServiceClient;
        private readonly ILogger<PlayGameCommandHandler> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        public PlayGameCommandHandler(
            IGameLogicService gameService,
            IComputerServiceClient computerServiceClient,
            IScoreboardServiceClient scoreboardServiceClient,
            ILogger<PlayGameCommandHandler> logger)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _computerServiceClient = computerServiceClient ?? throw new ArgumentNullException(nameof(computerServiceClient));
            _scoreboardServiceClient = scoreboardServiceClient ?? throw new ArgumentNullException(nameof(scoreboardServiceClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Setup retry policy for resilient external calls
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt =>
                {
                    var delay = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    _logger.LogWarning("Retrying in {Delay} seconds due to external service failure (Attempt: {RetryAttempt})", delay, retryAttempt);
                    return delay;
                });
        }

        public async Task<GameResultDto> Handle(PlayGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling PlayGameCommand for player choice: {PlayerChoice}", request.PlayerChoice);

            try
            {
                // Fetch computer's choice by calling ComputerService using Polly retry policy
                _logger.LogInformation("Fetching computer choice from ComputerService...");
                var computerDetails = await _retryPolicy.ExecuteAsync(() =>
                    _computerServiceClient.GetComputerDetailsAsync(0));

                _logger.LogInformation("Fetched computer choice: {ComputerChoice}", computerDetails.Name);

                // Map the computer's choice and play the game
                var computerChoice = MapToGameChoiceEnum(computerDetails.Name);
                var gameResult = _gameService.Play(request.PlayerChoice, computerChoice);

                // Create GameResultDto
                var resultDto = new GameResultDto
                {
                    PlayerChoice = request.PlayerChoice.ToString(),
                    ComputerChoice = computerChoice.ToString(),
                    Result = gameResult.Result.ToString(),
                    Timestamp = DateTime.UtcNow
                };

                _logger.LogInformation("Game played successfully. Player: {PlayerChoice}, Computer: {ComputerChoice}, Result: {Result}",
                    resultDto.PlayerChoice, resultDto.ComputerChoice, resultDto.Result);

                // Save the game result to the ScoreboardService using Polly retry policy
                _logger.LogInformation("Saving game result to ScoreboardService...");
                await _retryPolicy.ExecuteAsync(() => _scoreboardServiceClient.SaveGameResult(resultDto));

                _logger.LogInformation("Game result saved successfully.");

                return resultDto;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to communicate with external services.");
                throw new Exception("Failed to communicate with external services.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling PlayGameCommand.");
                throw;
            }
        }

        private GameChoiceEnum MapToGameChoiceEnum(string computerChoiceName)
        {
            _logger.LogInformation("Mapping computer choice '{ComputerChoiceName}' to GameChoiceEnum.", computerChoiceName);

            if (Enum.TryParse(computerChoiceName, out GameChoiceEnum choice))
            {
                _logger.LogInformation("Successfully mapped computer choice '{ComputerChoiceName}' to {GameChoiceEnum}", computerChoiceName, choice);
                return choice;
            }

            _logger.LogWarning("Invalid computer choice received from ComputerService: {ComputerChoiceName}", computerChoiceName);
            throw new ArgumentException("Invalid computer choice received from ComputerService.");
        }
    }
}
