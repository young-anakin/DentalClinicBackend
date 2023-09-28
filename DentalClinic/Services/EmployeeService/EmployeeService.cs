using AutoMapper;
using DentalClinic.Context;
using DentalClinic.Models;
using DentalClinic.DTOs.EmployeeDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EmployeeService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Employee> AddEmployee(AddEmployeeDTO employeeDTO)
        {
            //var employee = _mapper.Map<Employee>(employeeDTO);

            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == employeeDTO.RoleName);

            if (role == null)
            {
                // Handle the case where the role does not exist
                throw new ApplicationException($"Role '{employeeDTO.RoleName}' not found.");
            }


            var userAccount = new UserAccount
            {
                UserName = employeeDTO.UserName,
                Password = employeeDTO.Password,
                Role = role
            };

            var employee = new Employee
            {
                EmployeeName = employeeDTO.EmployeeName,
                Email = employeeDTO.Email,
                Phone = employeeDTO.Phone,
                DateOfBirth = employeeDTO.DateOfBirth,
                EmployeeGender = employeeDTO.EmployeeGender,
                CreatedAt = employeeDTO.CreatedAt,
            };
            employee.UserAccount = userAccount;
            await _context.Employees.AddAsync(employee);



            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<int> GetTotalEmployeeCountAsync()
        {
            int totalCount = await _context.Employees.CountAsync();
            return totalCount;
        }
        public async Task<List<Employee>> GetAllHiredEmployee()
        {
            var employees = await _context.Employees
                                    .Where(e=> e.IsCurrentlyActive == true)
                                    .Include(e=>e.UserAccount)
                                        .ThenInclude(ua=> ua.Role)
                                    .OrderByDescending(e => e.EmployeeId)
                                   .ToListAsync();
                                
            return employees;
                             
        }
        public async Task<List<Employee>> GetAllEmployee()
        {
            var employees = await _context.Employees
                                    .Include(e => e.UserAccount)
                                        .ThenInclude(ua => ua.Role)
                                    .OrderByDescending(e => e.EmployeeId)
                                   .ToListAsync();

            return employees;

        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            var employee = await _context.Employees
                                   .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Emp not found");
            }
            employee.IsCurrentlyActive = false;
            employee.DateOfTermination = DateTime.Now;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                            .Include(e => e.UserAccount)
                                   .ThenInclude(ua => ua.Role)
                            .FirstOrDefaultAsync(e => e.EmployeeId == id && e.IsCurrentlyActive == true)
                            ?? throw new KeyNotFoundException("Emp not found");
            return employee;

        }
        public async Task<Employee> UpdateEmployee(UpdateEmployeeDTO employeeDTO)
        {
            var employee = await _context.Employees
                                    .Where(e => e.EmployeeId == employeeDTO.EmployeeID)
                                    .FirstOrDefaultAsync()??throw new KeyNotFoundException("Employee Not Found");
                                                //var employee = _mapper.Map<Employee>(employeeDTO);

            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == employeeDTO.RoleName);
            employee.UpdateAt = employeeDTO.UpdatedAt;

            if (role == null)
            {
                // Handle the case where the role does not exist
                throw new ApplicationException($"Role '{employeeDTO.RoleName}' not found.");
            }
            employee = _mapper.Map(employeeDTO, employee);
            var UserAccount = await _context.UserAccounts
                                .Where(ua => ua.UserAccountId == employeeDTO.EmployeeID)
                                .FirstOrDefaultAsync()??throw new KeyNotFoundException("User Account Not found!");
            UserAccount.UserName = employeeDTO.UserName1;
            UserAccount.Password = employeeDTO.Password;
            UserAccount.Role = role;
            employee.IsCurrentlyActive = employeeDTO.IsCurrentlyActive;
            employee.UserAccount = UserAccount;



             _context.Employees.Update(employee);

            await _context.SaveChangesAsync();
            return employee;
        }




    }
}
