using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.DTOs.PatientDTO;
using DentalClinic.Services.EmployeeService;
using DentalClinic.Services.PatientService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpPost]
        public async Task<ActionResult> AddPatient(AddPatientDTO patientDTO)
        {
            try
            {
                
                return Ok(await _patientService.AddPatient(patientDTO));
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
        public async Task<ActionResult> DeletePatient(int ID)
        {
            try
            {
                return Ok(await _patientService.DeletePatient(ID));
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
        public async Task<ActionResult> UpdatePatient(UpdatePatientDTO patientDTO)
        {
            try
            {
                return Ok(await _patientService.UpdatePatient(patientDTO));
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
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult> GetAllPatients()
        {
            try
            {
                return Ok(await _patientService.GetAllPatients());
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
        [HttpGet("GetSpecificPatient")]
        public async Task<ActionResult> GetSpecificPatient(int ID)
        {
            try
            {
                return Ok(await _patientService.GetSpecificPatient(ID));
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
