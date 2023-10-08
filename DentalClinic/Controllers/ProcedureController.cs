using DentalClinic.DTOs.Pricing;
using DentalClinic.DTOs.ProcedureDTO;
using DentalClinic.Services.PricingService;
using DentalClinic.Services.ProcedureService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : Controller
    {
        private readonly IProcedureService _procedureService;
        public ProcedureController(IProcedureService procedureService)
        {
            _procedureService = procedureService;
        }
        [HttpPost("AddProcedure")]
        public async Task<ActionResult> AddProcedure(AddProcedureDTO procedureDTO)
        {
            try
            {
               
                return Ok(await _procedureService.AddProcedure(procedureDTO));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Patient/Dentist/ActionBy Not Found
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Appointment start time in the past
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Dentist or ActionBy already has an appointment
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> ReturnProcedures()
        {
            try
            {

                return Ok(await _procedureService.GetProcedures());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Patient/Dentist/ActionBy Not Found
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Appointment start time in the past
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Dentist or ActionBy already has an appointment
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProcedures(string procedure)
        {
            try
            {
                return Ok(await _procedureService.DeleteProcedure(procedure));

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Patient/Dentist/ActionBy Not Found
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Appointment start time in the past
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Dentist or ActionBy already has an appointment
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProcedures(UpdateProcedureDTO procedureDTO)
        {
            try
            {
                return Ok(await _procedureService.UpdateProcedure(procedureDTO));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Patient/Dentist/ActionBy Not Found
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Appointment start time in the past
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Dentist or ActionBy already has an appointment
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
