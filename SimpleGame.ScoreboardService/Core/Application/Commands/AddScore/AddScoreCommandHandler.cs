using MediatR;
using SimpleGame.ScoreboardService.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardService.Core.Application.Commands.AddScore;

public class AddScoreCommandHandler : IRequestHandler<AddScoreCommand, Unit>
{
    private readonly IScoreboardService _scoreboardService;
    private readonly ILogger<AddScoreCommandHandler> _logger;

    public AddScoreCommandHandler(IScoreboardService scoreboardService, ILogger<AddScoreCommandHandler> logger)
    {
        _scoreboardService = scoreboardService;
        _logger = logger;
    }

    /// <summary>
    /// Handles the command to add a game result to the scoreboard.
    /// </summary>
    /// <param name="request">The command containing the game result to be added.</param>
    /// <param name="cancellationToken">Cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<Unit> Handle(AddScoreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _scoreboardService.AddResult(request.GameResult);
            _logger.LogInformation("Game result added successfully");
            return Task.FromResult(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while adding game result to scoreboard");
            throw;
        }
    }
}
