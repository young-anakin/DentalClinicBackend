namespace DentalClinic.DTOs.PatientDTO
{
    public class DisplayPatientDTO
    {
        public int PatientId { get; set; }
        public string PatientFullName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Subcity { get; set; }
        public string Address { get; set; }
        public string MedicalHistory { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public string Chronics { get; set; } = string.Empty;
    }
}
