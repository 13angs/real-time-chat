using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.DTOs;
using Newtonsoft.Json;

namespace backend.Models
{
    public class User : UserModel
    {
        public User()
        {
            Messages=new List<Message>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
    }
}