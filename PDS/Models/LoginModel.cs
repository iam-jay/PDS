using Lombok.NET;

namespace PDS.Models
{
    [NoArgsConstructor]
    [AllArgsConstructor]
    public partial class LoginModel
    {
        public string UserEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
