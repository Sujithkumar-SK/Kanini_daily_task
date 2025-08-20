using APIKanini.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace APIKanini.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Doctor–Patient many-to-many (no explicit join entity required)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Patients)
                .WithMany(p => p.Doctors)
                .UsingEntity(j => j.ToTable("DoctorPatients")); // optional, EF will auto-name

            // Hospital Seed
            modelBuilder.Entity<Hospital>().HasData(
                new Hospital { HospitalId = "HOS001", Name = "City Hospital" },
                new Hospital { HospitalId = "HOS002", Name = "Green Valley Hospital" }
            );

            // Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = "DOC001", Name = "Dr. Smith", HospitalId = "HOS001" },
                new Doctor { DoctorId = "DOC002", Name = "Dr. John", HospitalId = "HOS001" },
                new Doctor { DoctorId = "DOC003", Name = "Dr. Emma", HospitalId = "HOS002" }
            );

            // Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = "PAT001", Name = "Alice", HospitalId = "HOS001" },
                new Patient { PatientId = "PAT002", Name = "Bob", HospitalId = "HOS001" },
                new Patient { PatientId = "PAT003", Name = "Charlie", HospitalId = "HOS002" }
            );

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = "R001", RoleName = "Admin" },
                new Role { RoleId = "R002", RoleName = "Doctor" },
                new Role { RoleId = "R003", RoleName = "Patient" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = "U001", UserName = "SuperAdmin", Email = "admin@hospital.com", PasswordHash = "hashed123", RoleId = "R001" },
                new User { UserId = "U002", UserName = "DrJohn", Email = "doctor@hospital.com", PasswordHash = "hashed456", RoleId = "R002" },
                new User { UserId = "U003", UserName = "PatientMary", Email = "patient@hospital.com", PasswordHash = "hashed789", RoleId = "R003" }
            );

        }
    }

}
