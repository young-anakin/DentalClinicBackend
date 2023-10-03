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
        [HttpPut("PricingReason")]

        public async Task<ActionResult> UpdatePricingReason(UpdatePricingReasonDTO DTO)
        {
            try
            {

                return Ok(await _pricingService.UpdatePricingReason(DTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the Pricing reason.");
            }
        }
        [HttpPut("PricingDescription")]

        public async Task<ActionResult> UpdatePricingDescription(UpdatePricingDescriptionDTO DTO)
        {
            try
            {

                return Ok(await _pricingService.UpdatePricingDescription(DTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the Pricing Description.");
            }
        }
        [HttpDelete("PricingDescription")]
        public async Task<ActionResult> DeletePricingDescription(int id)
        {
            try
            {

                return Ok(await _pricingService.DeletePricingDescription(id));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the Pricing Description.");
            }
        }
        [HttpDelete("PricingReason")]
        public async Task<ActionResult> DeletePricingReason(int id)
        {
            try
            {

                return Ok(await _pricingService.DeletePricingReason(id));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the Pricing Reason.");
            }
        }

    }
}
