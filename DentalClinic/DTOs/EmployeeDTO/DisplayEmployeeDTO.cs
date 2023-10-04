namespace DentalClinic.DTOs.EmployeeDTO
{
    public class DisplayEmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; } = string.Empty;

        public bool isActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
