namespace PDS.Models
{
    public class LoginResponse
    {
        public string UserID { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty ;
        public string UserType { get; set; } = string.Empty ;
    }
}
