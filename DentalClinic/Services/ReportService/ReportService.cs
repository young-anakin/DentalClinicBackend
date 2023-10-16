using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.ReportDTO;
using DentalClinic.Models;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.ReportService
{
    public class ReportService : IReportService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public ReportService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }
        //public async Task<List<ProcedureGenderDisplayDTO>> ProcedureGender()
        //{
        //    var procedure = await _context.Procedures.Where(p => p.)
        //}
        public async Task<List<Object>> GenderBySubCity()
        {
            var data = _context.Patients
                    .GroupBy(p => p.Subcity)
                    .Select(g => new
                    {
                        SubCity = g.Key,
                        Male = g.Count(p => p.Gender == "Male"),
                        Female = g.Count(p => p.Gender == "Female")
                    })
                    .ToList();
            return data.Cast<object>().ToList();
        }
        public async Task<RevenuesDisplayDTO> Revenues()
        {
            List<Payment> payments = await _context.Payments.ToListAsync();

            decimal totalRevenue = payments.Sum(payment => payment.Total);

            RevenuesDisplayDTO rv = new RevenuesDisplayDTO
            {
                TotalRevenues = totalRevenue,
            };
            return rv;
        }
        public async Task<RevenuesDisplayDTO> CollectedAmount()
        {
            List<Payment> payments = await _context.Payments.Where(r=> r.IsCredit == false).ToListAsync();

            decimal totalRevenue = payments.Sum(payment => payment.Total);

            RevenuesDisplayDTO rv = new RevenuesDisplayDTO
            {
                TotalRevenues = totalRevenue,
            };
            return rv;
        }
        public async Task<RevenuesDisplayDTO> CreditedAmount()
        {
            List<Payment> payments = await _context.Payments.Where(r => r.IsCredit == true).ToListAsync();

            decimal totalRevenue = payments.Sum(payment => payment.Total);

            RevenuesDisplayDTO rv = new RevenuesDisplayDTO
            {
                TotalRevenues = totalRevenue,
            };
            return rv;
        }
        public async Task<Dictionary<string, decimal>> TotalRevenuePerGender()
        {
            // Assuming you have a list of Payment objects named payments
            List<Payment> payments = await _context.Payments.Where(r => r.IsCredit == true).ToListAsync();

            var totalRevenuePerGender = payments
                .Where(payment => payment.Patient != null && payment.Patient.Gender != null)
                .GroupBy(payment => payment.Patient.Gender)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(payment => payment.Total)
                );

            return totalRevenuePerGender;
        }
        public async Task<List<Object>> TotalNumberofPatientByGender()
        {
            var data = _context.Patients
                    .GroupBy(p => p.Gender)
                    .Select(g => new
        {
            Gender = g.Key,
            Male = g.Count(p => p.Gender == "Male" || p.Gender == "male" || p.Gender == "MALE"),
            Female = g.Count(p => p.Gender == "Female" || p.Gender == "female" || p.Gender == "FEMALE")
        })
        .ToList();
            return data.Cast<object>().ToList();
        }
        //Total Users By Gender
        public async Task<List<Object>> TotalUsers()
        {
            var data = _context.Employees
                    .GroupBy(p => p.EmployeeGender)
                    .Select(g => new
                    {
                        Gender = g.Key,
                        Male = g.Count(p => p.EmployeeGender == "Male" || p.EmployeeGender == "male" || p.EmployeeGender == "MALE"),
                        Female = g.Count(p => p.EmployeeGender == "Female" || p.EmployeeGender == "female" || p.EmployeeGender == "FEMALE")
                    })
        .ToList();
            return data.Cast<object>().ToList();
        }
        public async Task<RevenuesDisplayDTO> TotalNumberOfProcedures()
        {
            var data = await _context.Procedures.ToListAsync();
            int totalNumberOfProcedures = data.Count;
            RevenuesDisplayDTO DTO = new RevenuesDisplayDTO
            {
                TotalRevenues = totalNumberOfProcedures,
            };
            return DTO;
        }
        public async Task<List<Object>> TotalDentistsPerGender()
        {
            var data = await _context.Employees
                .Include(p => p.UserAccount)
                    .ThenInclude(p => p.Role)
                .Where(p => p.UserAccount.Role.RoleName.Equals("Dentist"))
                .GroupBy(p => p.EmployeeGender)
                .Select(g => new
                {
                    Gender = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return data.Cast<Object>().ToList();
        }




    }
}
