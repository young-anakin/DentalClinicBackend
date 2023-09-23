using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.PatientDTO;
using DentalClinic.DTOs.Pricing;
using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.PricingService
{
    public class PricingService : IPricingService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PricingService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PricingReason> AddPricingReason(AddPricingReasonDTO pricingReasonDTO)
        {
            var re = _mapper.Map<PricingReason>(pricingReasonDTO);
            _context.pricingReasons.Add(re);
            await _context.SaveChangesAsync();
            return re;

        }
        public async Task<PricingDescription> AddPricingDescription(AddPricingDescriptionDTO pricingDescriptionDTO)
        {
            var re = _mapper.Map<PricingDescription>(pricingDescriptionDTO);
            _context.pricingDescriptions.Add(re);
            await _context.SaveChangesAsync();
            return re;

        }
    }
}
