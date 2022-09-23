using Lombok.NET;
using PDS.Data;

namespace PDS.Models
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class ConsentResponseData : BaseData
    {
        public int OTP { get; set; }
        public String PUI { get; set; }
        public String ClientId { get; set; }
        public int TTLinMins { get; set; } = 2;
    }
}
