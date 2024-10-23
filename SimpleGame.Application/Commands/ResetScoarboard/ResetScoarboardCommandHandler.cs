using MediatR;
using SimpleGame.Domain.Interfaces;

namespace SimpleGame.Application.Commands.ResetScoarboard
{
    public class ResetScoreboardCommandHandler : IRequestHandler<ResetScoreboardCommand, Unit>
    {
        private readonly IScoreboardRepository _scoreboardRepository;

        public ResetScoreboardCommandHandler(IScoreboardRepository scoreboardRepository)
        {
            _scoreboardRepository = scoreboardRepository;
        }

        public async Task<Unit> Handle(ResetScoreboardCommand request, CancellationToken cancellationToken)
        {
            await _scoreboardRepository.ResetScoreboardAsync();
            return Unit.Value;
        }
    }
}
