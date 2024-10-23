using MediatR;
using SimpleGame.ComputerService.Core.Application.Dtos;

namespace SimpleGame.ComputerService.Core.Application.Queries.AddComputerChoice;

public class GetComputerChoiceCommand : IRequest<ComputerChoiceDto> { }
