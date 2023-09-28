﻿using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.AreaSettingDTO;
using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.AreaSettingService
{
    public class AreaSettingService : IAreaSettingService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AreaSettingService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Country> SetCountry(AddCountryDTO countryDTO)
        {
            var country = new Country
            {
                CountryName = countryDTO.Country
            };
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }
        public async Task<Country> RemoveCountry(int CN)
        {
            var country = await _context.Countries
                                        .Where(c => c.CountryID == CN)
                                        .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Country Not Found!");
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return country;
        }
        public async Task<List<Country>> GetCountries()
        {
            var LiofCountries = await _context.Countries
                                        .OrderByDescending(c => c.CountryName)
                                        .ToListAsync();
            return LiofCountries;
        }
        public async Task<City> SetCity(AddCityDTO cityDTO)
        {

            var city = new City
            {
                CityName = cityDTO.City
            };
            return city;
        }
        public async Task<List<City>> GetCities(string CountryName)
        {
            var Cities = await _context.Cities
                                        .OrderByDescending(c => c.CityName)
                                        .ToListAsync() ?? throw new KeyNotFoundException("Country Not FOund");
            return Cities;
        }
        public async Task<City> RemoveCity(int cityID)
        {
            var city = await _context.Cities
                            .Where(c => c.CityId == cityID)
                            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Country Not Found!");
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return city;
        }
        public async Task<SubCity> SetSubCity(AddSubCityDTO subCityDTO)
        {

            var subCity = new SubCity
            {
                SubCityName = subCityDTO.SubCity,
            };
            await _context.SubCities.AddAsync(subCity);
            await _context.SaveChangesAsync();
            return subCity;

        }
        public async Task<SubCity> RemoveSubCity(int subCityID)
        {
            var city = await _context.SubCities
                .Where(c => c.SubCityID == subCityID)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Country Not Found!");
            _context.SubCities.Remove(city);
            await _context.SaveChangesAsync();
            return city;

        }
        public async Task<List<SubCity>> GetSubCities(string cityName)
        {
            var subcity = await _context.SubCities
                                        .OrderByDescending(c => c.SubCityName)
                                        .ToListAsync();
            return subcity;

        }


    }
}