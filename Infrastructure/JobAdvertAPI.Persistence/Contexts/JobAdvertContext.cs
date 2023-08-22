using System;
using System.Collections.Generic;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Domain.Entities.common;
using Microsoft.EntityFrameworkCore;

namespace JobAdvertAPI.Persistence.Contexts;

public partial class JobAdvertContext : DbContext
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

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserJobPost> UserJobPosts { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       var datas= ChangeTracker.Entries<BaseEntity>();

        foreach (var data in datas)
        {
             _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=JobAdvert;User Id=sa;Password=1234;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.ContactNumber).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserType");
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

            entity.HasOne(d => d.User).WithMany(p => p.UserJobPosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserJobPost_User");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserType1)
                .HasMaxLength(50)
                .HasColumnName("UserType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
