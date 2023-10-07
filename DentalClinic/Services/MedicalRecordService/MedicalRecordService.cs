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
            int cardExpireAfter = 14;
            decimal totalPrice = 0;
            List<Procedure> proceduresList = new List<Procedure>();
            var companySettings = await _context.CompanySettings
                    .FirstOrDefaultAsync();

            if (companySettings != null)
            {
                cardExpireAfter = companySettings.CardExpireAfter;
            }
            record.Patient = await _context.Patients
                        .Where(pa => pa.PatientId == recordDTO.PatientId)
                        .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Patient Not Found!!!");
            var TreatmentBY = await _context.Employees
                        .Where(e => e.EmployeeId == recordDTO.TreatedByID)
                        .FirstOrDefaultAsync()?? throw new KeyNotFoundException("Employee Not Found!!!");
            var patientCard = await _context.PatientCards
                        .Where(e => e.PatientID == recordDTO.PatientId)
                        .FirstOrDefaultAsync();
            if (patientCard == null)
            {
                var pc = new PatientCard
                {
                    PatientID = record.Patient.PatientId,
                    CreatedAT = DateTime.Now,
                };
                await _context.PatientCards.AddAsync(pc);
                Procedure cardProcedure = await _context.Procedures
                        .Where(pr => pr.ProcedureName == "card")  // Replace with actual ID
                        .FirstOrDefaultAsync()??throw new KeyNotFoundException("card Procedure not found!!!");

                if (cardProcedure != null)
                {
                    proceduresList.Add(cardProcedure);
                    totalPrice += cardProcedure.Price.Value; // Assuming Price is a decimal property
                }
            }
            else if (patientCard != null && patientCard.CreatedAT < DateTime.Now.AddDays(-cardExpireAfter))
            {

                patientCard.CreatedAT = DateTime.Now;
                _context.PatientCards.Update(patientCard);
                Procedure cardProcedure = await _context.Procedures
                       .Where(pr => pr.ProcedureName == "card")  // Replace with actual ID
                       .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("card Procedure not found!!!");
                if (cardProcedure != null)
                {
                    proceduresList.Add(cardProcedure);
                    totalPrice += cardProcedure.Price.Value; // Assuming Price is a decimal property
                }

            }
            record.TreatedBy = TreatmentBY;
            //string[] separatedStrings = _toolsService.ReturnArrayofCommaSeparatedStrings(recordDTO.ReferalsList);

            //List<Referal> referalList = new List<Referal>();



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


