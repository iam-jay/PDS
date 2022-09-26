using Lombok.NET;
using PDS.Data;
using System.Text.Json.Serialization;

namespace PDS.Models
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class Consultation : BaseData
    {

        [JsonIgnore]
        public string ClientId { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;
        [JsonIgnore]
        public string PatientId { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string ChiefComplaints { get; set; } = string.Empty;
        public List<string> Reports { get; set; } = new List<string>();
        public string Diagnosis { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public string ConsultationType { get; set; } = String.Empty;
        public DateTime ConsultationDate { get; set; }
    }
}
