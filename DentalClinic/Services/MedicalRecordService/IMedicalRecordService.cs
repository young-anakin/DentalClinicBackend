using DentalClinic.DTOs.MedicalRecordDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.MedicalRecordService
{
    public interface IMedicalRecordService
    {
        Task AddMedicalRecord(AddMedicalRecordDTO recordDTO);
        Task<List<MedicalRecord>> GetMedicalRecordforPatient(int patientID);
    }
}