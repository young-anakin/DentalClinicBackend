namespace DentalClinic.DTOs
{
    public class AddPrescriptionDTO
    {
        public string? DrugName { get; set; }
        public string? Strength { get; set; }
        public string? DosageFromDoseFrequency { get; set; }
        public string? DurationQuantity { get; set; }
        public string? HowToUse { get; set; }
        public string? OtherInformation { get; set; }

        public Decimal? TotalPrice { get; set; }

        public string? Registrations { get; set; }

        public string? Qualification { get; set; }

        // Optional fields for relationships
        public int PatientId { get; set; }
        public string PatientFullName { get; set; } = string.Empty; // Assuming you want to display patient's full name
        public int EmployeeId { get; set; }
        public string PrescriberName { get; set; } = string.Empty; // Assuming you want to display employee's (prescriber) name
    }
}
