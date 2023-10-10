using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.CreditDTO;
using DentalClinic.Models;
using DentalClinic.Services.AppointmentService;
using DentalClinic.Services.CreditService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    public class CreditController : Controller
    {
        private readonly ICreditService _creditService;
        public CreditController(ICreditService creditService)
        {
            _creditService = creditService;
        }
        //add new credit
        [HttpPost]
        public async Task<ActionResult> ChargeCredit(ChargeCreditDTO chargeCreditDTO)
        {
            try
            {
                return Ok(await _creditService.ChargeCredit(chargeCreditDTO));
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
