using DentalClinic.Models;

namespace DentalClinic.Services.PaymentTypeService
{
    public interface IPaymentTypeService
    {
        Task<PaymentType> AddPaymentType(string name);
        Task<List<PaymentType>> GetAllPaymentTypes();
        Task<PaymentType> RemovePaymentType(int PaymentType);
    }
}