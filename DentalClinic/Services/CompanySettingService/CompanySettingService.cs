using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Models;
using DentalClinic.Services.EmployeeService;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.CompanySettingService
{
    public class CompanySettingService : ICompanySettingService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CompanySettingService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompanySetting> AddCompanySettingService(AddCompanySettingsDTO companySettingsDTO)
        {
            var compSetting = _mapper.Map<CompanySetting>(companySettingsDTO);
            _context.CompanySettings.Add(compSetting);
            await _context.SaveChangesAsync();
            return compSetting;
        }
        public async Task<CompanySetting> UpdateCompanySetting(UpdateCompanySettingDTO companySettingDTO)
        {
            var compSetting = await _context.CompanySettings
                                    .Where(cs => cs.CompanySettingID == companySettingDTO.CompanySettingIDs)
                                    .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Company setting Not Found");
            compSetting = _mapper.Map(companySettingDTO, compSetting);
            _context.CompanySettings.Update(compSetting);
            await _context.SaveChangesAsync();
            return compSetting;
        }

        public async Task<CompanySetting> GetCompanySetting()
        {
            var compSetting = await _context.CompanySettings
                                        .OrderByDescending(cs => cs.UpdatedAt)
                                        .FirstOrDefaultAsync()??throw new KeyNotFoundException("Settings Not Found");
            return compSetting;

        }
    }
}


