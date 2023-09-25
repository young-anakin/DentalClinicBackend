using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.CompanySettingService
{
    public interface ICompanySettingService
    {
        Task<CompanySetting> AddCompanySettingService(AddCompanySettingsDTO companySettingsDTO);
        Task<CompanySetting> GetCompanySetting();
        Task<CompanySetting> UpdateCompanySetting(UpdateCompanySettingDTO companySettingDTO);
    }
}