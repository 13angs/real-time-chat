using backend.Models;
using Newtonsoft.Json;

namespace backend.DTOs
{
    public class MessageModel : BaseEntity
    {
        [JsonProperty("from")]
        public string? From { get; set; }

        [JsonProperty("to")]
        public string? To { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }
    }
}