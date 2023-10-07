using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.DTOs.MedicalRecordDTO;
using DentalClinic.Services.EmployeeService;
using DentalClinic.Services.MedicalRecordService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : Controller
    {
        private readonly IMedicalRecordService _recordService;
        public MedicalRecordController(IMedicalRecordService recordService)
        {
            _recordService = recordService;
        }
        //Add a new Employee 
        [HttpPost]
        public async Task<ActionResult> AddMedicalRecord(AddMedicalRecordDTO medicalRecordDTO)
        {
            try
            {
                
                return Ok(await _recordService.AddMedicalRecord(medicalRecordDTO));
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
        [HttpGet("GetMedicalRecordForPatient")]
        public async Task<ActionResult> GetMedicalRecordForPatient(int patientID)
        {
            try
            {
                return Ok(await _recordService.GetMedicalRecordById(patientID));
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
        [HttpGet("GetMedicalRecords")]
        public async Task<ActionResult> GetMedicalRecords()
        {
            try
            {
                return Ok(await _recordService.GetAllMedicalRecords());
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));

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
        private ActionResult ParseException(Exception ex)
        {
            throw new NotImplementedException();
        }

    }
}
