using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task AddAppointment(AddAppointmentDTO appointmentDTO);
        Task<Appointment> DeleteAppointment(int AppID);
        Task<List<Appointment>> GetAllAppointments();
        Task<List<Appointment>> GetAppointmentByEmployee(int EmployeeID);
        Task<Appointment> UpdateAppointment(UpdateAppointmentDTO appointmentDTO);
    }
}