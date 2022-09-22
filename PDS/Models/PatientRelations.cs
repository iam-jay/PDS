using Lombok.NET;
using PDS.Data;

namespace PDS.Models
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class PatientRelations : BaseData
    {
        public String PatientId { get; set; }
        public String RelativePatientId { get; set; }
        public String RelationType { get; set; }
        public Boolean IsActive { get; set; } = true;

    }
}
