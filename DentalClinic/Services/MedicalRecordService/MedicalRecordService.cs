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
        public async Task<MedicalRecord> AddMedicalRecord(AddMedicalRecordDTO recordDTO)
        {
            var record = _mapper.Map<MedicalRecord>(recordDTO);
            record.Patient = await _context.Patients
                        .Where(pa => pa.PatientId == recordDTO.PatientIdNo)
                        .FirstOrDefaultAsync();
            var TreatmentBY = await _context.Employees
                        .Where(e => e.EmployeeId == recordDTO.TreatedByID)
                        .FirstOrDefaultAsync();
            record.TreatedBy = TreatmentBY;
            //string[] separatedStrings = _toolsService.ReturnArrayofCommaSeparatedStrings(recordDTO.ReferalsList);

            //List<Referal> referalList = new List<Referal>();


            List<Procedure> proceduresList = new List<Procedure>();
            int[] Procedures;

            Procedures = recordDTO.ProceduresIDs;
            //foreach (int var in Procedures)
            //{
            //    Procedure? procedureItem = new Procedure();
            //    procedureItem = await _context.Procedures
            //                                    .Where(pr => pr.ProcedureID == var)
            //                                    .FirstOrDefaultAsync();
            //    if (procedureItem != null)
            //    {
            //        proceduresList.Add(procedureItem);
            //    }
            //}

            decimal totalPrice = 0;
            for (int i = 0; i < (recordDTO.Quantity.Length); i++)
            {
                int procedureId = Procedures[i];
                int quantity = recordDTO.Quantity[i];
                Procedure? procedureItem = new Procedure();
                
                procedureItem = await _context.Procedures
                                                       .Where(pr => pr.ProcedureID == procedureId)
                                                       .FirstOrDefaultAsync();

                if (procedureItem != null)
                {
                    proceduresList.Add(procedureItem);

                    // Multiply the price with the quantity
                   totalPrice = totalPrice + (decimal)(procedureItem.Price * quantity);

                    // Do something with totalPrice if needed.
                }
            }
            if (record.DiscountPercent != 0)
            {
                totalPrice = (totalPrice) - (decimal)(record.DiscountPercent) / 100 * totalPrice;
            }
            else
            {
                 totalPrice = totalPrice;
            }
            record.DiscountPercent = recordDTO.DiscountPercent;
            record.Procedures = proceduresList;
            record.TotalAmount = totalPrice ; 
            await _context.MedicalRecords.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
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
        //public async Task<List<MedicalRecord>> GetMedicalRecordforPatient(int patientID)
        //{
        //    var record = await _context.MedicalRecords
        //                .Where(Mr => Mr.PatientId == patientID)
        //                .Include(Mr => Mr.Procedures)
        //                .ToListAsync();
        //    if (record != null)
        //    {
        //        return record;
        //    }

        //    return new List<MedicalRecord>();



        //}
        public async Task<List<DisplayMedicalRecordDTO>> GetAllMedicalRecords()
        {
            var records = await _context.MedicalRecords
                                    .Include(r=> r.Procedures)
                                    .Include(r => r.Procedures)
                                    .Include(r => r.TreatedBy)
                                    .ToListAsync();

            var recordDTOs = records.Select(r => new DisplayMedicalRecordDTO
            {
                Medical_RecordID = r.Medical_RecordID,
                PatientId = r.PatientId,
                TreatedById = r.TreatedById.HasValue ? r.TreatedById.Value : 0,
                TreatedByName = r.TreatedBy?.EmployeeName ?? "",
                Procedures = r.Procedures,
                PrescribedMedicinesandNotes = r.PrescribedMedicinesandNotes,
                ReferalsList = r.ReferalList,
                DiscountPercent = r.DiscountPercent,
                TotalAmount = r.TotalAmount,
                date = r.Date ?? DateTime.MinValue,
            }).ToList().OrderByDescending(r => r.date).ToList();



            return recordDTOs;
        }

        public async Task<List<DisplayMedicalRecordDTO>> GetMedicalRecordById(int id)
        {
            var records = await _context.MedicalRecords
                                 .Where(pp => pp.PatientId == id)
                                 .Include(r=> r.Procedures)
                                 .Include(r=> r.TreatedBy)
                                 .ToListAsync();
                                 

            if (records != null)
            {
                var recordDTOs = records.Select(r => new DisplayMedicalRecordDTO
                {
                    Medical_RecordID = r.Medical_RecordID,
                    PatientId = r.PatientId,
                    TreatedById = r.TreatedById.HasValue ? r.TreatedById.Value : 0,
                    TreatedByName = r.TreatedBy?.EmployeeName ?? "",
                    PrescribedMedicinesandNotes = r.PrescribedMedicinesandNotes,
                    ReferalsList = r.ReferalList,
                    Procedures = r.Procedures,
                    DiscountPercent = r.DiscountPercent,
                    TotalAmount = r.TotalAmount,
                    date = r.Date ?? DateTime.MinValue,
                }).ToList().OrderByDescending(r => r.date).ToList();

                return recordDTOs;
            }
            else
            {
                return null;
            }
        }
    }


}


