namespace SimpleGame.GameServiceApp.Core.Domain.Models
{
    public class ComputerDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public int RamSize { get; set; } // Size in GB
        public string GraphicsCard { get; set; } = string.Empty;
        public DateTime ManufactureDate { get; set; } = DateTime.Now;
    }
}
