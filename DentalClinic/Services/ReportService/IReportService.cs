namespace DentalClinic.Services.ReportService
{
    public interface IReportService
    {
        Task<List<object>> GenderBySubCity();
    }
}