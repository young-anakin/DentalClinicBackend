namespace DentalClinic.DTOs.PaymentDTO
{
    public class MakePaymentMedRecDTO
    {
        public int MedicalRecordID { get; set; }

        public int PaymentType { get; set; }

        public int IssuedByID { get; set; }

        public int PatientID { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public int Discount { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
