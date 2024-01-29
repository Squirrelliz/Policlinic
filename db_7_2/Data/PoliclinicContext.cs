using System;
using System.Collections.Generic;
using db_7_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace db_7_2.Data;

public partial class PoliclinicContext : DbContext
{
    public PoliclinicContext()
    {
    }

    public PoliclinicContext(DbContextOptions<PoliclinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContactDatum> ContactData { get; set; }

    public virtual DbSet<Diagnose> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<ElectronicCard> ElectronicCards { get; set; }

    public virtual DbSet<HistoryOfDiagnose> HistoryOfDiagnoses { get; set; }

    public virtual DbSet<HistoryOfMedicalService> HistoryOfMedicalServices { get; set; }

    public virtual DbSet<MedicalService> MedicalServices { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=policlinic;Username=postgres;Password=liza8908");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("status_medical_service", new[] { "выполнена", "ожидается", "отменена" });

        modelBuilder.Entity<ContactDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contact_data_pkey");

            entity.ToTable("contact_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasDefaultValueSql("'no'::text")
                .HasColumnName("address");
            entity.Property(e => e.PassportData)
                .HasDefaultValueSql("'no'::text")
                .HasColumnName("passport_data");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Diagnose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("diagnose_pkey");

            entity.ToTable("diagnose");

            entity.HasIndex(e => e.Name, "constraint_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctor_pkey");

            entity.ToTable("doctor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactDataId)
                .ValueGeneratedOnAdd()
                .HasColumnName("contact_data_id");
            entity.Property(e => e.Fcs)
                .HasMaxLength(100)
                .HasColumnName("fcs");
            entity.Property(e => e.QualificationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("qualification_id");

            entity.HasOne(d => d.ContactData).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.ContactDataId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("doctor_contact_data_id_fkey");

            entity.HasOne(d => d.Qualification).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.QualificationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("doctor_qualification_id_fkey");
        });

        modelBuilder.Entity<ElectronicCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("electronic_card_pkey");

            entity.ToTable("electronic_card");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfCreation).HasColumnName("date_of_creation");
            entity.Property(e => e.MhiPolicy)
                .HasDefaultValueSql("'no'::text")
                .HasColumnName("mhi_policy");
        });

        modelBuilder.Entity<HistoryOfDiagnose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("history_of_diagnose_pkey");

            entity.ToTable("history_of_diagnose");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfDetection).HasColumnName("date_of_detection");
            entity.Property(e => e.DiagnoseId)
                .ValueGeneratedOnAdd()
                .HasColumnName("diagnose_id");
            entity.Property(e => e.ElectronicCardId)
                .ValueGeneratedOnAdd()
                .HasColumnName("electronic_card_id");

            entity.HasOne(d => d.Diagnose).WithMany(p => p.HistoryOfDiagnoses)
                .HasForeignKey(d => d.DiagnoseId)
                .HasConstraintName("history_of_diagnose_diagnose_id_fkey");

            entity.HasOne(d => d.ElectronicCard).WithMany(p => p.HistoryOfDiagnoses)
                .HasForeignKey(d => d.ElectronicCardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("history_of_diagnose_electronic_card_id_fkey");
        });

        modelBuilder.Entity<HistoryOfMedicalService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("history_of_medical_service_pkey");

            entity.ToTable("history_of_medical_service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_time");
            entity.Property(e => e.DoctorId)
                .ValueGeneratedOnAdd()
                .HasColumnName("doctor_id");
            entity.Property(e => e.ElectronicCardId)
                .ValueGeneratedOnAdd()
                .HasColumnName("electronic_card_id");
            entity.Property(e => e.MedicalServiceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("medical_service_id");
            entity.Property(e => e.Result)
                .HasDefaultValueSql("'no'::text")
                .HasColumnName("result");
            entity.Property(e => e.Status).IsRequired().IsRequired()
                       .HasConversion(new EnumToNumberConverter<StatusOfMedicalService, int>())
                       .HasColumnName("status");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HistoryOfMedicalServices)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("history_of_medical_service_doctor_id_fkey");

            entity.HasOne(d => d.ElectronicCard).WithMany(p => p.HistoryOfMedicalServices)
                .HasForeignKey(d => d.ElectronicCardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("history_of_medical_service_electronic_card_id_fkey");

            entity.HasOne(d => d.MedicalService).WithMany(p => p.HistoryOfMedicalServices)
                .HasForeignKey(d => d.MedicalServiceId)
                .HasConstraintName("history_of_medical_service_medical_service_id_fkey");
        });

        modelBuilder.Entity<MedicalService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("medical_service_pkey");

            entity.ToTable("medical_service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_pkey");

            entity.ToTable("patient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactDataId)
                .ValueGeneratedOnAdd()
                .HasColumnName("contact_data_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.ElectronicCardId)
                .ValueGeneratedOnAdd()
                .HasColumnName("electronic_card_id");
            entity.Property(e => e.Fcs)
                .HasMaxLength(100)
                .HasColumnName("fcs");

            entity.HasOne(d => d.ContactData).WithMany(p => p.Patients)
                .HasForeignKey(d => d.ContactDataId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("patient_contact_data_id_fkey");

            entity.HasOne(d => d.ElectronicCard).WithMany(p => p.Patients)
                .HasForeignKey(d => d.ElectronicCardId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("patient_electronic_card_id_fkey");
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("qualification_pkey");

            entity.ToTable("qualification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Education)
                .HasDefaultValueSql("'no'::text")
                .HasColumnName("education");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
