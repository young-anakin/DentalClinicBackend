    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace DentalClinic.Models
    {
        public class Referal
        {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public int ReferalID { get; set; }
        [ForeignKey("Employee")]
        [System.Text.Json.Serialization.JsonIgnore]
        public int ReferingDoctor { get; set; }
        [ForeignKey("MedicalRecord")]
        [System.Text.Json.Serialization.JsonIgnore]
        public int MedicalRecordeID { get; set; }
        public string ReferedDoctor { get; set; } = string.Empty;
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? ReferalDate { get; set; } = DateTime.Now;
        public string ReasonForReferal { get; set; } = string.Empty;
        [System.Text.Json.Serialization.JsonIgnore]
        public Employee? Employee { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public MedicalRecord? MedicalRecord { get; set; }


        }
    }
