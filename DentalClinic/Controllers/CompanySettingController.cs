using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Services.AppointmentService;
using DentalClinic.Services.CompanySettingService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanySettingController : Controller
    {
        private readonly ICompanySettingService _companySettingService;
        public CompanySettingController(ICompanySettingService companySettingService)
        {
            _companySettingService = companySettingService;
        }
        //Add a new Employee 
        [HttpPost]
        public async Task<ActionResult> SetCompanySetting(AddCompanySettingsDTO companySettingsDTO)
        {
            try
            {
                
                return Ok(await _companySettingService.AddCompanySettingService(companySettingsDTO));
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while adding the Setting.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCompanySetting(UpdateCompanySettingDTO companySettingsDTO)
        {
            try
            {

                return Ok(await _companySettingService.UpdateCompanySetting(companySettingsDTO));
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while updating the Setting.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpGet("RecentCompanySetting")]
        public async Task<ActionResult> GetCompanySetting()
        {
            try
            {

                return Ok(await _companySettingService.GetCompanySetting());
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while updating the Setting.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }


    }
}
