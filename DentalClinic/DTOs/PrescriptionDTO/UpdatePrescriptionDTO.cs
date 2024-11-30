namespace DentalClinic.DTOs
{
    public class UpdatePrescriptionDTO
    {
        public int Id { get; set; } // The ID is required for updates

        // Optional fields for updating prescription details
        public string? DrugName { get; set; }
        public string? Strength { get; set; }
        public string? DosageFromDoseFrequency { get; set; }
        public string? DurationQuantity { get; set; }
        public string? HowToUse { get; set; }
        public string? OtherInformation { get; set; }

        public Decimal? TotalPrice { get; set; }
        public string? Registrations { get; set; }
        public string? Qualification { get; set; }

        // Optional fields for relationships (if updating them is allowed)
        public int? PatientId { get; set; } // Nullable, in case the patient association might change
        public int? EmployeeId { get; set; } // Nullable, in case the prescriber (employee) association might change
    }
}
