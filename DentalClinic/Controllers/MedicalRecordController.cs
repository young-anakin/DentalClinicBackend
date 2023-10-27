using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.DTOs.MedicalRecordDTO;
using DentalClinic.Models;
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
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
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
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
            }

        }
        [HttpPut]
        public async Task<ActionResult> UpdateMedicalRecord(UpdateMedicalRecordDTO DTO)
        {
            return Ok(await _recordService.UpdateMedicalRecord(DTO));

            try
            {
                return Ok(await _recordService.UpdateMedicalRecord(DTO));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
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
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
            }

        }
        private ActionResult ParseException(Exception ex)
        {
            throw new NotImplementedException();
        }

    }
}
