namespace SimpleGame.GameService.Core.Domain.Interfaces
{
    public interface IRandomNumberService
    {
        Task<int> GetRandomNumber();
    }
}
