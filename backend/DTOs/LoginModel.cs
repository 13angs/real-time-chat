using Newtonsoft.Json;

namespace backend.DTOs
{
    public class LoginModel
    {
        [JsonProperty("user_id")]
        public int Id { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}