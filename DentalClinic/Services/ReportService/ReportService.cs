using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.ReportDTO;
using DentalClinic.Models;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

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
            else if (DTO.ActionName.Equals("weekly", StringComparison.OrdinalIgnoreCase))
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
                    (DTO.CityName.Equals("All", StringComparison.OrdinalIgnoreCase) || patient.City == DTO.CityName || DTO.CountryName.Equals("All", StringComparison.OrdinalIgnoreCase) || patient.Country == DTO.CountryName))

                .GroupBy(p => p.Subcity)
                .Select(g => new
                {
                    SubCity = g.Key,
                    Male = g.Count(p => p.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase)),
                    Female = g.Count(p => p.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
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
                        Male = g.Count(p => p.EmployeeGender.Equals("Male", StringComparison.OrdinalIgnoreCase)),
                        Female = g.Count(p => p.EmployeeGender.Equals("Female", StringComparison.OrdinalIgnoreCase))
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
                .Where(p => p.UserAccount.Role.RoleName.Equals("Dentist", StringComparison.OrdinalIgnoreCase))
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
                    (payment.PaymentDate <= endDate))
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
                    Male = g.Count(u => u.Employee.EmployeeGender.Equals("Male", StringComparison.OrdinalIgnoreCase)),
                    Female = g.Count(u => u.Employee.EmployeeGender.Equals("Female", StringComparison.OrdinalIgnoreCase))
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
        public async Task<List<ProcedureUsage>> GetProcedureUsage(DateTimeRangeDTO DTO)
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
            List<ProcedureUsage> procedureUsageList = new List<ProcedureUsage>();
            var _medicalRecords = await _context.MedicalRecords
                                              .Where(mr => mr.Date >= startDate && mr.Date <= endDate)
                                              .Include(p => p.Patient).ToListAsync();

            foreach (var record in _medicalRecords)
            {
                var procedureIds = string.IsNullOrEmpty(record.ProcedureIDs) ? new int[] {  } : JsonSerializer.Deserialize<int[]>(record.ProcedureIDs);
                if (procedureIds.IsNullOrEmpty())
                {
                    throw new KeyNotFoundException("Trying to Access Deleted Procedure");
                }
                foreach (var procedureId in procedureIds)
                {
                    var procedureName = await GetProcedureNameById(procedureId); // Await here

                    var existingUsage = procedureUsageList.FirstOrDefault(p => p.Procedures == procedureName);

                    if (existingUsage != null)
                    {
                        if (record.Patient.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase)) // Assuming PatientId is used for gender differentiation
                            existingUsage.Male++;
                        else
                            existingUsage.Female++;
                    }
                    else
                    {
                        var usage = new ProcedureUsage
                        {
                            Procedures = procedureName,
                            Male = record.Patient.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase) ? 1 : 0,
                            Female = record.Patient.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase) ? 1 : 0
                        };
                        procedureUsageList.Add(usage);
                    }
                }
            }

            return procedureUsageList;
        }

        // Inner class private method

        // procedure wise amount
        public async Task<List<ProcedureRevenue>> GetProcedureRevenues(DateTimeRangeDTO DTO)
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
            List<ProcedureRevenue> procedureRevenueList = new List<ProcedureRevenue>();
            var _medicalRecords = await _context.MedicalRecords
                                                            .Where(mr=> mr.Date>=startDate && mr.Date <= endDate)
                                                            .Include(p => p.Patient).ToListAsync();

            foreach (var record in _medicalRecords)
            {
                var procedureIds = string.IsNullOrEmpty(record.ProcedureIDs) ? new int[] { } : JsonSerializer.Deserialize<int[]>(record.ProcedureIDs);
                var quantities = string.IsNullOrEmpty(record.Quantities) ? new int[] { } : JsonSerializer.Deserialize<int[]>(record.Quantities);

                if (procedureIds.Length != quantities.Length)
                {
                    throw new InvalidOperationException("Procedure IDs and Quantities do not match.");
                }

                for (int i = 0; i < procedureIds.Length; i++)
                {
                    int procedureId = procedureIds[i];
                    int quantity = quantities[i];

                    var procedureName = await GetProcedureNameById(procedureId);
                    var procedurePrice = await GetProcedurePriceById(procedureId);

                    decimal subtotal = quantity * procedurePrice;
                    decimal discountAmount = subtotal * (record.DiscountPercent / 100m);
                    decimal totalAmount = subtotal - discountAmount;

                    var existingProcedure = procedureRevenueList.FirstOrDefault(p => p.Procedure == procedureName);

                    if (existingProcedure != null)
                    {
                        existingProcedure.Revenue += totalAmount;
                    }
                    else
                    {
                        var procedureRevenue = new ProcedureRevenue
                        {
                            Procedure = procedureName,
                            Revenue = totalAmount
                        };
                        procedureRevenueList.Add(procedureRevenue);
                    }
                }
            }

            return procedureRevenueList;
        }
        // Inner class private method to get procedure price
        private async Task<decimal> GetProcedurePriceById(int procedureId)
        {
            var procedure = await _context.Procedures
                .Where(p => p.ProcedureID == procedureId)
                .FirstOrDefaultAsync();

            return (decimal)procedure.Price;
        }
        private async Task<string> GetProcedureNameById(int procedureId)
        {
            var procedure = await _context.Procedures
                .Where(p => p.ProcedureID == procedureId)
                .FirstOrDefaultAsync();
            var name = procedure.ProcedureName.ToString();

            return name;
        }



    }
}
