    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace DentalClinic.Models
    {
        public class Referal
        {
            [Key]
            public int ReferalID { get; set; }
            [ForeignKey("Employee")]
            public int ReferingDoctor { get; set; }
        

            [ForeignKey("MedicalRecord")]
            public int MedicalRecordeID { get; set; }
            public string ReferedDoctor { get; set; } = string.Empty;

            public DateTime? ReferalDate { get; set; } = DateTime.Now;

            public string ReasonForReferal { get; set; } = string.Empty;

            public Employee? Employee { get; set; }

            public MedicalRecord? MedicalRecord { get; set; }


        }
    }
