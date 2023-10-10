using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.CreditDTO;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Models;
using DentalClinic.Services.CompanySettingService;

namespace DentalClinic.Services.CreditService
{
    public class CreditService : ICreditService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CreditService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Update works by updating only 1 record that's in the database and changing it
        public async Task<Credit> ChargeCredit(ChargeCreditDTO DTO)
        {
            var Credit = new Credit
            {
                PatientID = DTO.PatientID,
                TotalCreditAmount = DTO.CreditAmount,
                IssuedBy = DTO.IssuedBy,
                ChargeDate = DTO.DateTime,
                Paid = 0,
                UnPaid = 0
            };
            _context.Credits.Add(Credit);
            await _context.SaveChangesAsync();
            return Credit;

        }

    }
}

