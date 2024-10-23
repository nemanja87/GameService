namespace SimpleGame.GameService.Core.Application.Dtos
{
    public class GameResultDto
    {
        public string PlayerChoice { get; set; } = string.Empty;
        public string ComputerChoice { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
