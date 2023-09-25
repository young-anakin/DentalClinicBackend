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
    }
}

