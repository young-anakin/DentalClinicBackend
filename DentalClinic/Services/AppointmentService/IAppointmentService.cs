using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task AddAppointment(AddAppointmentDTO appointmentDTO);
        Task<List<Appointment>> GetAllAppointments();
        Task<List<Appointment>> GetAppointmentByEmployee(int EmployeeID);
    }
}