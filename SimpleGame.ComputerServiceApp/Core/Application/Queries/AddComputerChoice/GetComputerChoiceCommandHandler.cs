using MediatR;
using SimpleGame.ComputerServiceApp.Core.Application.Dtos;
using SimpleGame.ComputerServiceApp.Core.Domain.Interfaces;

namespace SimpleGame.ComputerServiceApp.Core.Application.Queries.AddComputerChoice;

public class GetComputerChoiceCommandHandler : IRequestHandler<GetComputerChoiceCommand, ComputerChoiceDto>
{
    private readonly IComputerChoiceService _computerChoiceService;

    public GetComputerChoiceCommandHandler(IComputerChoiceService computerChoiceService)
    {
        _computerChoiceService = computerChoiceService;
    }

    public async Task<ComputerChoiceDto> Handle(GetComputerChoiceCommand request, CancellationToken cancellationToken)
    {
        var choice = await _computerChoiceService.GetRandomComputerChoiceAsync();
        return new ComputerChoiceDto
        {
            Id = (int)choice,
            Name = choice.ToString()
        };
    }
}
