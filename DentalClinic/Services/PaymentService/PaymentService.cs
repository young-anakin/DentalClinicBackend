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

            var payment = new Payment
            {
                IssuedByID = (int)record.TreatedById,
                PaymentTypeID = DTO.PaymentType,
                PatientID = record.PatientId,
                
                SubTotal = record.TotalAmount + record.DiscountPercent / 100 * record.TotalAmount,
                Total = record.TotalAmount,
                Discount = record.DiscountPercent,
                PaymentDate = DateTime.Now,

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
                IssuedBy = record.TreatedBy?.EmployeeName, // Adding a null-conditional operator here
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
