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
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while adding the Health Progress.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpGet("GetMedicalRecordForPatient")]
        public async Task<ActionResult> GetMedicalRecordForPatient(int patientID)
        {
            try
            {
                return Ok(await _recordService.GetMedicalRecordforPatient(patientID));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while adding the Health Progress.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }

        }
        private ActionResult ParseException(Exception ex)
        {
            throw new NotImplementedException();
        }

    }
}
