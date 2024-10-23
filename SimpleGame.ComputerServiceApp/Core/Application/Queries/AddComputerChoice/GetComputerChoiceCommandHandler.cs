using MediatR;
using SimpleGame.ComputerService.Core.Application.Dtos;
using SimpleGame.ComputerService.Core.Domain.Interfaces;

namespace SimpleGame.ComputerService.Core.Application.Queries.AddComputerChoice;

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
