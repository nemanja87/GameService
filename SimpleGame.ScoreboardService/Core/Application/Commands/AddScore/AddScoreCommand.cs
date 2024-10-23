using MediatR;
using SimpleGame.ScoreboardService.Core.Application.Dtos;

namespace SimpleGame.ScoreboardService.Core.Application.Commands.AddScore;

public class AddScoreCommand : IRequest<Unit>
{
    public GameResultDto GameResult { get; }

    // Constructor for initializing the command with a valid GameResultDto
    public AddScoreCommand(GameResultDto gameResult)
    {
        // Ensure that GameResult is not null
        GameResult = gameResult ?? throw new ArgumentNullException(nameof(gameResult));
    }
}
