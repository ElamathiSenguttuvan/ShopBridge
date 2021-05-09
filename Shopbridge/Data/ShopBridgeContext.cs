using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Shopbridge.Data
{
    public partial class ShopBridgeContext : DbContext
    {
        public ShopBridgeContext()
        {
        }

        public ShopBridgeContext(DbContextOptions<ShopBridgeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StockInfo> StockInfos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ShopBridgeDB"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<StockInfo>(entity =>
            {
                entity.HasKey(e => e.StockId)
                    .HasName("PK__StockInf__2C83A9C2D1C9ADA7");

                entity.ToTable("StockInfo");

                entity.Property(e => e.StockId).ValueGeneratedNever();

                entity.Property(e => e.AvailabilityStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
