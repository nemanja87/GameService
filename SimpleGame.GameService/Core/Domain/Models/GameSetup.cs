namespace SimpleGame.GameService.Core.Domain.Models
{
    public class GameSetup
    {
        public string ComputerName { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public int RamSize { get; set; } // Size in GB
        public string GraphicsCard { get; set; } = string.Empty;
        public DateTime ManufactureDate { get; set; }
        public string GameMode { get; set; } = string.Empty;
        public int InitialLevel { get; set; }
        public string Difficulty { get; set; } = string.Empty;
    }
}
