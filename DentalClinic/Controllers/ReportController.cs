using DentalClinic.DTOs.ProcedureDTO;
using DentalClinic.Models;
using DentalClinic.Services.ProcedureService;
using DentalClinic.Services.ReportService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("GenderedReportBySubCity")]
        public async Task<ActionResult> GenderedReportBySubCity()
        {
            try
            {

                return Ok(await _reportService.GenderBySubCity());
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

    }
}
