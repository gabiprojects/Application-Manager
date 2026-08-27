using ApplicationManagerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagerAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<ContactPerson> ContactPersons => Set<ContactPerson>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<FollowUp> FollowUps => Set<FollowUp>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<ApplicationStatusHistory> ApplicationStatusHistories => Set<ApplicationStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);

            entity.HasMany(c => c.ContactPersons)
                .WithOne(cp => cp.Company)
                .HasForeignKey(cp => cp.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.JobApplications)
                .WithOne(ja => ja.Company)
                .HasForeignKey(ja => ja.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ContactPerson>(entity =>
        {
            entity.HasKey(cp => cp.Id);
        });

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(ja => ja.Id);
            entity.Property(ja => ja.PositionTitle).IsRequired().HasMaxLength(200);
            entity.Property(ja => ja.Status).HasConversion<string>();
            entity.Property(ja => ja.EmploymentType).HasConversion<string>();
            entity.Property(ja => ja.WorkModel).HasConversion<string>();
            entity.Property(ja => ja.SalaryMin).HasPrecision(18, 2);
            entity.Property(ja => ja.SalaryMax).HasPrecision(18, 2);

            entity.HasMany(ja => ja.Interviews)
                .WithOne(i => i.JobApplication)
                .HasForeignKey(i => i.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(ja => ja.FollowUps)
                .WithOne(f => f.JobApplication)
                .HasForeignKey(f => f.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ja => ja.Offer)
                .WithOne(o => o.JobApplication)
                .HasForeignKey<Offer>(o => o.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(ja => ja.StatusHistory)
                .WithOne(h => h.JobApplication)
                .HasForeignKey(h => h.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.InterviewType).HasConversion<string>();
            entity.Property(i => i.Status).HasConversion<string>();
        });

        modelBuilder.Entity<FollowUp>(entity =>
        {
            entity.HasKey(f => f.Id);
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Status).HasConversion<string>();
            entity.Property(o => o.AnnualSalary).HasPrecision(18, 2);
            entity.HasIndex(o => o.JobApplicationId).IsUnique();
        });

        modelBuilder.Entity<ApplicationStatusHistory>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Status).HasConversion<string>();
        });
    }
}
