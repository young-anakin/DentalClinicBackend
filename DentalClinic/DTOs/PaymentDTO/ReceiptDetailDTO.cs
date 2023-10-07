using DentalClinic.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentalClinic.DTOs.PaymentDTO
{
    public class ReceiptDetailDTO
    {
            public int Id { get; set; }
            public decimal Discount { get; set; }
            public string PatientName { get; set; } = string.Empty;
            public decimal SubTotal { get; set; }
            public string IssuedBy{ get; set; } = string.Empty;
            public decimal Total { get; set; }
            public string PaymentType { get; set; } = string.Empty;
            public DateTime PaymentDate { get; set; }
        }
    }

