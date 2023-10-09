using DentalClinic.Services;
using DentalClinic.Services.AppointmentService;
using DentalClinic.Services.AreaSettingService;
using DentalClinic.Services.CompanySettingService;
using DentalClinic.Services.EmployeeService;
using DentalClinic.Services.HealthProgressService;
using DentalClinic.Services.MedicalRecordService;
using DentalClinic.Services.PatientService;
using DentalClinic.Services.PaymentService;
using DentalClinic.Services.PaymentTypeService;
using DentalClinic.Services.PricingService;
using DentalClinic.Services.ProcedureService;
using DentalClinic.Services.RoleService;
using DentalClinic.Services.SMSSettingService;
using DentalClinic.Services.Tools;
using DentalClinic.Services.User;
using System.ComponentModel.Design;

namespace DentalClinic
{
    public static class AppServiceRegistration
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IRoleService,  RoleService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPricingService, PricingService>();
            services.AddScoped<IProcedureService, ProcedureService>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IToolsService, ToolsService>();
            services.AddScoped<IHealthProgressService, HealthProgressService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ICompanySettingService, CompanySettingService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAreaSettingService, AreaSettingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ISMSSettingService, SMSSettingService>();

        }

    }
}





