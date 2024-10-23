using MediatR;
using SimpleGame.ScoreboardServiceApp.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardServiceApp.Core.Application.Commands.AddScore;

public class AddScoreCommandHandler : IRequestHandler<AddScoreCommand, Unit>
{
    private readonly IScoreboardService _scoreboardService;

    public AddScoreCommandHandler(IScoreboardService scoreboardService)
    {
        _scoreboardService = scoreboardService;
    }

    public Task<Unit> Handle(AddScoreCommand request, CancellationToken cancellationToken)
    {
        _scoreboardService.AddResult(request.GameResult);

        // Return a Task with Unit.Value
        return Task.FromResult(Unit.Value);
    }
}
