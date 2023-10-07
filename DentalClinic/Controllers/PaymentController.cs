using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.PaymentDTO;
using DentalClinic.Services.AppointmentService;
using DentalClinic.Services.PaymentService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _patientService;
        public PaymentController(IPaymentService patientService)
        {
            _patientService = patientService;
        }
        //add a new employee
        [HttpPost]
        public async Task<ActionResult> MakePayment(MakePaymentMedRecDTO DTO)
        {
            try
            {
                return Ok(await _patientService.AddPaymentfromMedicalRecord(DTO));
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
        public async Task<ActionResult> displayID(int id)
        {
            try
            {
                return Ok(await _patientService.DisplayPaymentReceipt(id));
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
