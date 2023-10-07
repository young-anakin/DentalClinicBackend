using DentalClinic.Models;
using DentalClinic.Utils;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Procedure>   Procedures { get; set; }

        public DbSet<PricingDescription> pricingDescriptions { get; set; }
        public DbSet<MedicalRecord > MedicalRecords { get; set; }   

        public DbSet<PricingReason> pricingReasons { get; set; }

        public DbSet<PatientProfile> patientProfiles { get; set; }

        public DbSet<PatientVisit> patientVisits { get; set; }

        //public DbSet<Referal> Referals { get; set; }

        public DbSet<HealthProgress> HealthProgresses { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<SubCity> SubCities { get; set; }

        public DbSet<CompanySetting> CompanySettings { get; set; }

        public DbSet<PaymentType> paymentTypes { get; set; }

        public DbSet<PatientCard> PatientCards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        //public DbSet<ProcedureQuantity> ProcedureQuantity { get; set; }




        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var added = ChangeTracker.Entries<IAuditableEntity>().Where(E => E.State == EntityState.Added).ToList();
            var now = DateTime.Now;
            added.ForEach(E =>
            {
                E.Property(x => x.CreatedAt).CurrentValue = now;
                E.Property(x => x.UpdatedAt).CurrentValue = now;

            });

            var modified = ChangeTracker.Entries<IAuditableEntity>().Where(E => E.State == EntityState.Modified).ToList();

            modified.ForEach(E =>
            {
                E.Property(x => x.UpdatedAt).CurrentValue = now;

            });

            return base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HealthProgress>()
                .HasOne(hp => hp.Employee)
                .WithMany(e => e.HealthProgresses)
                .HasForeignKey(hp => hp.AdministeredById)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(hp => hp.TreatedBy)
                .WithMany(e => e.MedicalRecordAdministered)
                .HasForeignKey(hp => hp.TreatedById)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Dentist)
                .WithMany()
                .HasForeignKey(a => a.DentistID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.Cascade);// Set foreign key to null on delete
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<HealthProgress>()
        //        .HasOne(hp => hp.Employee)
        //        .WithMany()
        //        .HasForeignKey(hp => hp.AdministeredBY)
        //        .OnDelete(DeleteBehavior.SetNull); // Set the behavior to Set Null

        //    // Other configurations...
        //}




        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>()
        //        .HasOne(e => e.UserAccount)
        //        .WithOne(u => u.Employee)
        //        .HasForeignKey<UserAccount>(u => u.UserAccountId)
        //        .OnDelete(DeleteBehavior.Cascade); // Set cascade delete behavior

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
