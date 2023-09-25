using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }  = string.Empty;

        public List<City>? CityList { get; set; }  


    }
}
