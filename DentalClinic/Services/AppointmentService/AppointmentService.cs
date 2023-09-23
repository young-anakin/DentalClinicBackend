using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.EmployeeDTO;
using DentalClinic.Models;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public AppointmentService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }

        public async Task AddAppointment(AddAppointmentDTO appointmentDTO)
        {
            //var appointment = _mapper.Map<Appointment>(appointmentDTO);
            var patient = await _context.Patients.Where(a => a.PatientId == appointmentDTO.PatientID).FirstOrDefaultAsync();
            var Dentist = await _context.Employees.Where(e => e.EmployeeId == appointmentDTO.DentistID).FirstOrDefaultAsync();
            var ActionBY = await _context.Employees.Where(e => e.EmployeeId == appointmentDTO.ActionByID).FirstOrDefaultAsync();
            if (patient == null)
            {
                throw new ApplicationException($"Patient '{appointmentDTO.PatientID}' not found.");
            }
            var appointment = new Appointment
            {
                AppointmentDate = appointmentDTO.AppointmentDate,
                ActionName = appointmentDTO.ActionName,

            };
            appointment.Patient = patient;
            appointment.ActionBy = ActionBY;
            appointment.Dentist = Dentist;

            await _context.Appointments.AddAsync(appointment);

            await _context.SaveChangesAsync();

        }
        public async Task<List<Appointment>> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                                        .OrderByDescending(appointment => appointment.AppointmentDate)
                                        .ToListAsync();

            return appointments;
        }
        public async Task<List<Appointment>> GetAppointmentByEmployee(int EmployeeID)
        {
            var appointments = await _context.Appointments
                .Where(appointment => appointment.ActionByID == EmployeeID)
                .OrderByDescending(appointment => appointment.AppointmentDate)
                .ToListAsync();

            return appointments;
        }

    }
}
