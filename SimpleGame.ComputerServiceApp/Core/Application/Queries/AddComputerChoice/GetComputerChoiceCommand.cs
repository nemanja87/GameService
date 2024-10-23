using MediatR;
using SimpleGame.ComputerServiceApp.Core.Application.Dtos;

namespace SimpleGame.ComputerServiceApp.Core.Application.Queries.AddComputerChoice;

public class GetComputerChoiceCommand : IRequest<ComputerChoiceDto>
{
    // No properties required for this command since we are just fetching a random computer choice.
}
