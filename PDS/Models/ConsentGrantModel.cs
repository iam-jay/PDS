using Lombok.NET;

namespace PDS.Models
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class ConsentGrantModel
    {
        public string ClientId { get; set; }
        public string PUI { get; set; }
        public int OTP { get; set; }

    }
}
