using Lombok.NET;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PDS.Data
{
    [AllArgsConstructor]
    public partial class BaseData
    {
        [Key]
        public String GUID { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
    }
}
