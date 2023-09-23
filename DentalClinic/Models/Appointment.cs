using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Dentist")]
        public int? DentistID { get; set; }
        public Employee? Dentist { get; set; } 

        public DateTime AppointmentDate { get; set; }

        [ForeignKey("ActionBy")]
        public int? ActionByID { get; set; }
        public Employee? ActionBy { get; set; } 

        public string ActionName { get; set; } = string.Empty;
    }
}
