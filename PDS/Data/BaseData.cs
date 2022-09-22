using Lombok.NET;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PDS.Data
{
    [AllArgsConstructor]
    public partial class BaseData
    {
        [Key]
        [JsonIgnore]
        public String GUID { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
    }
}
