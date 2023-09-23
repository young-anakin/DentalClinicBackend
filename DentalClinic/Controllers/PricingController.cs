using DentalClinic.DTOs.PatientDTO;
using DentalClinic.DTOs.Pricing;
using DentalClinic.Services.PricingService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : Controller
    {
        private readonly IPricingService _pricingService;
        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }
        [HttpPost("api/AddPricingReason")]
        public async Task<ActionResult> AddPricingReason(AddPricingReasonDTO pricingReasonDTO)
        {
            try
            {
                await _pricingService.AddPricingReason(pricingReasonDTO);
                return Ok("Registration successful.");
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the pricing reason.");
            }
        }
        [HttpPost("api/AddPricingDescription")]
        public async Task<ActionResult> AddPricingDescription(AddPricingDescriptionDTO descriptionDTO)
        {
            try
            {
                await _pricingService.AddPricingDescription(descriptionDTO);
                return Ok("Registration successful.");
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Pricing Description.");
            }
        }


    }
}
