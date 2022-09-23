using Lombok.NET;
using PDS.Data;

namespace PDS.DTO
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class PatientConsultations
    {
        public PatientData? patientData { get; set; }
        public List<ConsultationData>? consultations { get; set; }
    }
}
