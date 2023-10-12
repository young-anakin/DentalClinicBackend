using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs.ReportDTO;
using DentalClinic.Services.Tools;

namespace DentalClinic.Services.ReportService
{
    public class ReportService
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
    }
}
