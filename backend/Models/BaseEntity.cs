using Newtonsoft.Json;

namespace backend.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate=DateTime.Now;
            ModifiedDate=DateTime.Now;
        }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}