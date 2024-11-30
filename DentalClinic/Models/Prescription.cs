using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        public string? DrugName { get; set; }

        public string? Strength { get; set; }

        public string? DosageFromDoseFrequency { get; set; }

        public string? DurationQuantity { get; set; }

        public string ? HowToUse { get; set; }

        public string? OtherInformation { get; set; }

        public Decimal? TotalPrice { get; set; }

        public string? Registrations { get; set; }

        public string? Qualification { get; set; }

        //[ForeignKey("PatientId")]
        //public int? PatientId { get; set; }
        //public Patient? Patient { get; set; }

        //// Relationship to Employee
        //[ForeignKey("EmployeeId")]
        //public int? EmployeeId { get; set; }
        //public Employee? Employee { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
 
    }
}
