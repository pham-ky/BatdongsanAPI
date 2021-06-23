using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblBaiDang")]
    public partial class TblBaiDang
    {
        public TblBaiDang()
        {
            TblDsYeuThiches = new HashSet<TblDsYeuThich>();
            TblHinhAnhs = new HashSet<TblHinhAnh>();
            TblThanhToans = new HashSet<TblThanhToan>();
        }

        [Key]
        [StringLength(50)]
        public string MaBaiDang { get; set; }
        [StringLength(50)]
        public string MaHinhThuc { get; set; }
        [Column("MaLoaiBDS")]
        [StringLength(50)]
        public string MaLoaiBds { get; set; }
        [Column("MaXP")]
        [StringLength(5)]
        public string MaXp { get; set; }
        [StringLength(50)]
        public string MaTk { get; set; }
        [StringLength(250)]
        public string DiaChiChiTiet { get; set; }
        [StringLength(250)]
        public string TieuDe { get; set; }
        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }
        public int? DienTich { get; set; }
        public int? MucGia { get; set; }
        [StringLength(150)]
        public string DonViGia { get; set; }
        [StringLength(150)]
        public string HuongNha { get; set; }
        [StringLength(150)]
        public string HuongBanCong { get; set; }
        public int? SoPhongNgu { get; set; }
        public int? SoPhongTam { get; set; }
        [Column("SoPhongWC")]
        public int? SoPhongWc { get; set; }
        public int? SoTang { get; set; }
        [StringLength(250)]
        public string GiayToPhapLy { get; set; }
        [Column(TypeName = "ntext")]
        public string MoTaNoiThat { get; set; }
        [StringLength(150)]
        public string TenLienHe { get; set; }
        [StringLength(250)]
        public string DiaChiLienHe { get; set; }
        [StringLength(50)]
        public string Sdt { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "date")]
        public DateTime NgayBatDau { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }
        [StringLength(50)]
        public string TrangThai { get; set; }
        [StringLength(50)]
        public string LoaiBaiDang { get; set; }
        public int? LuotXem { get; set; }

        [ForeignKey(nameof(MaHinhThuc))]
        [InverseProperty(nameof(TblHinhThuc.TblBaiDangs))]
        public virtual TblHinhThuc MaHinhThucNavigation { get; set; }
        [ForeignKey(nameof(MaLoaiBds))]
        [InverseProperty(nameof(TblLoaiBd.TblBaiDangs))]
        public virtual TblLoaiBd MaLoaiBdsNavigation { get; set; }
        [ForeignKey(nameof(MaTk))]
        [InverseProperty(nameof(TblTaiKhoan.TblBaiDangs))]
        public virtual TblTaiKhoan MaTkNavigation { get; set; }
        [ForeignKey(nameof(MaXp))]
        [InverseProperty(nameof(TblXaPhuong.TblBaiDangs))]
        public virtual TblXaPhuong MaXpNavigation { get; set; }
        [InverseProperty(nameof(TblDsYeuThich.MaBaiDangNavigation))]
        public virtual ICollection<TblDsYeuThich> TblDsYeuThiches { get; set; }
        [InverseProperty(nameof(TblHinhAnh.MaBaiDangNavigation))]
        public virtual ICollection<TblHinhAnh> TblHinhAnhs { get; set; }
        [InverseProperty(nameof(TblThanhToan.MaBaiDangNavigation))]
        public virtual ICollection<TblThanhToan> TblThanhToans { get; set; }
    }
}
