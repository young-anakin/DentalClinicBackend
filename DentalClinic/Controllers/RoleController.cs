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
            catch (Exception ex)
            {
                return this.ParseException(ex);
            }
        }
        [HttpDelete("{Name}")]

        public async Task<ActionResult> DeleteRole(string Name)
        {
            try
            {
                return Ok(await _roleService.DeleteRole(Name));
            }
            catch (Exception ex)
            {
                return this.ParseException(ex);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                return Ok(await _roleService.GetRoles());
            }
            catch(Exception ex)
            {
                return this.ParseException(ex);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(UpdateRoleDTO roleDTO)
        {
            try
            {
                return Ok(await _roleService.UpdateRole(roleDTO));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred."); // Generic error response for other exceptions.
            }
        }

        private ActionResult ParseException(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
