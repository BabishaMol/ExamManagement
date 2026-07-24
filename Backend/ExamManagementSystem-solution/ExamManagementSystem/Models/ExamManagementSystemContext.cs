using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Models;

public partial class ExamManagementSystemContext : DbContext
{
    public ExamManagementSystemContext()
    {
    }

    public ExamManagementSystemContext(DbContextOptions<ExamManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExamDtl> ExamDtls { get; set; }

    public virtual DbSet<ExamMaster> ExamMasters { get; set; }

    public virtual DbSet<StudentMst> StudentMsts { get; set; }

    public virtual DbSet<SubjectMst> SubjectMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=BABI\\SQLEXPRESS;Initial Catalog=ExamManagementSystem;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExamDtl>(entity =>
        {
            entity.HasKey(e => e.DtlsId).HasName("PK__ExamDtls__63D4BCF5E8178D66");

            entity.Property(e => e.DtlsId).HasColumnName("DtlsID");
            entity.Property(e => e.MasterId).HasColumnName("MasterID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Master).WithMany(p => p.ExamDtls)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamDtls_ExamMaster");

            entity.HasOne(d => d.Subject).WithMany(p => p.ExamDtls)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamDtls_Subject");
        });

        modelBuilder.Entity<ExamMaster>(entity =>
        {
            entity.HasKey(e => e.MasterId).HasName("PK__ExamMast__F6B782C486A22533");

            entity.ToTable("ExamMaster");

            entity.HasIndex(e => new { e.StudentId, e.ExamYear }, "UQ_Student_ExamYear").IsUnique();

            entity.Property(e => e.MasterId).HasColumnName("MasterID");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PassOrFail)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Student).WithMany(p => p.ExamMasters)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamMaster_Student");
        });

        modelBuilder.Entity<StudentMst>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__StudentM__32C52A79F370AAD3");

            entity.ToTable("StudentMst");

            entity.HasIndex(e => e.Mail, "UQ__StudentM__2724B2D10F2D51D7").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Mail)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubjectMst>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__SubjectM__AC1BA38877763EA7");

            entity.ToTable("SubjectMst");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
