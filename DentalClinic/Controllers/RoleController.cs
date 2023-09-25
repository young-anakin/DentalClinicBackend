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
                var errorMessage = "An error occurred while creating the role.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
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
                var errorMessage = "An error occurred while deleting the role.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                return Ok(await _roleService.GetRoles());
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while returning the role.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(UpdateRoleDTO roleDTO)
        {
            try
            {
                return Ok(await _roleService.UpdateRole(roleDTO));
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while Updating the role.";

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
