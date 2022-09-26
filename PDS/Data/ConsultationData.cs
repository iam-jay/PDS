using Lombok.NET;
using PDS.Models;

namespace PDS.Data
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class ConsultationData : Consultation
    {
        public ConsultationData addConsultationDetails(Consultation consultation)
        {
            this.ConsultationType = consultation.ConsultationType;
            this.Diagnosis = consultation.Diagnosis;
            this.ChiefComplaints = consultation.ChiefComplaints;
            this.DoctorName = consultation.DoctorName;
            this.Remark = consultation.Remark;
            this.PatientId = consultation.PatientId;
            this.Reports = consultation.Reports;
            this.ClientId = consultation.ClientId;
            this.ConsultationDate = consultation.ConsultationDate;
            this.HospitalName = consultation.HospitalName;
            return this;
        }
    }
}
