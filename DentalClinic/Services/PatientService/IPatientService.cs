using DentalClinic.DTOs.PatientDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.PatientService
{
    public interface IPatientService
    {
        Task<Patient> AddPatient(AddPatientDTO patientDTO);
        Task<Patient> DeletePatient(int ID);
        Task<List<Patient>> GetAllPatients();
        Task<Patient> GetSpecificPatient(int ID);
        Task<Patient> UpdatePatient(UpdatePatientDTO patientDTO);
    }
}