using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace th.onlineconsign.Model
{
    public partial class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<ItemSample> ItemSample { get; set; }
        public virtual DbSet<ItemItem> ItemItem { get; set; }
        public virtual DbSet<ItemKind> ItemKind { get; set; }
        public virtual DbSet<ItemGrade> ItemGrade { get; set; }
        public virtual DbSet<ItemParameter> ItemParameter { get; set; }
        public virtual DbSet<ItemSpec> ItemSpec { get; set; }
        public virtual DbSet<DpProductionUnitType> DpProductionUnitType { get; set; }
        public virtual DbSet<UnitProductionUnit> UnitProductionUnit { get; set; }
        public virtual DbSet<DpDelegateQuanUnit> DpDelegateQuanUnit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<ItemSample>(entity =>
            {
                entity.ToTable("Item_Sample");

                entity.HasIndex(e => e.CanConsign)
                    .HasName("IX_Item_Sample_2");

                entity.HasIndex(e => e.ItemId)
                    .HasName("IX_Item_Sample_1");

                entity.HasIndex(e => e.SampleId)
                    .HasName("IX_Item_Sample")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CanConsign).HasDefaultValueSql("((0))");

                entity.Property(e => e.DownloadTarget)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.GradeSn).HasColumnName("GradeSN");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.KeyBuildingMaterials).HasDefaultValueSql("((0))");

                entity.Property(e => e.ParameterSn).HasColumnName("ParameterSN");

                entity.Property(e => e.RecodeDot)
                    .HasColumnName("Recode_dot")
                    .HasMaxLength(256);

                entity.Property(e => e.ReportDot)
                    .HasColumnName("Report_dot")
                    .HasMaxLength(256);

                entity.Property(e => e.SampleDescription).HasMaxLength(256);

                entity.Property(e => e.SampleId).HasColumnName("SampleID");

                entity.Property(e => e.SampleJudge).HasMaxLength(256);

                entity.Property(e => e.SampleName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.SampleSymbol).HasMaxLength(64);

                entity.Property(e => e.SampleUc)
                    .HasMaxLength(256)
                    .HasDefaultValueSql("('d_sampleuc_default')");

                entity.Property(e => e.SampleUnit).HasMaxLength(64);

                entity.Property(e => e.SpecSn).HasColumnName("SpecSN");
            });


            modelBuilder.Entity<ItemItem>(entity =>
            {
                entity.ToTable("Item_Item");

                entity.HasIndex(e => e.ItemId)
                    .HasName("IX_Item_Item")
                    .IsUnique();

                entity.HasIndex(e => e.ItemName)
                    .HasName("IX_Item_Item_1");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AssessItemId)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.CanConsign).HasDefaultValueSql("((0))");

                entity.Property(e => e.ItemDescription).HasMaxLength(256);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ItemUc)
                    .HasColumnName("ItemUC")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.KindId).HasColumnName("KindID");

                entity.Property(e => e.Ord)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SampleSn).HasColumnName("SampleSN");
            });

            modelBuilder.Entity<ItemKind>(entity =>
            {
                entity.ToTable("Item_Kind");

                entity.HasIndex(e => e.KindId)
                    .HasName("IX_Item_Kind")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CanConsign).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCtrl).HasDefaultValueSql("((0))");

                entity.Property(e => e.ItemSn).HasColumnName("ItemSN");

                entity.Property(e => e.KindDescription).HasMaxLength(256);

                entity.Property(e => e.KindId).HasColumnName("KindID");

                entity.Property(e => e.KindName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

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

            modelBuilder.Entity<DpProductionUnitType>(entity =>
            {
                entity.ToTable("DP_ProductionUnitType");

                entity.HasIndex(e => e.ItemId)
                    .HasName("IX_DP_ProductionUnitType");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(64)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<UnitProductionUnit>(entity =>
            {
                entity.HasKey(e => e.ProductionUnitId)
                    .HasName("PK_Public_Unit_ProductionUnit");

                entity.ToTable("Unit_ProductionUnit");

                entity.HasIndex(e => e.ProductionUnitId)
                    .HasName("IX_Unit_ProductionUnit_2");

                entity.HasIndex(e => e.PutOnRecordsPassport)
                    .HasName("IX_Unit_ProductionUnit_3");

                entity.Property(e => e.ProductionUnitId)
                    .HasColumnName("ProductionUnitID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(512);

                entity.Property(e => e.Bindlicences).HasMaxLength(128);

                entity.Property(e => e.Fax)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LegalPerson).HasMaxLength(32);

                entity.Property(e => e.LinkMan).HasMaxLength(32);

                entity.Property(e => e.LinkManPhone)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LinkPhone)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Orders)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductionUnitType)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.PutOnRecordsPassport).HasMaxLength(128);

                entity.Property(e => e.RecordsPassportOrdersPart1)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RecordsPassportOrdersPart2)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RecordsPassportOrdersPart3)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SurfaceFlagPicture).HasColumnType("image");

                entity.Property(e => e.SurfaceFlagText)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DpDelegateQuanUnit>(entity =>
            {
                entity.ToTable("DP_DelegateQuanUnit");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Nam)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Val)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}
