using Lombok.NET;
using Newtonsoft.Json;
using PDS.Data;

namespace PDS.Models
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class PatientProfile : BaseData
    {
        public string Title { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PermanentAddress { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string PUI { get; set; } = string.Empty;
        public String DOB { get; set; } = String.Empty;
        public String Gender { get; set; } = string.Empty;
        public String MaritalStatus { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public String ContactNumber { get; set; } = string.Empty;
        public String Status { get; set; } = string.Empty;
        [JsonIgnore]
        public String RegistrationStatus { get; set; } = string.Empty;

    }
}
