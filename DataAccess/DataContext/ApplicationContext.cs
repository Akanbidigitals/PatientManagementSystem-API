using Microsoft.EntityFrameworkCore;
using PatientManagementSystem_API.Domain.Models;

namespace PatientManagementSystem_API.DataAccess.DataContext
{
    public class ApplicationContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Patient>()
                .HasMany(r=>r.PatientRecords)
                .WithOne()
                .HasForeignKey(r => r.PatientId);

            modelBuilder.Entity<PatientRecord>()
                .HasKey(r => r.Id);    
        }
    }
}
