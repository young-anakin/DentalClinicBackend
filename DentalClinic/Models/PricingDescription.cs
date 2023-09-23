using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class PricingDescription
    {
        [Key]
        public int PricingDescriptionId { get; set; }
        public string pricingDescription { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Procedure> Procedures { get; set; }
    }
}
