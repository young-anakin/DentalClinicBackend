using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.DTOs.RoleDTO;
using DentalClinic.Models;
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
        [HttpDelete]

        public async Task<ActionResult> DeleteRole(string Name)
        {
            try
            {
                return Ok(await _roleService.DeleteRole(Name));
            }
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
        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                return Ok(await _roleService.GetRoles());
            }
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
        public async Task<ActionResult> UpdateEmployee(UpdateRoleDTO roleDTO)
        {
            try
            {
                return Ok(await _roleService.UpdateRole(roleDTO));
            }
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
