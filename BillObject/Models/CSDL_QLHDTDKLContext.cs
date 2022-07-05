using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BillObject.Models
{
    public partial class CSDL_QLHDTDKLContext : DbContext
    {
        public CSDL_QLHDTDKLContext()
        {
        }

        public CSDL_QLHDTDKLContext(DbContextOptions<CSDL_QLHDTDKLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(local); database = CSDL_QLHDTDKL;uid=sa;pwd=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("ChiTietHoaDon");

                entity.Property(e => e.MaKh)
                    .ValueGeneratedNever()
                    .HasColumnName("MaKH");

                entity.Property(e => e.DiaChiKh)
                    .HasMaxLength(50)
                    .HasColumnName("DiaChiKH");

                entity.Property(e => e.DoiTuongKh)
                    .HasMaxLength(50)
                    .HasColumnName("DoiTuongKH");

                entity.Property(e => e.HoTenKh)
                    .HasMaxLength(50)
                    .HasColumnName("HoTenKH");

                entity.Property(e => e.QuocTich).HasMaxLength(50);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HoaDon");

                entity.Property(e => e.DiaChiKh)
                    .HasMaxLength(50)
                    .HasColumnName("DiaChiKH");

                entity.Property(e => e.DoiTuongKh)
                    .HasMaxLength(50)
                    .HasColumnName("DoiTuongKH");

                entity.Property(e => e.HoTenKh)
                    .HasMaxLength(50)
                    .HasColumnName("HoTenKH");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.QuocTich).HasMaxLength(50);
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("KhachHang");

                entity.Property(e => e.DiaChiKh)
                    .HasMaxLength(50)
                    .HasColumnName("DiaChiKH");

                entity.Property(e => e.DoiTuongKh)
                    .HasMaxLength(50)
                    .HasColumnName("DoiTuongKH");

                entity.Property(e => e.HoTenKh)
                    .HasMaxLength(50)
                    .HasColumnName("HoTenKH");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.QuocTich).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
