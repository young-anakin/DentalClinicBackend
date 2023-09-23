using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Models
{
    public class PatientProfile
    {
        [Key]
        [ForeignKey("Patient")]
        public int patient_Id { get; set; }

        public string MedicalHistory { get; set; } = string.Empty;

        public string Allergies { get; set; } = string.Empty;

        public string previousConditions { get; set; } = string.Empty;

        public string FamilyHistory { get; set; } = string.Empty;

        public Patient? Patient { get; set; }
    }
}
