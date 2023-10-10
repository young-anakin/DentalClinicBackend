using DentalClinic.DTOs.CreditDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.CreditService
{
    public interface ICreditService
    {
        Task<Credit> ChargeCredit(ChargeCreditDTO DTO);
    }
}