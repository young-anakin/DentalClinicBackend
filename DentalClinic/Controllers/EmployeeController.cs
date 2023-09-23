using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.Models;
using DentalClinic.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        //Add a new Employee 
        [HttpPost]
        public async Task<ActionResult> AddEmployee(AddEmployeeDTO employeeDTO)
        {
            try
            {
                await _employeeService.AddEmployee(employeeDTO);
                return Ok("Registration successful.");
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the employee.");
            }
        }
        //Get total Number of Employees 
        [HttpGet("total")]
        public async Task<ActionResult> GetTotalEmployees()
        {
            try
            {
                int totalEmployees = await _employeeService.GetTotalEmployeeCountAsync();
                return Ok(totalEmployees);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // You can also return a meaningful error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the total employee count.");
            }
        }
        //Get all the employees 
        [HttpGet("GetAllEMployee")]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok( await _employeeService.GetAllEmployee());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning all the employees.");
            }
        }
        [HttpGet("GetAllEMployeeWhoAreHired")]
        public async Task<ActionResult> GetWorkingEmployee()
        {
            try
            {
                return Ok(await _employeeService.GetAllHiredEmployee());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning all the employees.");
            }
        }
        //Get a specific employee by ID
        [HttpGet("GetSpecificEmployee")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            try
            {
                return Ok(await _employeeService.GetEmployeeById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while returning employee with id {id}");
            }
        }
        //Update employee information


        //Delete an employee
        [HttpDelete("Employee")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                return Ok(await _employeeService.DeleteEmployee(id));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting an employee.");
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeDTO employeeDTO)
        {
            try
            {
                return Ok(await _employeeService.UpdateEmployee(employeeDTO));

            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while Updating the employee.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }


        private ActionResult ParseException(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
