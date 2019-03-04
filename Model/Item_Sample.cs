using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace th.onlineconsign.Model
{
    public partial class Item_Sample : DbContext
    {
        public Item_Sample()
        {
        }

        public Item_Sample(DbContextOptions<Item_Sample> options)
            : base(options)
        {
        }

        public virtual DbSet<ItemGrade> ItemGrade { get; set; }
        public virtual DbSet<ItemParameter> ItemParameter { get; set; }
        public virtual DbSet<ItemSpec> ItemSpec { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=101.227.63.18,3496;uid=sa;pwd=941417;database=ScetiMis_Commonality");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<ItemGrade>(entity =>
            {
                entity.ToTable("Item_Grade");

                entity.HasIndex(e => e.GradeId)
                    .HasName("IX_Item_Grade")
                    .IsUnique();

                entity.HasIndex(e => e.SampleId)
                    .HasName("IX_Item_Grade_1");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.GradeName).HasMaxLength(64);

                entity.Property(e => e.GradeType).HasMaxLength(32);

                entity.Property(e => e.SampleId).HasColumnName("SampleID");
            });

            modelBuilder.Entity<ItemParameter>(entity =>
            {
                entity.ToTable("Item_Parameter");

                entity.HasIndex(e => e.CanConsign)
                    .HasName("IX_Item_Parameter_2");

                entity.HasIndex(e => e.ParameterId)
                    .HasName("IX_Item_Parameter")
                    .IsUnique();

                entity.HasIndex(e => e.SampleId)
                    .HasName("IX_Item_Parameter_1");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AssessParmId)
                    .HasColumnName("AssessParmID")
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.CanConsign).HasDefaultValueSql("((0))");

                entity.Property(e => e.CriterionDescription).HasMaxLength(256);

                entity.Property(e => e.IsDefault).HasDefaultValueSql("((0))");

                entity.Property(e => e.ParameterDescription).HasMaxLength(256);

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.ParameterName).HasMaxLength(64);

                entity.Property(e => e.ParameterUc)
                    .HasColumnName("ParameterUC")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PassResult)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SampleId).HasColumnName("SampleID");

                entity.Property(e => e.SetValue)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SetValueDescription).HasMaxLength(256);

                entity.Property(e => e.TestStandard).HasMaxLength(256);

                entity.Property(e => e.UnPassResult)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemSpec>(entity =>
            {
                entity.ToTable("Item_Spec");

                entity.HasIndex(e => e.SampleId)
                    .HasName("IX_Item_Spec");

                entity.HasIndex(e => e.SpecId)
                    .HasName("IX_Item_Spec_1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SampleId).HasColumnName("SampleID");

                entity.Property(e => e.SpecDescription).HasMaxLength(256);

                entity.Property(e => e.SpecId).HasColumnName("SpecID");

                entity.Property(e => e.SpecName).HasMaxLength(64);

                entity.Property(e => e.SpecType).HasMaxLength(32);
            });
        }
    }
}
