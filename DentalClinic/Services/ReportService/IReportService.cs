using DentalClinic.DTOs.ReportDTO;

namespace DentalClinic.Services.ReportService
{
    public interface IReportService
    {
        Task<RevenuesDisplayDTO> CollectedAmounts(DateTimeRangeDTO DTO);
        Task<RevenuesDisplayDTO> CreditedAmount(DateTimeRangeDTO DTO);
        Task<List<object>> GenderBySubCity(DateTimeRangeDTOForCity DTO);
        Task<RevenuesDisplayDTO> Revenues(DateTimeRangeDTO DTO);
        Task<List<object>> TotalDentistsPerGender();
        Task<List<object>> TotalNumberofPatientByGender();
        Task<RevenuesDisplayDTO> TotalNumberOfProcedures();
        Task<List<object>> TotalRevenuesPerGender(DateTimeRangeDTO DTO);
        Task<List<object>> TotalUsers();
    }
}