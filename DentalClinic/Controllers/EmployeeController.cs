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
                return Ok(await _employeeService.AddEmployee(employeeDTO));
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while adding the employee.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
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
                var errorMessage = "An error occurred while returning the employee.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
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
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while returning the employees.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpGet("GetAllEMployeeWhoAreHired")]
        public async Task<ActionResult> GetWorkingEmployee()
        {
            try
            {
                return Ok(await _employeeService.GetAllHiredEmployee());
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while returning the employee.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
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
                var errorMessage = "An error occurred while returning the employee.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
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
                var errorMessage = "An error occurred while Deleting the employee.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
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
