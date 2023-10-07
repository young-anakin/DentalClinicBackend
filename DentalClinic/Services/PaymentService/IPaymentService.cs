using DentalClinic.DTOs.PaymentDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Payment> AddPaymentfromMedicalRecord(MakePaymentMedRecDTO DTO);
        Task<ReceiptDetailDTO> DisplayPaymentReceipt(int id);
    }
}