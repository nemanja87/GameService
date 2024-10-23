using Newtonsoft.Json;

namespace SimpleGame.GameService.Core.Application.Dtos
{
    public class RandomNumberResponseDto
    {
        [JsonProperty("random_number")]
        public int RandomNumber { get; set; }
    }

}
