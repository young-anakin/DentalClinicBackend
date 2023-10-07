using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Services.AppointmentService;
using DentalClinic.Services.CompanySettingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class CompanySettingController : Controller
    {
        private readonly ICompanySettingService _companySettingService;
        public CompanySettingController(ICompanySettingService companySettingService)
        {
            _companySettingService = companySettingService;
        }
        //Add Company Setting
        [HttpPost]
        public async Task<ActionResult> SetCompanySetting(AddCompanySettingsDTO companySettingsDTO)
        {
            try
            {

                return Ok(await _companySettingService.AddCompanySetting(companySettingsDTO));
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
        //Update Company setting
        [HttpPut]
        public async Task<ActionResult> UpdateCompanySetting(UpdateCompanySettingDTO b)
        {
            try
            {

                return Ok(await _companySettingService.UpdateComapnySetting(b));
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
    //Return Company Setting
        [HttpGet("RecentCompanySetting")]
        public async Task<ActionResult> GetCompanySetting()
        {
            try
            {

                return Ok(await _companySettingService.GetCompanySetting());
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


    }
}
