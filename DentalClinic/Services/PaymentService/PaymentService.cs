using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.PaymentDTO;
using DentalClinic.Models;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DentalClinic.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public PaymentService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }

        public async Task<Payment> AddPaymentfromMedicalRecord(MakePaymentMedRecDTO DTO)
        {
            var record = await _context.MedicalRecords
                                        .Where(a => a.Medical_RecordID == DTO.MedicalRecordID)
                                        .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Medical Record Not Found");

            if (DTO.IsCredit == true)
            {
                var compSet = await _context.CompanySettings.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Company settings not set!!!");

                if (DTO.Total > compSet.MaximumLoanAmount)
                {
                    throw new InvalidOperationException("Credit exceeds Maximum loan amount alloted");
                }
                var cr = await _context.Credits.
                            Where(p => p.PatientID == DTO.PatientID).
                            OrderByDescending(p => p.ChargeDate).
                            FirstOrDefaultAsync();

                var Credit = new Credit
                {
                    PatientID = DTO.PatientID,
                    TotalCreditAmount = 0 - DTO.Total,
                    IssuedBy = DTO.IssuedByID,
                    ChargeDate = DTO.DateTime,
                    Paid = 0,
                    UnPaid = DTO.Total
                };

                if (cr != null)
                {
                    // Case 1: Update existing Credit record
                    if (cr.UnPaid + DTO.Total > compSet.MaximumLoanAmount)
                    {
                        throw new InvalidOperationException("Credit exceeds Maximum loan amount alloted");
                    }
                    cr.TotalCreditAmount = cr.TotalCreditAmount - DTO.Total;
                    cr.UnPaid = cr.UnPaid + DTO.Total;
                    _context.Credits.Update(cr);

                }
                else
                {
                    _context.Credits.Add(Credit);
                }



            }

            var procedureIDS = DTO.ProcedureIDs;
            var Quantities = DTO.Quantity;

            record.Quantities = JsonSerializer.Serialize(Quantities); 
            record.ProcedureIDs = JsonSerializer.Serialize(procedureIDS);

            var payment = new Payment
            {
                IssuedByID = DTO.IssuedByID,
                PaymentTypeID = DTO.PaymentType,
                PatientID = DTO.PatientID,
                
                SubTotal = DTO.Total + DTO.Discount / 100 * DTO.Total,
                Total = DTO.Total,
                Discount = DTO.Discount,
                PaymentDate = DTO.DateTime,
                IsCredit = DTO.IsCredit,

            };
            record.IsPaid = true;
            _context.MedicalRecords.Update(record);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
        public async Task<GetMDforPaymentDTO> GetMedicalRecordsforPayment(int id)
        {
            var record = await _context.MedicalRecords
                                                   .Where(a => a.PatientId == id )
                                                   .Where(a=> a.IsPaid == false)
                                                   .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Medical Record Not Found, or Medical Record has been paid for");


            int[] proceduresArray = 
            string.IsNullOrEmpty(record.ProcedureIDs)? new int[] { 0 }: JsonSerializer.Deserialize<int[]>(record.ProcedureIDs);
            int[] quantityArray = 
            string.IsNullOrEmpty(record.Quantities) ? new int[] { 0 } : JsonSerializer.Deserialize<int[]>(record.Quantities);

            var display = new GetMDforPaymentDTO
            {
                PatientId = record.PatientId,
                MedicalRecordID = record.Medical_RecordID,
                Discount = record.DiscountPercent,
                IssuedBy = (int)record.TreatedById, // Adding a null-conditional operator here
                MedicalRecordDate = (DateTime)record.Date,
                SubTotal = record.SubTotalAmount,
                Total = record.TotalAmount,
                ProcedureIDs = proceduresArray,
                Quantity = quantityArray,
                isCard = record.IsCard,   
            };

            return display;

        }
        

    }
}
