using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.PatientDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.Tools
{
    public class ToolsService : IToolsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ToolsService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string[] ReturnArrayofCommaSeparatedStrings(string inputString)
        {
            string[] strings = {

        };
            if (string.IsNullOrEmpty(inputString))
            {
                return strings;
            }

            string[] separatedStrings = inputString.Split(',');
            return separatedStrings;
        }
        public int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - birthDate.Year;

            if (currentDate.Month < birthDate.Month ||
                (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }
    }

}

