using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Models
{
    public class SubCity
    {
        public int SubCityID { get; set; }

        public string SubCityName { get; set;} = String.Empty;

        public City? City { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }


    }
}
