using DentalClinic.DTOs.Pricing;
using DentalClinic.Models;

namespace DentalClinic.Services.PricingService
{
    public interface IPricingService
    {
        Task<PricingDescription> AddPricingDescription(AddPricingDescriptionDTO pricingDescriptionDTO);
        Task<PricingReason> AddPricingReason(AddPricingReasonDTO pricingReasonDTO);
    }
}