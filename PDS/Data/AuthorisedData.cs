using Lombok.NET;

namespace PDS.Data
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class AuthorisedData : BaseData
    {
        public string PatientId { get; set; }  = string.Empty;
        public string CleintId { get; set; } = string.Empty;
    }
}
