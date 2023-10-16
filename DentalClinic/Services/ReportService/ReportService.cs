﻿using AutoMapper;
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
        public async Task<List<Object>> GenderBySubCity(DateTimeRangeDTOForCity DTO)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (DTO.ActionName.Equals("daily", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-1);
                endDate = DateTime.Now;
            }

            if (DTO.ActionName.Equals("weekly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-6);
                endDate = DateTime.Now;
            }
            else if (DTO.ActionName.Equals("monthly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("yearly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, 1, 1);
                endDate = startDate.AddYears(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("AllTime", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(2000, 1, 1);
                endDate = DateTime.Now;
            }
            else
            {
                startDate = DTO.StartDate;
                endDate = DTO.EndDate;
            }
            var data = _context.Patients
                .Where(patient =>
                    (patient.CreatedAt >= startDate) &&
                    (patient.CreatedAt <= endDate) &&
                    (DTO.CityName != "All" || patient.City == DTO.CityName) &&
                    (DTO.CountryName == "All" || patient.Country == DTO.CountryName))
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
        //public async Task<RevenuesDisplayDTO> Revenues()
        //{
        //    List<Payment> payments = await _context.Payments.ToListAsync();

        //    decimal totalRevenue = payments.Sum(payment => payment.Total);

        //    RevenuesDisplayDTO rv = new RevenuesDisplayDTO
        //    {
        //        TotalRevenues = totalRevenue,
        //    };
        //    return rv;
        //}
        public async Task<RevenuesDisplayDTO> Revenues(DateTimeRangeDTO DTO)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (DTO.ActionName.Equals("daily", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-1);
                endDate = DateTime.Now;
            }

            if (DTO.ActionName.Equals("weekly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-6);
                endDate = DateTime.Now;
            }
            else if (DTO.ActionName.Equals("monthly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("yearly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, 1, 1);
                endDate = startDate.AddYears(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("AllTime", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(2000, 1, 1);
                endDate = DateTime.Now;
            }
            else
            {
                startDate = DTO.StartDate ;
                endDate = DTO.EndDate ;
            }

            List<Payment> payments = await _context.Payments
                .Where(payment =>
                    (payment.PaymentDate >= startDate) &&
                    (payment.PaymentDate <= endDate))
                .ToListAsync();

            decimal totalRevenue = payments.Sum(payment => payment.Total);



            RevenuesDisplayDTO rv = new RevenuesDisplayDTO
            {
                TotalRevenues = totalRevenue,
                // Add other properties as needed (collected amount, credited amount, etc.)
            };

            return rv;
        }


        public async Task<RevenuesDisplayDTO> CollectedAmounts(DateTimeRangeDTO DTO)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (DTO.ActionName.Equals("daily", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-1);
                endDate = DateTime.Now;
            }

            if (DTO.ActionName.Equals("weekly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-6);
                endDate = DateTime.Now;
            }
            else if (DTO.ActionName.Equals("monthly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("yearly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, 1, 1);
                endDate = startDate.AddYears(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("AllTime", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(2000, 1, 1);
                endDate = DateTime.Now;
            }
            else
            {
                startDate = DTO.StartDate;
                endDate = DTO.EndDate;
            }

            List<Payment> payments = await _context.Payments
                .Where(payment =>
                    (payment.PaymentDate >= startDate) &&
                    (payment.PaymentDate <= endDate) && (payment.IsCredit == false))
                .ToListAsync();

            decimal totalRevenue = payments.Sum(payment => payment.Total);



            RevenuesDisplayDTO rv = new RevenuesDisplayDTO
            {
                TotalRevenues = totalRevenue,
                // Add other properties as needed (collected amount, credited amount, etc.)
            };

            return rv;
        }
        public async Task<RevenuesDisplayDTO> CreditedAmount(DateTimeRangeDTO DTO)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (DTO.ActionName.Equals("daily", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-1);
                endDate = DateTime.Now;
            }

            if (DTO.ActionName.Equals("weekly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-6);
                endDate = DateTime.Now;
            }
            else if (DTO.ActionName.Equals("monthly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("yearly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, 1, 1);
                endDate = startDate.AddYears(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("AllTime", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(2000, 1, 1);
                endDate = DateTime.Now;
            }
            else
            {
                startDate = DTO.StartDate;
                endDate = DTO.EndDate;
            }

            List<Payment> payments = await _context.Payments
                .Where(payment =>
                    (payment.PaymentDate >= startDate) &&
                    (payment.PaymentDate <= endDate) && (payment.IsCredit == true))
                .ToListAsync();

            decimal totalRevenue = payments.Sum(payment => payment.Total);



            RevenuesDisplayDTO rv = new RevenuesDisplayDTO
            {
                TotalRevenues = totalRevenue,
                // Add other properties as needed (collected amount, credited amount, etc.)
            };

            return rv;
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

        public async Task<List<Object>> TotalRevenuesPerGender(DateTimeRangeDTO DTO)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (DTO.ActionName.Equals("daily", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-1);
                endDate = DateTime.Now;
            }
            if (DTO.ActionName.Equals("weekly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.Now.AddDays(-6);
                endDate = DateTime.Now;
            }
            else if (DTO.ActionName.Equals("monthly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("yearly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.Now.Year, 1, 1);
                endDate = startDate.AddYears(1).AddDays(-1);
            }
            else if (DTO.ActionName.Equals("AllTime", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(2000, 1, 1);
                endDate = DateTime.Now;
            }
            else
            {
                startDate = DTO.StartDate;
                endDate = DTO.EndDate;
            }
            var data = await _context.Payments
                 .Where(payment =>
                    (payment.PaymentDate >= startDate) &&
                    (payment.PaymentDate <= endDate) && (payment.IsCredit == true))
                .Include(p => p.Patient) // Include the related Patient
                .GroupBy(p => p.Patient.Gender) // Group by patient's gender
                .Select(g => new
                {
                    Gender = g.Key,
                    TotalRevenue = g.Sum(p => p.Total)
                })
                .ToListAsync();

            return data.Cast<Object>().ToList();
        }

        public async Task<List<Object>> GetRoleGenderCounts()
        {
            var data = await _context.UserAccounts
                .Include(u => u.Role)
                .Include(u => u.Employee)
                .Where(u => u.Employee != null && u.Employee.EmployeeGender != null)
                .GroupBy(u => u.Role.RoleName)
                .Select(g => new
                {
                    RoleName = g.Key,
                    Male = g.Count(u => u.Employee.EmployeeGender == "Male"),
                    Female = g.Count(u => u.Employee.EmployeeGender == "Female")
                })
                .ToListAsync();

            return data.Cast<Object>().ToList();
        }
        public async Task<List<Object>> TotalActiveInactiveEmployeesByRole()
        {
            var data = await _context.Roles
                .Include(role => role.UserAccounts)
                    .ThenInclude(userAccount => userAccount.Employee)
                .Select(role => new
                {
                    RoleName = role.RoleName,
                    ActiveCount = role.UserAccounts.Count(u => u.Employee != null && u.Employee.IsCurrentlyActive),
                    InactiveCount = role.UserAccounts.Count(u => u.Employee != null && !u.Employee.IsCurrentlyActive)
                })
                .ToListAsync();

            return data.Cast<Object>().ToList();
        }




    }
}
