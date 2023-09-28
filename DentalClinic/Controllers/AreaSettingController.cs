using DentalClinic.DTOs.AreaSettingDTO;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Services.AreaSettingService;
using DentalClinic.Services.CompanySettingService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaSettingController : Controller
    {


        private readonly IAreaSettingService _areaSettingService;
        public AreaSettingController(IAreaSettingService areaSettingService)
        {
            _areaSettingService = areaSettingService;
        }
        [HttpPost("Country")]
        public async Task<ActionResult> SetCountry(AddCountryDTO countryDTO)
        {
            try
            {

                return Ok(await _areaSettingService.SetCountry(countryDTO));
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
        [HttpPost("City")]
        public async Task<ActionResult> SetCity(AddCityDTO cityDTO)
        {
            try
            {

                return Ok(await _areaSettingService.SetCity(cityDTO));
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
        [HttpPost("SubCity")]
        public async Task<ActionResult> SetSubCity(AddSubCityDTO subCityDTO)
        {
            try
            {

                return Ok(await _areaSettingService.SetSubCity(subCityDTO));
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
        [HttpDelete("Country")]
        public async Task<ActionResult> RemoveCountry(int CI)
        {
            try
            {

                return Ok(await _areaSettingService.RemoveCountry(CI));
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
        [HttpDelete("City")]
        public async Task<ActionResult> RemoveCity(int CI)
        {
            try
            {

                return Ok(await _areaSettingService.RemoveCity(CI));
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
        [HttpDelete("SubCity")]
        public async Task<ActionResult> RemoveSubCity(int SI)
        {
            try
            {

                return Ok(await _areaSettingService.RemoveSubCity(SI));
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
        [HttpGet("Country")]
        public async Task<ActionResult> GetCountries()
        {
            try
            {

                return Ok(await _areaSettingService.GetCountries());
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
    }

}
