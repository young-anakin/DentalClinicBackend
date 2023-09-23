using System.ComponentModel.DataAnnotations;

namespace DentalClinic.DTOs.PatientDTO
{
    public class AddPatientDTO
    {
        public string PatientFullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = null!;

        public string Gender { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string subcity { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string MedicalHistory { get; set; } = string.Empty;

        public string Allergies { get; set; } = string.Empty;

        public string previousConditions { get; set; } = string.Empty;

        public string FamilyHistory { get; set; } = string.Empty;
    }
}
