namespace SimpleGame.GameServiceApp.Core.Domain.Interfaces
{
    public interface IRandomNumberService
    {
        Task<int> GetRandomNumber();
    }
}
