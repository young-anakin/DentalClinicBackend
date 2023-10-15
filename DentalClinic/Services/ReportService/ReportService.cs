using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.ReportDTO;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.ReportService
{
    public class ReportService : IReportService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public ReportService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }
        //public async Task<List<ProcedureGenderDisplayDTO>> ProcedureGender()
        //{
        //    var procedure = await _context.Procedures.Where(p => p.)
        //}
        public async Task<List<Object>> GenderBySubCity()
        {
            var data = _context.Patients
                    .GroupBy(p => p.Subcity)
                    .Select(g => new
                    {
                        SubCity = g.Key,
                        Male = g.Count(p => p.Gender == "Male"),
                        Female = g.Count(p => p.Gender == "Female")
                    })
                    .ToList();
            return data.Cast<object>().ToList();
        }
    }
}
