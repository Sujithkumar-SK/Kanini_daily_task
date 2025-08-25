using System.Text.Json.Serialization;

namespace Frontend.Models
{
    public class KidsDto
    {
        [JsonPropertyName("KidId")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }

        [JsonPropertyName("user")]
        public UserDto? User { get; set; }
    }
}
