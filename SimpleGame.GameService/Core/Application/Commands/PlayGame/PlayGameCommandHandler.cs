using MediatR;
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

        // Constructor for dependency injection
        public PlayGameCommandHandler(
            IGameLogicService gameService,
            IComputerServiceClient computerServiceClient,
            IScoreboardServiceClient scoreboardServiceClient)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _computerServiceClient = computerServiceClient ?? throw new ArgumentNullException(nameof(computerServiceClient));
            _scoreboardServiceClient = scoreboardServiceClient ?? throw new ArgumentNullException(nameof(scoreboardServiceClient));
        }

        public async Task<GameResultDto> Handle(PlayGameCommand request, CancellationToken cancellationToken)
        {
            // Fetch computer's choice by calling ComputerService
            var computerDetails = await _computerServiceClient.GetComputerDetailsAsync(0);
            GameChoiceEnum computerChoice = MapToGameChoiceEnum(computerDetails.Name);

            // Play the game and get the result
            var gameResult = _gameService.Play(request.PlayerChoice, computerChoice);

            // Map to DTO
            var resultDto = new GameResultDto
            {
                PlayerChoice = request.PlayerChoice.ToString(),
                ComputerChoice = computerChoice.ToString(),
                Result = gameResult.Result.ToString(),
                Timestamp = DateTime.UtcNow
            };

            // Save the game result to the ScoreboardService
            await _scoreboardServiceClient.SaveGameResult(resultDto);

            return resultDto;
        }

        private GameChoiceEnum MapToGameChoiceEnum(string computerChoiceName)
        {
            // Map the string value from ComputerService to the GameChoiceEnum
            return Enum.TryParse(computerChoiceName, out GameChoiceEnum choice)
                ? choice
                : throw new ArgumentException("Invalid computer choice received from ComputerService.");
        }
    }
}
