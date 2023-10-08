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

        [HttpPost("AddPricingDescription")]
        public async Task<ActionResult> AddPricingDescription(AddPricingDescriptionDTO descriptionDTO)
        {
            try
            {
                
                return Ok(await _pricingService.AddPricingDescription(descriptionDTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
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
        [HttpGet("Pricing Reasons")]
        public async Task<ActionResult> GetPricingReasons()
        {
            try
            {
                
                return Ok(await _pricingService.GetPricingReasonsList());
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
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
        [HttpGet("Pricing Description")]
        public async Task<ActionResult> GetPricingDescription()
        {
            try
            {
                
                return Ok(await _pricingService.GetPricingDescriptions());
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
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
        [HttpPut("PricingReason")]

        public async Task<ActionResult> UpdatePricingReason(UpdatePricingReasonDTO DTO)
        {
            try
            {

                return Ok(await _pricingService.UpdatePricingReason(DTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
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
        [HttpPut("PricingDescription")]

        public async Task<ActionResult> UpdatePricingDescription(UpdatePricingDescriptionDTO DTO)
        {
            try
            {

                return Ok(await _pricingService.UpdatePricingDescription(DTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
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
        [HttpDelete("PricingDescription")]
        public async Task<ActionResult> DeletePricingDescription(int id)
        {
            try
            {

                return Ok(await _pricingService.DeletePricingDescription(id));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
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
        [HttpDelete("PricingReason")]
        public async Task<ActionResult> DeletePricingReason(int id)
        {
            try
            {

                return Ok(await _pricingService.DeletePricingReason(id));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
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
