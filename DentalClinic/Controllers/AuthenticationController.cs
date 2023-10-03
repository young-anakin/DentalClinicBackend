using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.DTOs.LogInDTO;
using DentalClinic.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public AuthenticationController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody]LoginDTO login)
        {
            try
            {
                return Ok(await _employeeService.Login(login));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // User Name Not found
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); // Invalid password
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Handle other exceptions
            }
        }
    }
}
