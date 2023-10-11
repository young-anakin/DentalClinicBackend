using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.CreditDTO;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Models;
using DentalClinic.Services.CompanySettingService;
using Microsoft.EntityFrameworkCore;

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
            var compSet = await _context.CompanySettings.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Company settings not set!!!");

            if (DTO.CreditAmount > compSet.MaximumLoanAmount)
            {
                throw new InvalidOperationException("Credit exceeds Maximum loan amount alloted");
            }
            var cr = await _context.Credits.
                        Where(p => p.PatientID == DTO.PatientID).
                        OrderByDescending(p => p.ChargeDate).
                        FirstOrDefaultAsync();

            var Credit = new Credit
            {
                PatientID = DTO.PatientID,
                TotalCreditAmount = DTO.CreditAmount,
                IssuedBy = DTO.IssuedBy,
                ChargeDate = DTO.DateTime,
                Paid = 0,
                UnPaid = 0
            };

            if (cr != null)
            {
                // Case 1: Update existing Credit record
                if (cr.TotalCreditAmount + DTO.CreditAmount > compSet.MaximumLoanAmount)
                {
                    throw new InvalidOperationException("Credit exceeds Maximum loan amount alloted");
                }
                Credit.TotalCreditAmount = cr.TotalCreditAmount + DTO.CreditAmount;
                Credit.Paid = cr.Paid;
                Credit.UnPaid = cr.UnPaid;
            }

           

            _context.Credits.Add(Credit);
            await _context.SaveChangesAsync();
            return Credit;

        }
        public async Task<Credit> RecentCreditInfo(int id)
        {
            var credit = await _context.Credits.
                            Where(p=> p.PatientID == id)
                            .OrderByDescending(p=>p.ChargeDate)
                            .FirstOrDefaultAsync()??throw new KeyNotFoundException("Patient hasn't been Credited with any charges.");
            return credit;
        }
        public async Task<List<Credit>> CreditHistoryForPatient(int id)
        {
            var credit = await _context.Credits.
                            Where(p => p.PatientID == id)
                            .OrderByDescending(p => p.ChargeDate)
                            .ToListAsync() ?? throw new KeyNotFoundException("Patient hasn't been Credited with any charges.");
            return credit;
        }

    }
}

