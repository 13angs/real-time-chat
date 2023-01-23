using backend.Models;
using Newtonsoft.Json;

namespace backend.DTOs
{
    public class UserModel : BaseEntity
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}