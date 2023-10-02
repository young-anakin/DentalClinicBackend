using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.Models;
using DentalClinic.Services.AppointmentService;
using DentalClinic.Services.EmployeeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        //add a new employee
        [HttpPost]
        public async Task<ActionResult> setappointmnet(AddAppointmentDTO appointmentdto)
        {
            try
            {

                return Ok(await _appointmentService.AddAppointment(appointmentdto));
            }
            catch (Exception ex)
            {
                var errormessage = "an error occurred while adding the appointment.";

                if (ex.InnerException != null)
                {
                    errormessage += $" inner exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errormessage);
            }
        }
        [HttpGet("GetAllAppointmentsforEmployees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            try
            {
                return Ok(await _appointmentService.GetAllAppointments());

            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the Appointment.");
            }
        }
        [HttpGet("GetAppointmentforSpecificEmployee")]
        public async Task<ActionResult> GetAppforSpecificEmp(int empID)
        {
            try
            {
                return Ok(await _appointmentService.GetAppointmentByEmployee(empID));

            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the Appointment.");
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAppointment (int appointmentID)
        {
            try
            {
                return Ok(await _appointmentService.DeleteAppointment(appointmentID));
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while Deleting the Appointment.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAppointment(UpdateAppointmentDTO appointmentDTO)
        {
            try
            {

                return Ok(await _appointmentService.UpdateAppointment(appointmentDTO));
            }   
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while Updating the Appointment.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
    }
}
