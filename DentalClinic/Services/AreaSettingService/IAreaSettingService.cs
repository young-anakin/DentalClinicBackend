﻿using DentalClinic.DTOs.AreaSettingDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.AreaSettingService
{
    public interface IAreaSettingService
    {
        Task<List<City>> GetCities(string CountryName);
        Task<List<Country>> GetCountries();
        Task<List<SubCity>> GetSubCities(string cityName);
        Task<City> RemoveCity(int cityName);
        Task<Country> RemoveCountry(int CN);
        Task<SubCity> RemoveSubCity(int subCityName);
        Task<City> SetCity(AddCityDTO cityDTO);
        Task<Country> SetCountry(AddCountryDTO countryDTO);
        Task<SubCity> SetSubCity(AddSubCityDTO subCityDTO);
    }
}