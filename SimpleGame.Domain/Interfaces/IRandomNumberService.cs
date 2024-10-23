namespace SimpleGame.Domain.Interfaces
{
    public interface IRandomNumberService
    {
        Task<int> GetRandomNumber();
    }
}
