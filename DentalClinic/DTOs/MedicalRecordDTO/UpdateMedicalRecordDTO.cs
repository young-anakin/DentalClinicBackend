namespace DentalClinic.DTOs.MedicalRecordDTO
{
    public class UpdateMedicalRecordDTO
    {
        public int MedicalRecordID { get; set; }
        
        public int[]? Procedures { get; set; }

        public int[]? Quantities { get; set; }

        public int DiscountPercent { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; } = false;

        public string ProcedureIDs { set; get; } = string.Empty;

        //public string Quantities { get; set; } = string.Empty;

        public decimal SubTotalAmount { get; set; }
    }
}
