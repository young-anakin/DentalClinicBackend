using DentalClinic.DTOs.CreditDTO;
using DentalClinic.DTOs.MobileBankingDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.CreditService
{
    public interface ICreditService
    {
        Task<Credit> ChargeCredit(ChargeCreditDTO DTO);
        Task<List<CreditPaymentRecord>> CreditHistoryForPatient(int DTO);
        Task<Credit> CurrentCreditInfo(int DTO);
    }
}