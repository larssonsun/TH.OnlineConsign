﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace th.onlineconsign.Model
{
    public partial class SampleStorageDbContext : DbContext
    {
        public SampleStorageDbContext()
        {
        }

        public SampleStorageDbContext(DbContextOptions<SampleStorageDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SampleStorageAddonDefault> SampleStorageAddonDefault { get; set; }
        public virtual DbSet<SampleStorageMain> SampleStorageMain { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<SampleStorageAddonDefault>(entity =>
            {
                entity.ToTable("SampleStorage_Addon_Default");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Memo)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<SampleStorageMain>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_WT_Sample_1")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("SampleStorage_Main");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.BatchNumber)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ContractSignNumber)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CoreCodeId)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CoreCodeIdEnd)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CoreCodeIdRange)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DelegateQuan).HasColumnName("Delegate_Quan");

                entity.Property(e => e.DelegateQuanUnit)
                    .IsRequired()
                    .HasColumnName("Delegate_Quan_Unit")
                    .HasMaxLength(64);

                entity.Property(e => e.DetectonDate).HasColumnType("datetime");

                entity.Property(e => e.ExamKind).HasColumnName("Exam_Kind");

                entity.Property(e => e.ExamParameter)
                    .IsRequired()
                    .HasColumnName("Exam_Parameter")
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ExamParameterCn)
                    .IsRequired()
                    .HasColumnName("Exam_Parameter_Cn")
                    .HasMaxLength(1024);

                entity.Property(e => e.ExamResult)
                    .IsRequired()
                    .HasColumnName("Exam_Result")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.GradeName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.JzcertificateNo)
                    .IsRequired()
                    .HasColumnName("JZCertificateNo")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.KindId).HasColumnName("KindID");

                entity.Property(e => e.KindName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastEditDateTime).HasColumnType("datetime");

                entity.Property(e => e.MoldingDate)
                    .HasColumnName("Molding_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProJectPart)
                    .IsRequired()
                    .HasColumnName("ProJect_Part")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProduceFactory)
                    .IsRequired()
                    .HasColumnName("Produce_Factory")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.QycertificateNo)
                    .IsRequired()
                    .HasColumnName("QYCertificateNo")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ReExamOldSampleId)
                    .IsRequired()
                    .HasColumnName("ReExam_OldSample_Id")
                    .HasMaxLength(10);

                entity.Property(e => e.RecordCertificate)
                    .IsRequired()
                    .HasColumnName("Record_Certificate")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.Property(e => e.ReportNumber)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SampleId).HasColumnName("SampleID");

                entity.Property(e => e.SampleName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.SampleNo)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SampleRegular).HasColumnName("Sample_Regular");

                entity.Property(e => e.SampleStatus).HasColumnName("Sample_Status");

                entity.Property(e => e.SampleUcDbTableName)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SpecId).HasColumnName("SpecID");

                entity.Property(e => e.SpecName)
                    .IsRequired()
                    .HasMaxLength(128);
            });
        }
    }
}