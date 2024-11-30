using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs;
using DentalClinic.Models;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.PrescriptionService
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public PrescriptionService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }

        public async Task<Prescription> CreatePrescriptionAsync(AddPrescriptionDTO prescription)
        {
            Patient? patient = await _context.Patients.FindAsync(prescription.PatientId);

            Employee? employee = await _context.Employees.FindAsync(prescription.EmployeeId);

            Prescription presc = new Prescription
            {
                //Patient = patient,
                DrugName = prescription.DrugName,
                Strength = prescription.Strength,
                DosageFromDoseFrequency = prescription.DosageFromDoseFrequency,
                DurationQuantity = prescription.DurationQuantity,
                HowToUse = prescription.HowToUse,
                OtherInformation = prescription.OtherInformation,
                TotalPrice = prescription.TotalPrice,
                Registrations = prescription.Registrations,
                Qualification = prescription.Qualification,

            };

            //presc.Patient = patient;
            //presc.Employee = employee;


            _context.Prescriptions.Add(presc);
            await _context.SaveChangesAsync();
            return presc;

        }

        public async Task<List<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }


        public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
        {
            return await _context.Prescriptions.FindAsync(id);
        }

        public async Task<Prescription?> UpdatePrescriptionAsync(Prescription updatedPrescription)
        {
            var prescription = await _context.Prescriptions.FindAsync(updatedPrescription.Id);
            if (prescription == null)
            {
                return null; // Prescription not found
            }

            // Update fields with the new values
            prescription.DrugName = updatedPrescription.DrugName;
            prescription.Strength = updatedPrescription.Strength;
            prescription.DosageFromDoseFrequency = updatedPrescription.DosageFromDoseFrequency;
            prescription.DurationQuantity = updatedPrescription.DurationQuantity;
            prescription.HowToUse = updatedPrescription.HowToUse;
            prescription.OtherInformation = updatedPrescription.OtherInformation;
            prescription.UpdatedAt = DateTime.Now;
            prescription.TotalPrice = updatedPrescription.TotalPrice;
            prescription.Registrations = updatedPrescription.Registrations;
            prescription.Qualification = updatedPrescription.Qualification;

            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return false; // Prescription not found
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
