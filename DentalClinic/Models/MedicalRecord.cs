using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Models
{
    public class MedicalRecord
    {
        [Key]
        public int Medical_RecordID { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public DateTime? Date { get; set; } = DateTime.Now;

        public string LabTests { get; set; } = string.Empty;

        public string PrescribedMedicines { get; set; } = string.Empty;

        public string TreatmentDetails { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
        public Patient? Patient { get; set; }

        public List<Referal>? Referals { get; set; } = new();

        public List<Procedure>? Procedures { get; set; } = new();

        [ForeignKey("TreatedBy")]
        public int? TreatedById { get; set; }

        public Employee? TreatedBy { get; set; }

    }
}
