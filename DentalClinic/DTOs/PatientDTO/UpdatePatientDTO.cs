namespace DentalClinic.DTOs.PatientDTO
{
    public class UpdatePatientDTO
    {
        public int PatientID { set; get; }
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

        public string Chronics { get; set; } = string.Empty;

    }
}
