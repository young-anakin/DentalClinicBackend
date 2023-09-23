using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.Models;
using DentalClinic.Services.AppointmentService;
using DentalClinic.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        //Add a new Employee 
        [HttpPost]
        public async Task<ActionResult> SetAppointmnet(AddAppointmentDTO appointmentDTO)
        {
            try
            {
                await _appointmentService.AddAppointment(appointmentDTO);
                return Ok("Registration successful.");
            }
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
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            try
            {
                return Ok(await _appointmentService.GetAllAppointments());

            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the Health Progress.");
            }
        }
        [HttpGet("GetSpecificEmployeeApp")]
        public async Task<ActionResult> GetAppforSpecificEmp(int empID)
        {
            try
            {
                return Ok(await _appointmentService.GetAppointmentByEmployee(empID));

            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the Health Progress.");
            }
        }


    }
}
