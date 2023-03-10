using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.DTOs;
using Newtonsoft.Json;

namespace backend.Models
{
    public class Message : MessageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public string? Id { get; set; }
        public int UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public User? User { get; set; }
    }
}