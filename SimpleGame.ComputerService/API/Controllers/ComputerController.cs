using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleGame.ComputerService.Core.Application.Dtos;
using SimpleGame.ComputerService.Core.Application.Queries.AddComputerChoice;

namespace SimpleGame.ComputerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ComputerController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling requests.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        public ComputerController(IMediator mediator, ILogger<ComputerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Gets a random computer choice for the game.
        /// </summary>
        /// <returns>Returns a random computer choice.</returns>
        [HttpGet("choice")]
        [ProducesResponseType(typeof(ComputerChoiceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComputerChoice()
        {
            try
            {
                _logger.LogInformation("Processing GetComputerChoice request...");

                var result = await _mediator.Send(new GetComputerChoiceCommand());

                _logger.LogInformation("Successfully processed GetComputerChoice request.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing GetComputerChoice request.");
                return StatusCode(500, "An error occurred while fetching the computer choice.");
            }
        }
    }
}