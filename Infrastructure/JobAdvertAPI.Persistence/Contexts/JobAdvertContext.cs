using System;
using System.Collections.Generic;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Domain.Entities.common;
using JobAdvertAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = JobAdvertAPI.Domain.Entities.File;

namespace JobAdvertAPI.Persistence.Contexts;

public partial class JobAdvertContext : IdentityDbContext<AppUser,AppRole, string>
{
    public JobAdvertContext()
    {
    }

    public JobAdvertContext(DbContextOptions<JobAdvertContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplyStatus> ApplyStatuses { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    

    public virtual DbSet<UserJobPost> UserJobPosts { get; set; }

    
    public virtual DbSet<File> Files { get; set; }
    public virtual DbSet<UserCvFile> UserCvFiles { get; set; }
    public virtual DbSet<JobPostImageFile> JobPostImageFiles { get; set; }

    

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       var datas= ChangeTracker.Entries<BaseEntity>();

        foreach (var data in datas)
        {
             _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=JobAdvert2;User Id=sa;Password=1234;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        modelBuilder.Entity<ApplyStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AplyStatus");

            entity.ToTable("ApplyStatus");

            entity.Property(e => e.ApplyStatus1)
                .HasMaxLength(150)
                .HasColumnName("ApplyStatus");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.ToTable("JobPost");

            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.JobType).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_JobType");
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.ToTable("JobType");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(500);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

       

        modelBuilder.Entity<UserJobPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ApplyStatus");

            entity.ToTable("UserJobPost");

            entity.Property(e => e.ApplyDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.ApplyStatus).WithMany(p => p.UserJobPosts)
                .HasForeignKey(d => d.ApplyStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserJobPost_ApplyStatus");

            entity.HasOne(d => d.JobPost).WithMany(p => p.UserJobPosts)
                .HasForeignKey(d => d.JobPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserJobPost_JobPost");

        });

    

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
