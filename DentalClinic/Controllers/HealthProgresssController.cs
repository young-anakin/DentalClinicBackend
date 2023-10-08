using DentalClinic.DTOs.HealthProgressDTO;
using DentalClinic.Models;
using DentalClinic.Services.HealthProgressService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

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
               
                return Ok(await _healthProgressService.AddHealthProgressToEmployee(healthProgressDTO));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
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
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
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
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
            }

        }
    }
}
