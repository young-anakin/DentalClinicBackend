namespace DentalClinic.DTOs.AppointmentDTO
{
    public class UpdateAppointmentDTO
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int? DentistID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int? ActionByID { get; set; }
        public string ActionName { get; set; } = string.Empty;
    }
}
