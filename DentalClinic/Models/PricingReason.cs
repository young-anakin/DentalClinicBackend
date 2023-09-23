using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class PricingReason
    {
        [Key]
        public int PricingReasonID { get; set; }  
        public string PricingReasonName { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Procedure> Procedures { get; set; }
    }
}
