using DentalClinic.Models;

namespace DentalClinic.DTOs.MedicalRecordDTO
{
    public class DisplayMedicalRecordDTO
    {
        public int Medical_RecordID { get; set; }
        public int PatientId { get; set; }
        public int TreatedById { get; set; }
        public string TreatedByName { get; set; } = string.Empty;
        public string PrescribedMedicinesandNotes { get; set; } = string.Empty;
        public string ReferalsList { get; set; } = string.Empty;
        public List<Procedure>? Procedures { get; set; }
        public int DiscountPercent { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime date { get; set; }
    }
}
