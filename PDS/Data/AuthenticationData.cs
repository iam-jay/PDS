using Lombok.NET;

namespace PDS.Data
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class AuthenticationData : BaseData
    {
        public string UserId { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}
