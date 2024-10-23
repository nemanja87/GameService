using MediatR;
using SimpleGame.GameServiceApp.Core.Application.Dtos;
using SimpleGame.GameServiceApp.Core.Domain.Enums;
using SimpleGame.GameServiceApp.Core.Domain.Interfaces;

namespace SimpleGame.GameServiceApp.Core.Application.Commands.PlayGame
{
    public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, GameResultDto>
    {
        private readonly IGameLogicService _gameService;
        private readonly IRandomNumberService _randomNumberService;

        // Constructor for dependency injection
        public PlayGameCommandHandler(IGameLogicService gameService, IRandomNumberService randomNumberService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _randomNumberService = randomNumberService ?? throw new ArgumentNullException(nameof(randomNumberService));
        }

        public async Task<GameResultDto> Handle(PlayGameCommand request, CancellationToken cancellationToken)
        {
            // Fetch computer's random choice
            var computerChoice = (GameChoiceEnum)(await _randomNumberService.GetRandomNumber());

            // Play the game and get the result
            var gameResult = _gameService.Play(request.PlayerChoice, computerChoice);

            // Map to DTO
            var resultDto = new GameResultDto
            {
                PlayerChoice = request.PlayerChoice.ToString(),
                ComputerChoice = computerChoice.ToString(),
                Result = gameResult.Result.ToString()
            };

            return resultDto;
        }
    }
}
