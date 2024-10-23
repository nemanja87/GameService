using Newtonsoft.Json;

namespace SimpleGame.GameServiceApp.Core.Application.Dtos
{
    public class RandomNumberResponseDto
    {
        [JsonProperty("random_number")]
        public int RandomNumber { get; set; }
    }

}
