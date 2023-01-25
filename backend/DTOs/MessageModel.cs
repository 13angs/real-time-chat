using backend.Models;
using Newtonsoft.Json;

namespace backend.DTOs
{
    public class MessageModel : BaseEntity
    {
        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("to")]
        public int To { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }
    }
}