using DentalClinic.DTOs.PatientDTO;
using DentalClinic.Services.PatientService;
using DentalClinic.Services.PaymentTypeService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;
        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }
        [HttpPost]
        public async Task<ActionResult> AddPaymentType(string str)
        {
            try
            {

                return Ok(await _paymentTypeService.AddPaymentType(str));
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
        [HttpGet]
        public async Task<ActionResult> GetPaymentTypes()
        {
            try
            {

                return Ok(await _paymentTypeService.GetAllPaymentTypes());
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
        [HttpDelete]
        public async Task<ActionResult> DeletePaymentTypes(int id)
        {
            try
            {

                return Ok(await _paymentTypeService.RemovePaymentType(id));
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
