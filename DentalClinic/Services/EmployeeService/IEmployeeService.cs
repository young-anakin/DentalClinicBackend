using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.DTOs.LogInDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(AddEmployeeDTO employeeDTO);
        Task<Employee> DeleteEmployee(int id);
        Task<int> GetTotalEmployeeCountAsync();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> UpdateEmployee(UpdateEmployeeDTO employeeDTO);
        Task<List<Employee>> GetAllEmployee();
        Task<List<Employee>> GetAllHiredEmployee();
        Task<string> Login(LoginDTO login);
        Task<string> RestorePassword(int User_id);
    }
}