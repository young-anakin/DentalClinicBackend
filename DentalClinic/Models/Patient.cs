using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public string PatientFullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"([0-9]){9}$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Subcity { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public List<HealthProgress>? HealthProgresses { get; set; }

        public List<MedicalRecord>? MedicalRecords { get; set; }

        public PatientProfile? Profile { get; set; }

        public List<PatientVisit>? PatientVisits { get; set;}

        public List<Appointment>? Appointments { get; set;}


    }
}
