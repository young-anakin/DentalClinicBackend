using DentalClinic.DTOs.HealthProgressDTO;
using DentalClinic.Services.HealthProgressService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthProgresssController : ControllerBase
    {
        private readonly IHealthProgressService _healthProgressService;
        public HealthProgresssController(IHealthProgressService healthProgressService)
        {
            _healthProgressService = healthProgressService;
        }
        //Add a new Employee 
        [HttpPost]
        public async Task<ActionResult> AddHealthProgress(AddHealthProgressDTO healthProgressDTO)
        {
            try
            {
                await _healthProgressService.AddHealthProgressToEmployee(healthProgressDTO);
                return Ok("Registration successful.");
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while adding the Health Progress.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }

        [HttpGet("GetProgressForPatient")]
        public async Task<ActionResult> GetHealthProgresses(int  Patientid)
        {
            try
            {
                return Ok(await _healthProgressService.GetHealthProgressesForPatient(Patientid));
                
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the Health Progress.");
            }

        }
        [HttpGet("GetProgressByEmployee")]
        public async Task<ActionResult> GetHealthProgressByEmplpoyee(int employeeID)
        {
            try
            {
                return Ok(await _healthProgressService.GetHealthProgressesAdministeredByEmployee(employeeID));

            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the Health Progress.");
            }

        }
    }
}
