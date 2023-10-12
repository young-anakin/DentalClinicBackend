using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.CreditDTO;
using DentalClinic.DTOs.MobileBankingDTO;
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
            var cr = await _context.Credits.Where(p => p.PatientID == DTO.PatientID).FirstOrDefaultAsync();
            CreditPaymentRecord CPR = new CreditPaymentRecord
            {
                PatientID = DTO.PatientID,
                Paid = DTO.CreditAmount,
                CreateAt = DateTime.Now,
                IssuedBy = DTO.IssuedBy,
                PaymentType = DTO.PaymentType

            };
            if (cr == null)
            {
                Credit credit = new Credit
                {
                    PatientID = DTO.PatientID,
                    TotalCreditAmount = DTO.CreditAmount,
                    UnPaid =0,
                    Paid = DTO.CreditAmount,
                    IssuedBy = DTO.IssuedBy,
                    ChargeDate = DateTime.Now
                    
                };


                _context.Credits.Add(credit);
                _context.CreditPaymentRecords.Add(CPR);
                await _context.SaveChangesAsync();
                return credit;
            }


            cr.TotalCreditAmount = cr.TotalCreditAmount + DTO.CreditAmount;
            cr.Paid = cr.Paid + DTO.CreditAmount;
            cr.UnPaid = cr.UnPaid - DTO.CreditAmount;

            _context.Credits.Update(cr);
            _context.CreditPaymentRecords.Add(CPR);
            await _context.SaveChangesAsync();
            return cr;
        }
        public async Task<Credit> CurrentCreditInfo(int DTO)
        {
            var credit = await _context.Credits.
                            Where(p=> p.PatientID == DTO)
                            .OrderByDescending(p=>p.ChargeDate)
                            .FirstOrDefaultAsync()??throw new KeyNotFoundException("Patient hasn't been Credited with any charges.");
            return credit;
        }
        public async Task<List<CreditPaymentRecord>> CreditHistoryForPatient(int DTO)
        {
            var credit = await _context.CreditPaymentRecords.
                            Where(p => p.PatientID == DTO)
                            .OrderByDescending(p => p.CreateAt)
                            .ToListAsync() ?? throw new KeyNotFoundException("Patient hasn't been Credited with any charges.");
            return credit;
        }

    }
}

