using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BatdongsanAPI.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBaiDang> TblBaiDangs { get; set; }
        public virtual DbSet<TblDsYeuThich> TblDsYeuThiches { get; set; }
        public virtual DbSet<TblHinhAnh> TblHinhAnhs { get; set; }
        public virtual DbSet<TblHinhThuc> TblHinhThucs { get; set; }
        public virtual DbSet<TblLoaiBd> TblLoaiBds { get; set; }
        public virtual DbSet<TblNapTien> TblNapTiens { get; set; }
        public virtual DbSet<TblQuanHuyen> TblQuanHuyens { get; set; }
        public virtual DbSet<TblTaiKhoan> TblTaiKhoans { get; set; }
        public virtual DbSet<TblThanhToan> TblThanhToans { get; set; }
        public virtual DbSet<TblTinTuc> TblTinTucs { get; set; }
        public virtual DbSet<TblTinhThanhPho> TblTinhThanhPhos { get; set; }
        public virtual DbSet<TblXaPhuong> TblXaPhuongs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=QLBatdongsan;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<TblBaiDang>(entity =>
            {
                entity.Property(e => e.MaBaiDang).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.LoaiBaiDang).IsUnicode(false);

                entity.Property(e => e.MaHinhThuc).IsUnicode(false);

                entity.Property(e => e.MaLoaiBds).IsUnicode(false);

                entity.Property(e => e.MaTk).IsUnicode(false);

                entity.Property(e => e.MaXp).IsUnicode(false);

                entity.Property(e => e.Sdt).IsUnicode(false);

                entity.Property(e => e.TrangThai).IsUnicode(false);

                entity.HasOne(d => d.MaHinhThucNavigation)
                    .WithMany(p => p.TblBaiDangs)
                    .HasForeignKey(d => d.MaHinhThuc)
                    .HasConstraintName("FK_tblBaiDang_tblHinhThuc");

                entity.HasOne(d => d.MaLoaiBdsNavigation)
                    .WithMany(p => p.TblBaiDangs)
                    .HasForeignKey(d => d.MaLoaiBds)
                    .HasConstraintName("FK_tblBaiDang_tblLoaiBDS");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithMany(p => p.TblBaiDangs)
                    .HasForeignKey(d => d.MaTk)
                    .HasConstraintName("FK_tblBaiDang_tblTaiKhoan");

                entity.HasOne(d => d.MaXpNavigation)
                    .WithMany(p => p.TblBaiDangs)
                    .HasForeignKey(d => d.MaXp)
                    .HasConstraintName("FK_tblBaiDang_tblXaPhuong");
            });

            modelBuilder.Entity<TblDsYeuThich>(entity =>
            {
                entity.Property(e => e.MaYeuThich).IsUnicode(false);

                entity.Property(e => e.MaBaiDang).IsUnicode(false);

                entity.Property(e => e.MaTk).IsUnicode(false);

                entity.HasOne(d => d.MaBaiDangNavigation)
                    .WithMany(p => p.TblDsYeuThiches)
                    .HasForeignKey(d => d.MaBaiDang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDsYeuThich_tblBaiDang");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithMany(p => p.TblDsYeuThiches)
                    .HasForeignKey(d => d.MaTk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDsYeuThich_tblTaiKhoan");
            });

            modelBuilder.Entity<TblHinhAnh>(entity =>
            {
                entity.Property(e => e.MaHinhAnh).IsUnicode(false);

                entity.Property(e => e.MaBaiDang).IsUnicode(false);

                entity.Property(e => e.Url).IsUnicode(false);

                entity.HasOne(d => d.MaBaiDangNavigation)
                    .WithMany(p => p.TblHinhAnhs)
                    .HasForeignKey(d => d.MaBaiDang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblHinhAnh_tblBaiDang");
            });

            modelBuilder.Entity<TblHinhThuc>(entity =>
            {
                entity.Property(e => e.MaHinhThuc).IsUnicode(false);
            });

            modelBuilder.Entity<TblLoaiBd>(entity =>
            {
                entity.Property(e => e.MaLoaiBds).IsUnicode(false);

                entity.Property(e => e.MaHinhThuc).IsUnicode(false);

                entity.HasOne(d => d.MaHinhThucNavigation)
                    .WithMany(p => p.TblLoaiBds)
                    .HasForeignKey(d => d.MaHinhThuc)
                    .HasConstraintName("FK_tblLoaiBDS_tblHinhThuc");
            });

            modelBuilder.Entity<TblNapTien>(entity =>
            {
                entity.Property(e => e.MaNap).IsUnicode(false);

                entity.Property(e => e.MaTk).IsUnicode(false);

                entity.HasOne(d => d.MaTkNavigation)
                    .WithMany(p => p.TblNapTiens)
                    .HasForeignKey(d => d.MaTk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblNapTien_tblTaiKhoan");
            });

            modelBuilder.Entity<TblQuanHuyen>(entity =>
            {
                entity.HasKey(e => e.MaQh)
                    .HasName("PK__tblQuanH__2725F8568455B6D2");

                entity.Property(e => e.MaQh).IsUnicode(false);

                entity.Property(e => e.MaTp).IsUnicode(false);

                entity.HasOne(d => d.MaTpNavigation)
                    .WithMany(p => p.TblQuanHuyens)
                    .HasForeignKey(d => d.MaTp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblQuanHuyen_tblTinhThanhPho");
            });

            modelBuilder.Entity<TblTaiKhoan>(entity =>
            {
                entity.Property(e => e.MaTk).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Facebook).IsUnicode(false);

                entity.Property(e => e.LoaiTk).IsUnicode(false);

                entity.Property(e => e.MatKhau).IsUnicode(false);

                entity.Property(e => e.Sdt).IsUnicode(false);

                entity.Property(e => e.Skype).IsUnicode(false);

                entity.Property(e => e.TaiKhoan).IsUnicode(false);

                entity.Property(e => e.Telegram).IsUnicode(false);

                entity.Property(e => e.TrangThai).IsUnicode(false);

                entity.Property(e => e.Viber).IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);
            });

            modelBuilder.Entity<TblThanhToan>(entity =>
            {
                entity.Property(e => e.MaThanhToan).IsUnicode(false);

                entity.Property(e => e.MaBaiDang).IsUnicode(false);

                entity.Property(e => e.MaTk).IsUnicode(false);

                entity.HasOne(d => d.MaBaiDangNavigation)
                    .WithMany(p => p.TblThanhToans)
                    .HasForeignKey(d => d.MaBaiDang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblThanhToan_tblBaiDang");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithMany(p => p.TblThanhToans)
                    .HasForeignKey(d => d.MaTk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblThanhToan_tblTaiKhoan");
            });

            modelBuilder.Entity<TblTinTuc>(entity =>
            {
                entity.Property(e => e.MaTinTuc).IsUnicode(false);
            });

            modelBuilder.Entity<TblTinhThanhPho>(entity =>
            {
                entity.HasKey(e => e.MaTp)
                    .HasName("PK__tblTinhT__2725007DD9B71E71");

                entity.Property(e => e.MaTp).IsUnicode(false);
            });

            modelBuilder.Entity<TblXaPhuong>(entity =>
            {
                entity.HasKey(e => e.MaXp)
                    .HasName("PK__tblXaPhu__272520F8E251310B");

                entity.Property(e => e.MaXp).IsUnicode(false);

                entity.Property(e => e.MaQh).IsUnicode(false);

                entity.HasOne(d => d.MaQhNavigation)
                    .WithMany(p => p.TblXaPhuongs)
                    .HasForeignKey(d => d.MaQh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblXaPhuong_tblQuanHuyen");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
