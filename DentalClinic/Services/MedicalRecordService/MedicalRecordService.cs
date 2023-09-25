using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.MedicalRecordDTO;
using DentalClinic.DTOs.PatientDTO;
using DentalClinic.Models;
using DentalClinic.Services.PatientService;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.MedicalRecordService
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public MedicalRecordService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }
        public async Task AddMedicalRecord(AddMedicalRecordDTO recordDTO)
        {
            var record = _mapper.Map<MedicalRecord>(recordDTO);
            record.Patient = await _context.Patients
                        .Where(pa => pa.PatientId == recordDTO.PatientIdNo)
                        .FirstOrDefaultAsync();
            var TreatmentBY = await _context.Employees
                        .Where(e => e.EmployeeId == recordDTO.TreatedByID)
                        .FirstOrDefaultAsync();
            record.TreatedBy = TreatmentBY;
            string[] separatedStrings = _toolsService.ReturnArrayofCommaSeparatedStrings(recordDTO.ReferalsList);

            List<Referal> referalList = new List<Referal>();

            foreach (string str in separatedStrings)
            {
                Referal referalItem = new Referal();
                referalItem.ReasonForReferal = str;


                referalItem.Employee = TreatmentBY;
                referalItem.ReferedDoctor = recordDTO.ReferedDoctor;
                referalList.Add(referalItem);
            }
            record.Referals = referalList;
            List<Procedure> proceduresList = new List<Procedure>();
            int[] Procedures;

            Procedures = recordDTO.ProceduresIDs;
            foreach (int var in Procedures)
            {
                Procedure? procedureItem = new Procedure();
                procedureItem = await _context.Procedures
                                                .Where(pr => pr.ProcedureID == var)
                                                .FirstOrDefaultAsync();
                if (procedureItem != null)
                {
                    proceduresList.Add(procedureItem);
                }

            }
            record.Procedures = proceduresList;
            await _context.MedicalRecords.AddAsync(record);
            await _context.SaveChangesAsync();
        }
        public async Task<MedicalRecord> DeleteMedicalRecord(int MedicalRecordID)
        {
            var MedicalRecord = await _context.MedicalRecords.
                                 Where(mr=> mr.Medical_RecordID == MedicalRecordID)       
                                .FirstOrDefaultAsync()?? throw new KeyNotFoundException("Medical Record Not Found!");
            _context.MedicalRecords.Remove(MedicalRecord);
           await _context.SaveChangesAsync();
            return MedicalRecord;
        }
        //Medical record Update Not needed
        //public async Task<MedicalRecord> UpdateMedicalRecord(UpdateMedicalRecordDTO medicalRecordDTO)
        //{
        //    var record = await _context.MedicalRecords
        //                        .Where(mr=> mr.Medical_RecordID == medicalRecordDTO.MedicalRecordID)
        //                        .FirstOrDefaultAsync()?? throw new KeyNotFoundException("Medical Record not Found!");
        //    record.Patient = await _context.Patients
        //                .Where(pa => pa.PatientId == medicalRecordDTO.PatientIdNo)
        //                .FirstOrDefaultAsync();
        //    var TreatmentBY = await _context.Employees
        //                .Where(e => e.EmployeeId == medicalRecordDTO.TreatedByID)
        //                .FirstOrDefaultAsync();
        //    record.TreatedBy = TreatmentBY;

        //}
        public async Task<List<MedicalRecord>> GetMedicalRecordforPatient(int patientID)
        {
            var record = await _context.MedicalRecords
                        .Where(Mr => Mr.PatientId == patientID)
                        .Include(Mr => Mr.Procedures)
                        .Include(Mr => Mr.Referals)
                        .ToListAsync();
            if (record != null)
            {
                return record;
            }

            return new List<MedicalRecord>();


                         
        }
    }

}
