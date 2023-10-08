using DentalClinic.DTOs.AreaSettingDTO;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Services.AreaSettingService;
using DentalClinic.Services.CompanySettingService;
using DentalClinic.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AreaSettingController : Controller
    {


        private readonly IAreaSettingService _areaSettingService;
        private readonly UserService _userService;
        public AreaSettingController(IAreaSettingService areaSettingService /*DentalClinic.Services.User.UserService userService*/ )
        {
            _areaSettingService = areaSettingService;
            //_userService = userService;
        }
        [HttpPost("Country")]
        public async Task<ActionResult> SetCountry(AddCountryDTO countryDTO)
        {
            try
            {
                       return Ok(await _areaSettingService.SetCountry(countryDTO));
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
        [HttpPost("City")]
        public async Task<ActionResult> SetCity(AddCityDTO cityDTO)
        {
            try
            {
                    return Ok(await _areaSettingService.SetCity(cityDTO));
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
        [HttpPost("SubCity")]
        public async Task<ActionResult> SetSubCity(AddSubCityDTO subCityDTO)
        {
            try
            {

                return Ok(await _areaSettingService.SetSubCity(subCityDTO));
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
        [HttpDelete("Country")]
        public async Task<ActionResult> RemoveCountry(int CI)
        {
            try
            {

                return Ok(await _areaSettingService.RemoveCountry(CI));
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
        [HttpDelete("City")]
        public async Task<ActionResult> RemoveCity(int CI)
        {
            try
            {

                return Ok(await _areaSettingService.RemoveCity(CI));
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
        [HttpDelete("SubCity")]
        public async Task<ActionResult> RemoveSubCity(int SI)
        {
            try
            {

                return Ok(await _areaSettingService.RemoveSubCity(SI));
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
        [HttpGet("Country")]
        public async Task<ActionResult> GetCountries()
        {
            try
            {

                return Ok(await _areaSettingService.GetCountries());
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
        [HttpGet("City")]
        public async Task<ActionResult> GetCities(string CountryName)
        {
            try
            {

                return Ok(await _areaSettingService.GetCities(CountryName));
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
        [HttpGet("SubCity")]
        public async Task<ActionResult> GetSubCities(String cityName)
        {
            try
            {
                return Ok(await _areaSettingService.GetCities(cityName));
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
