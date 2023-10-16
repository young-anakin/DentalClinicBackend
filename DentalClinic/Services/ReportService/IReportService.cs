using DentalClinic.DTOs.ReportDTO;

namespace DentalClinic.Services.ReportService
{
    public interface IReportService
    {
        Task<RevenuesDisplayDTO> CollectedAmount();
        Task<RevenuesDisplayDTO> CreditedAmount();
        Task<List<object>> GenderBySubCity();
        Task<RevenuesDisplayDTO> Revenues();
        Task<List<object>> TotalDentistsPerGender();
        Task<List<object>> TotalNumberofPatientByGender();
        Task<RevenuesDisplayDTO> TotalNumberOfProcedures();
        Task<Dictionary<string, decimal>> TotalRevenuePerGender();
        Task<List<object>> TotalUsers();
    }
}