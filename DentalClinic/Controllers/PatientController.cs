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
                await _patientService.AddPatient(patientDTO);
                return Ok("Registration successful.");
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Patient.");
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeletePatient(int ID)
        {
            try
            {
                return Ok(await _patientService.DeletePatient(ID));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting.");
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePatient(UpdatePatientDTO patientDTO)
        {
            try
            {
                return Ok(await _patientService.UpdatePatient(patientDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the employee.");
            }
        }
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult> GetAllPatients()
        {
            try
            {
                return Ok(await _patientService.GetAllPatients());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning all the employees.");
            }
        }
        [HttpGet("GetSpecificPatient")]
        public async Task<ActionResult> GetSpecificPatient(int ID)
        {
            try
            {
                return Ok(await _patientService.GetSpecificPatient(ID));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning the patient.");
            }
        }
    }
}
