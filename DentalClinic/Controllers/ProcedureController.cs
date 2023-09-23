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
        [HttpPost("api/AddProcedure")]
        public async Task<ActionResult> AddProcedure(AddProcedureDTO procedureDTO)
        {
            try
            {
                await _procedureService.AddProcedure(procedureDTO);
                return Ok("Procedure Succesfully Added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Procedure.");
            }
        }
        [HttpGet]
        public async Task<ActionResult> ReturnProcedures()
        {
            try
            {

                return Ok(await _procedureService.GetProcedures());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while Fetching the Procedures.");
            }

        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProcedures(string procedure)
        {
            try
            {
                await _procedureService.DeleteProcedure(procedure);
                return Ok("Procedure Succesfully Deleted");

            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while updating the Procedure.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProcedures(UpdateProcedureDTO procedureDTO)
        {
            try
            {
                return Ok(await _procedureService.UpdateProcedure(procedureDTO));
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while updating the Procedure.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
    }
}
