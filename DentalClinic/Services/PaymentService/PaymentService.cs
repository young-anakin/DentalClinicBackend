using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.AppointmentDTO;
using DentalClinic.DTOs.PaymentDTO;
using DentalClinic.Models;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;

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
                Total = record.TotalAmount,
                SubTotal = record.TotalAmount + record.DiscountPercent / 100 * record.TotalAmount,
                Discount = record.DiscountPercent / 100 * record.TotalAmount,
                PaymentDate = DateTime.Now,

            };
            record.IsPaid = true;
            _context.MedicalRecords.Update(record);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
        public async Task<ReceiptDetailDTO> DisplayPaymentReceipt(int id)
        {
            var record = await _context.Payments
                                                   .Where(a => a.Id == id)
                                                   .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Medical Record Not Found");

            var display = new ReceiptDetailDTO
            {
                Id = record.Id,
                Discount = record.Discount,
                IssuedBy = record.Employee.EmployeeName,
                PaymentDate = record.PaymentDate,
                SubTotal = record.SubTotal,
                Total = record.Total,
                PaymentType = record.PaymentType.PaymentName,
                PatientName = record.Patient.PatientFullName,
            };
            return display;

        }

    }
}
