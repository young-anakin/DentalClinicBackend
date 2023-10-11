using DentalClinic.DTOs.CreditDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.CreditService
{
    public interface ICreditService
    {
        Task<Credit> ChargeCredit(ChargeCreditDTO DTO);
        Task<List<Credit>> CreditHistoryForPatient(int id);
        Task<Credit> RecentCreditInfo(int id);
    }
}