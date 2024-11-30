using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public string PatientFullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        [RegularExpression(@"([0-9]){9}$", ErrorMessage = "Invalid Phone Number")]

        public string? HomeNumber { get; set; }
        public string? TelephonePhone { get; set; } = null!;

        public bool? InPatient { get; set; }
        public string? Gender { get; set; } = string.Empty;

        public string? Country { get; set; } = string.Empty;

        public Decimal? Weight { get; set; }

        public string? Region { get; set; }

        public string? Town { get; set; }

        public string? Woreda { get; set; }

        public string? Kebele { get; set; }

        public string? City { get; set; } 

        public string? Subcity { get; set; } 

        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<HealthProgress>? HealthProgresses { get; set; }

        public List<MedicalRecord>? MedicalRecords { get; set; }

        public PatientProfile? Profile { get; set; }

        public List<PatientVisit>? PatientVisits { get; set;}
        [System.Text.Json.Serialization.JsonIgnore]

        public List<Appointment>? Appointments { get; set;}

        public PatientCard? PatientCard { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public List<Credit>? Credits { get; set; }

        //public List<Prescription>? Prescriptions { get; set; }


    }
}
