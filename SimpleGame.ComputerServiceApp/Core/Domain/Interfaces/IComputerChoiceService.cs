using SimpleGame.ComputerService.Core.Domain.Enum;

namespace SimpleGame.ComputerService.Core.Domain.Interfaces
{
    public interface IComputerChoiceService
    {
        /// <summary>
        /// Asynchronously retrieves a random computer choice from the available game options.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing the random computer choice.</returns>
        Task<ComputerChoiceEnum> GetRandomComputerChoiceAsync();
    }
}
