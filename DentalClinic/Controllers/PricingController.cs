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
        [HttpPost("AddPricingReason")]
        public async Task<ActionResult> AddPricingReason(AddPricingReasonDTO pricingReasonDTO)
        {
            try
            {
               
                return Ok(await _pricingService.AddPricingReason(pricingReasonDTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the pricing reason.");
            }
        }

        [HttpPost("AddPricingDescription")]
        public async Task<ActionResult> AddPricingDescription(AddPricingDescriptionDTO descriptionDTO)
        {
            try
            {
                
                return Ok(await _pricingService.AddPricingDescription(descriptionDTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Pricing Description.");
            }
        }
        [HttpGet("Pricing Reasons")]
        public async Task<ActionResult> GetPricingReasons()
        {
            try
            {
                
                return Ok(await _pricingService.GetPricingReasonsList());
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning the Pricing Reasons.");
            }
        }
        [HttpGet("Pricing Description")]
        public async Task<ActionResult> GetPricingDescription()
        {
            try
            {
                
                return Ok(await _pricingService.GetPricingDescriptions());
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning the Pricing Description.");
            }
        }


    }
}
