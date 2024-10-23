using MediatR;
using SimpleGame.ComputerService.Core.Application.Dtos;
using SimpleGame.ComputerService.Core.Domain.Exceptions;
using SimpleGame.ComputerService.Core.Domain.Interfaces;

namespace SimpleGame.ComputerService.Core.Application.Queries.AddComputerChoice;

public class GetComputerChoiceCommandHandler : IRequestHandler<GetComputerChoiceCommand, ComputerChoiceDto>
{
    private readonly IComputerChoiceService _computerChoiceService;
    private readonly ILogger<GetComputerChoiceCommandHandler> _logger;

    public GetComputerChoiceCommandHandler(IComputerChoiceService computerChoiceService, ILogger<GetComputerChoiceCommandHandler> logger)
    {
        _computerChoiceService = computerChoiceService;
        _logger = logger;
    }

    public async Task<ComputerChoiceDto> Handle(GetComputerChoiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling GetComputerChoiceCommand...");

            var choice = await _computerChoiceService.GetRandomComputerChoiceAsync();
            var result = new ComputerChoiceDto
            {
                Id = (int)choice,
                Name = choice.ToString()
            };

            _logger.LogInformation("Successfully generated computer choice: {Choice}", result.Name);

            return result;
        }
        catch (ComputerChoiceException ex)
        {
            _logger.LogError(ex, "Error occurred while generating computer choice.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred in GetComputerChoiceCommandHandler.");
            throw new ComputerChoiceException("An unexpected error occurred while generating a computer choice.", ex);
        }
    }
}
