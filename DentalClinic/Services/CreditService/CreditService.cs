using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.SettingsDTO;
using DentalClinic.Models;
using DentalClinic.Services.CompanySettingService;

namespace DentalClinic.Services.CreditService
{
    public class CreditService
    {

            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public CreditService(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            //Update works by updating only 1 record that's in the database and changing it


        }
    }

