using AutoMapper;
using DentalClinic.Context;
using DentalClinic.Models;
using DentalClinic.DTOs.PatientDTO;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PatientService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Patient> AddPatient(AddPatientDTO patientDTO)
        {
            var patient = _mapper.Map<Patient>(patientDTO);
            var patientProfile = _mapper.Map<PatientProfile>(patientDTO);
            patient.Profile = patientProfile;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<Patient> DeletePatient(int ID)
        {
            var patient = await _context.Patients.Where(p=> p.PatientId == ID)
                                                 .Include(p=> p.PatientVisits)
                                                 .Include(p=>p.HealthProgresses)
                                                 .Include(p=>p.Appointments)
                                                 .Include(p => p.Profile)
                                                 .Include(p=>p.MedicalRecords)
                                                .FirstOrDefaultAsync();
            if (patient != null)
            {
                // Step 2: Delete associated referrals
                if(patient.MedicalRecords != null) 
                {
                    var referralIds =  patient.MedicalRecords.SelectMany(mr => mr.Referals.Select(r => r.ReferalID)).ToList();
                    var referrals =  _context.Referals.Where(r => referralIds.Contains(r.ReferalID));
                    _context.Referals.RemoveRange(referrals);
                    _context.MedicalRecords.RemoveRange(patient.MedicalRecords);
                }

            }

            if (patient == null) throw new KeyNotFoundException("Patient Not Found");
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<Patient> UpdatePatient(UpdatePatientDTO patientDTO)
        {
            var patient = await _context.Patients
                        .Where(p => p.PatientId == patientDTO.PatientID)
                        .Include(p=>p.Profile)
                        .FirstOrDefaultAsync()
                        ?? throw new KeyNotFoundException("Patient Not Found");
            var PatientProfile = await _context.patientProfiles
            .Where(p => p.patient_Id == patientDTO.PatientID)
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException("Patient Not Found");

            patient = _mapper.Map(patientDTO, patient);
            PatientProfile = _mapper.Map(patientDTO, PatientProfile);
            patient.Profile = PatientProfile;
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            return patient;
                                                
        }
        public async Task<List<Patient>> GetAllPatients()
        {
            var patients = await _context.Patients
                                .Include(p=> p.Profile)
                                .ToListAsync();
            return patients;
        }
        public async Task<Patient> GetSpecificPatient(int ID)
        {
            var patient = await _context.Patients
                                .Where(p=> p.PatientId == ID)
                                .Include(p=>p.Profile)
                                .FirstOrDefaultAsync()
                                ?? throw new KeyNotFoundException("Patient Not Found");
            return patient;
        }

    }
}
