using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.DTOs.RoleDTO;
using DentalClinic.Services.EmployeeService;
using DentalClinic.Services.RoleService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost]
        public async Task<ActionResult> CreateRole(AddRoleDTO roleDTO)
        {
            try
            {
                return Ok(await _roleService.AddRole(roleDTO));
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
        [HttpDelete]

        public async Task<ActionResult> DeleteRole(string Name)
        {
            try
            {
                return Ok(await _roleService.DeleteRole(Name));
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
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                return Ok(await _roleService.GetRoles());
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
        public async Task<ActionResult> UpdateEmployee(UpdateRoleDTO roleDTO)
        {
            try
            {
                return Ok(await _roleService.UpdateRole(roleDTO));
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

        private ActionResult ParseException(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
