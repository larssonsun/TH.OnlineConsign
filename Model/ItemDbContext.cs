using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace th.onlineconsign.Model
{
    public partial class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemSoureDbContext> options) : base(options)
        { }

        public ItemDbContext(DbContextOptions<ItemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ItemItem> ItemItem { get; set; }
        public virtual DbSet<ItemKind> ItemKind { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

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
        }
    }
}
