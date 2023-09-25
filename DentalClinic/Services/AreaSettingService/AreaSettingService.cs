using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.AreaSettingDTO;
using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.AreaSettingService
{
    public class AreaSettingService
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
            var country = _mapper.Map<Country>(countryDTO);
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }
        public async Task<Country> RemoveCountry(string CN)
        {
            var country = await _context.Countries
                                        .Where(c=>c.CountryName == CN)
                                        .FirstOrDefaultAsync()??throw new KeyNotFoundException("Country Not Found!");
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return country;
        }
        public async Task<City> SetCity(AddCityDTO cityDTO)
        {
            var CountryVar = await _context.Countries
                                        .Where(C => C.CountryName == cityDTO.CountryName)
                                        .FirstOrDefaultAsync() ?? throw new KeyNotFoundException();
            var city = new City
            {
                CityName = cityDTO.City
            };
            city.Country = CountryVar;
            return city;
        }
        public async Task<City> RemoveCity(string cityName)
        {
            var city = await _context.Cities
                            .Where(c=> c.CityName == cityName)
                            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Country Not Found!");
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return city;
        }
        //public async Task<Country> SetSubCity(AddCountryDTO countryDTO)
        //{

        //}
        //public async Task<Country> RemoveSubCity(string countryName)
        //{

        //}
    }
}
