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

        public async Task<Appointment> AddAppointment(AddAppointmentDTO appointmentDTO)
        {

            var patient = await _context.Patients
                .Where(a => a.PatientId == appointmentDTO.PatientID)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Patient Not Found");

            var Dentist = await _context.Employees
                .Where(e => e.EmployeeId == appointmentDTO.DentistID)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Dentist Not Found");

            var ActionBY = await _context.Employees
                .Where(e => e.EmployeeId == appointmentDTO.ActionByID)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("ActionBy Not Found");

            if (appointmentDTO.AppointmentStartTime < DateTime.Now)
            {
                throw new ArgumentException("Appointment start time cannot be in the past.");
            }
            if (appointmentDTO.AppointmentEndTime < appointmentDTO.AppointmentStartTime)
            {
                throw new ArgumentException("Appointment end time cannot be in the past of Appointment Start time.");

            }
            // Check if Dentist already has an appointment at the specified time
            bool dentistHasConflict = await _context.Appointments
                .AnyAsync(a => a.Dentist.EmployeeId == appointmentDTO.DentistID &&
                               a.AppointmentStartTime < appointmentDTO.AppointmentEndTime &&
                               a.AppointmentEndTime > appointmentDTO.AppointmentStartTime);

            if (dentistHasConflict)
            {
                throw new InvalidOperationException("Dentist already has an appointment at this time.");
            }

            // Check if ActionBy already has an appointment at the specified time
            //bool actionByHasConflict = await _context.Appointments
            //    .AnyAsync(a => a.ActionBy.EmployeeId == appointmentDTO.ActionByID &&
            //                   a.AppointmentStartTime < appointmentDTO.AppointmentEndTime &&
            //                   a.AppointmentEndTime > appointmentDTO.AppointmentStartTime);

            //if (actionByHasConflict)
            //{
            //    throw new InvalidOperationException("ActionBy already has an appointment at this time.");
            //}

            var appointment = new Appointment
            {
                AllDay = appointmentDTO.AllDay,
                AppointmentSetDate = DateTime.Now,
                AppointmentStartTime = appointmentDTO.AppointmentStartTime,
                AppointmentEndTime = appointmentDTO.AppointmentEndTime,
                ActionName = appointmentDTO.ActionName,
            };
            appointment.Patient = patient;
            appointment.ActionBy = ActionBY;
            appointment.Dentist = Dentist;

            await _context.Appointments.AddAsync(appointment);

            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                                        .OrderByDescending(appointment => appointment.AppointmentStartTime)
                                        .Select(appointment => new Appointment
                                        {
                                            AppointmentId = appointment.AppointmentId,
                                            AllDay = appointment.AllDay,
                                            AppointmentStartTime = appointment.AllDay
                                            ? appointment.AppointmentStartTime.Date
                                            : appointment.AppointmentStartTime,
                                            AppointmentEndTime = appointment.AllDay
                                            ? appointment.AppointmentEndTime.Date
                                            : appointment.AppointmentEndTime,
                                            ActionName = appointment.ActionName,
                                            PatientID = appointment.PatientID,
                                            DentistID = appointment.DentistID,
                                            AppointmentSetDate = appointment.AppointmentSetDate,
                                            ActionBy = appointment.ActionBy,

                                        })
                                        .ToListAsync();

            return appointments;
        }
        public async Task<List<Appointment>> GetAppointmentByEmployee(int EmployeeID)
        {
            var appointments = await _context.Appointments
                                        .Where(ap => ap.DentistID == EmployeeID)
                                        .OrderByDescending(ap => ap.AppointmentStartTime)
                                        .Select(appointment => new Appointment
                                        {
                                            AppointmentId = appointment.AppointmentId,
                                            AllDay = appointment.AllDay,
                                            AppointmentStartTime = appointment.AllDay
                                            ? appointment.AppointmentStartTime.Date
                                            : appointment.AppointmentStartTime,
                                            AppointmentEndTime = appointment.AllDay
                                            ? appointment.AppointmentEndTime.Date
                                            : appointment.AppointmentEndTime,
                                            ActionName = appointment.ActionName,
                                            PatientID = appointment.PatientID,
                                            DentistID = appointment.DentistID,
                                            AppointmentSetDate = appointment.AppointmentSetDate,
                                            ActionBy = appointment.ActionBy,

                                        })
                                        .ToListAsync();
            return appointments;
        }
        public async Task<List<Appointment>> GetAppointmentsForEmployee(int patientID)
        {
            var appointments = await _context.Appointments
                .Where(appointment => appointment.ActionByID == patientID)
                .OrderByDescending(appointment => appointment.AppointmentStartTime)
                .ToListAsync();

            return appointments;
        }
        public async Task<Appointment> DeleteAppointment(int AppID)
        {
            var appointment = await _context.Appointments
                                    .Where(ap=> ap.AppointmentId == AppID)
                                    .FirstOrDefaultAsync()?? throw new KeyNotFoundException("Appointment Not Found!");
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
        public async Task<Appointment> UpdateAppointment(UpdateAppointmentDTO appointmentDTO)
        {
            var appointment = await _context.Appointments
                                    .Where(ap => ap.AppointmentId == appointmentDTO.AppointmentID)
                                    .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Appointment Not Found");
            appointment = _mapper.Map(appointmentDTO, appointment);

            _context.Appointments.Update(appointment); 
            await _context.SaveChangesAsync();
            return appointment;
        }

    }
}
