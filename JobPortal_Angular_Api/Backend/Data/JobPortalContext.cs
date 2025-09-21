using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class JobPortalContext : DbContext
{
    public JobPortalContext(DbContextOptions<JobPortalContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<JobSkill> JobSkills { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<CompanyProfile> CompanyProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Unique Email
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        // Enum stored as string
        modelBuilder.Entity<UserDetail>().Property(d => d.DetailType).HasConversion<string>();

        // JobSkill (many-to-many)
        modelBuilder.Entity<JobSkill>().HasKey(js => new { js.JobId, js.SkillId });
        modelBuilder.Entity<JobSkill>()
            .HasOne(js => js.Job)
            .WithMany(j => j.JobSkills)
            .HasForeignKey(js => js.JobId);
        modelBuilder.Entity<JobSkill>()
            .HasOne(js => js.Skill)
            .WithMany(s => s.JobSkills)
            .HasForeignKey(js => js.SkillId);

        // Explicit Job -> Recruiter FK using PostedBy
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Recruiter)
            .WithMany(u => u.Jobs)
            .HasForeignKey(j => j.PostedBy)
            .OnDelete(DeleteBehavior.Cascade); // optional - deleting a job cascades to its applications

        // Resume -> User (many resumes per user)
        // IMPORTANT: Restrict delete to avoid cascade path through Resume -> Application
        modelBuilder.Entity<Resume>()
            .HasOne(r => r.User)
            .WithMany(u => u.Resumes)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Application -> Candidate (User)
        // Restrict delete so deleting user won't cascade to applications (use soft-delete in prod)
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Candidate)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        // Application -> Resume
        // Restrict delete so deleting a resume won't cascade to applications (prevents multiple cascade paths)
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Resume)
            .WithMany(r => r.Applications)
            .HasForeignKey(a => a.ResumeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Application -> Job
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        // CompanyProfile -> User (Recruiter)
        modelBuilder.Entity<CompanyProfile>()
            .HasOne(c => c.Recruiter)
            .WithOne()
            .HasForeignKey<CompanyProfile>(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        var seedDate = new DateTime(2025, 09, 21);

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, FullName = "Admin User", Email = "admin@portal.com", PasswordHash = "admin123", Role = UserRole.Admin, IsActive = true, CreatedOn = seedDate },
            new User { UserId = 2, FullName = "Recruiter User", Email = "recruiter@portal.com", PasswordHash = "recruiter123", Role = UserRole.Recruiter, IsActive = true, CreatedOn = seedDate },
            new User { UserId = 3, FullName = "Candidate User", Email = "candidate@portal.com", PasswordHash = "candidate123", Role = UserRole.Candidate, IsActive = true, CreatedOn = seedDate }
        );

        modelBuilder.Entity<Skill>().HasData(
            new Skill { SkillId = 1, Name = "C#", IsActive = true },
            new Skill { SkillId = 2, Name = "Angular", IsActive = true },
            new Skill { SkillId = 3, Name = "SQL", IsActive = true }
        );

        modelBuilder.Entity<Job>().HasData(
            new Job { JobId = 1, Title = "Software Engineer", Description = "Develop .NET applications", Location = "Chennai", EmploymentType = "Full-time", Salary = 60000m, PostedBy = 2, PostedOn = seedDate, IsActive = true }
        );

        modelBuilder.Entity<JobSkill>().HasData(
            new { JobId = 1, SkillId = 1 },
            new { JobId = 1, SkillId = 2 }
        );
    }

}
