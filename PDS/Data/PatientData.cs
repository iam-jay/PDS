using Lombok.NET;
using PDS.Models;
using System.ComponentModel.DataAnnotations;

namespace PDS.Data
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class PatientData : PatientProfile
    {
        public PatientData addPatientDetails(PatientProfile patientProfile)
        {
            this.Status = patientProfile.Status;
            this.DOB = patientProfile.DOB;
            this.MaritalStatus = patientProfile.MaritalStatus;
            this.FirstName = patientProfile.FirstName;
            this.LastName = patientProfile.LastName;
            this.ContactNumber = patientProfile.ContactNumber;
            this.Email = patientProfile.Email;
            this.Title = patientProfile.Title;
            this.Gender = patientProfile.Gender;
            this.Nationality = patientProfile.Nationality;
            this.PermanentAddress = patientProfile.PermanentAddress;
            this.MiddleName = patientProfile.MiddleName;
            this.PUI = patientProfile.PUI;
            this.RegistrationStatus = "Pending";

            return this;
        }
    }
}
