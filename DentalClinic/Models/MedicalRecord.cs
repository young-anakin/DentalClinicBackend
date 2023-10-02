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

        //public string LabTests { get; set; } = string.Empty;

        public string PrescribedMedicinesandNotes { get; set; } = string.Empty;
        [System.Text.Json.Serialization.JsonIgnore]
        public Patient? Patient { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public string ReferalList { get; set; } = string.Empty;
        [System.Text.Json.Serialization.JsonIgnore]

        public List<Procedure>? Procedures { get; set; } = new();

        [ForeignKey("TreatedBy")]
        public int? TreatedById { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public Employee? TreatedBy { get; set; }
        
        public int DiscountPercent { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
