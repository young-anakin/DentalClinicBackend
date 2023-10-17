using AutoMapper;
using DentalClinic.Context;
using DentalClinic.Models;
using DentalClinic.DTOs.PatientDTO;
using Microsoft.EntityFrameworkCore;
using DentalClinic.Services.Tools;
using DentalClinic.Migrations;

namespace DentalClinic.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public PatientService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }
        public async Task<Patient> AddPatient(AddPatientDTO patientDTO)
        {
            var patient = _mapper.Map<Patient>(patientDTO);
            patient.CreatedAt = DateTime.Now;
            DateTime date = new DateTime(2000, 1, 1);

            patient.UpdatedAt = DateTime.Now;
            if (patientDTO.Age == 0)
            {
                patient.Age = _toolsService.CalculateAge(patientDTO.DateOfBirth);
            }
            else
            {
                patient.DateOfBirth = _toolsService.CalculateDOB(patient.Age);
            }
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
                                                 .Include(p=> p.Credits)
                                                .FirstOrDefaultAsync();
            if (patient != null)
            {
                // Step 2: Delete associated referrals
                if(patient.MedicalRecords != null) 
                {
                    //var referralIds =  patient.MedicalRecords.SelectMany(mr => mr.Referals.Select(r => r.ReferalID)).ToList();
                    //var referrals =  _context.Referals.Where(r => referralIds.Contains(r.ReferalID));
                    //_context.Referals.RemoveRange(referrals);
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
            patient.UpdatedAt = DateTime.Now;
            var PatientProfile = await _context.patientProfiles
            .Where(p => p.Patient_Id == patientDTO.PatientID)
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException("Patient Not Found");



            patient = _mapper.Map(patientDTO, patient);
            PatientProfile = _mapper.Map(patientDTO, PatientProfile);
            patient.Profile = PatientProfile;
            patient.DateOfBirth = _toolsService.CalculateDOB(patientDTO.Age);
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            return patient;
                                                
        }
        public async Task<List<DisplayPatientDTO>> GetAllPatients()
        {
            DateTime date = new DateTime(2000, 1, 1);
            var patients = await _context.Patients.OrderByDescending(c=> c.UpdatedAt)
                .Include(p=> p.PatientCard)
                .ToListAsync();
            var compSettings = await _context.CompanySettings.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Company Settings Not Found!");
            var cardExpireAfter = compSettings.CardExpireAfter;
            // Create a list of PatientDTO with calculated ages
            var patientDTOs = patients.Select(p => new DisplayPatientDTO
            {
                PatientId = p.PatientId,
                PatientFullName = p.PatientFullName,
                Age = _toolsService.CalculateAge(p.DateOfBirth),
                Phone = p.Phone,
                Gender = p.Gender,
                Country = p.Country,
                City = p.City,
                Subcity = p.Subcity,
                Address = p.Address,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt ?? date,
                CardNeeded =  p.PatientCard == null || p.PatientCard.CreatedAT < DateTime.Now.AddDays(-cardExpireAfter)



            }).ToList();
            return patientDTOs;
        }
        public async Task<DisplayPatientDTO> GetSpecificPatient(int ID)
        {
            DateTime date = new DateTime(2000, 1, 1);
            var compSettings = await _context.CompanySettings.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Company Settings Not Found!");
            var cardExpireAfter = compSettings.CardExpireAfter;
            var patients = await _context.Patients.
                                                 Where(pp => pp.PatientId == ID)
                                                .FirstOrDefaultAsync();
            var patientCard = await _context.PatientCards.Where(p => p.PatientID == ID).FirstOrDefaultAsync();
            var boolcheck = false;
            if (patientCard == null)
            {
                boolcheck = true;
            }
            else if (patientCard.CreatedAT < DateTime.Now.AddDays(-cardExpireAfter))
            {
                boolcheck = true;
            }
            var PatientProfile = await _context.patientProfiles
                                                .Where(pp=> pp.Patient_Id == ID)
                                                .FirstOrDefaultAsync();
            
            var procedure = await _context.Procedures.Where(p=> p.ProcedureName == "card" || p.ProcedureName == "Card" || p.ProcedureName == "CARD")
                .FirstOrDefaultAsync(); 


            
            var patientDTO = new DisplayPatientDTO
            {
                PatientId = patients.PatientId,
                PatientFullName = patients.PatientFullName,
                Age = _toolsService.CalculateAge(patients.DateOfBirth),
                Phone = patients.Phone,
                Gender = patients.Gender,
                Country = patients.Country,
                City = patients.City,
                Subcity = patients.Subcity,
                Address = patients.Address,
                MedicalHistory = PatientProfile.MedicalHistory,
                Chronics = PatientProfile.Chronics,
                Allergies = PatientProfile.Allergies,
                CreatedAt = patients.CreatedAt,
                UpdatedAt = patients.UpdatedAt ?? date,
                CardNeeded = boolcheck,
                Procedure = procedure,
            };
            
            return patientDTO;
        }

    }
}
