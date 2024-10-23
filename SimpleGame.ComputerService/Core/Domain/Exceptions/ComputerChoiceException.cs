namespace SimpleGame.ComputerService.Core.Domain.Exceptions;

public class ComputerChoiceException : Exception
{
    public ComputerChoiceException(string message)
        : base(message)
    {
    }

    public ComputerChoiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
