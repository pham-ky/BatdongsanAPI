using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblTaiKhoan")]
    public partial class TblTaiKhoan
    {
        public TblTaiKhoan()
        {
            TblBaiDangs = new HashSet<TblBaiDang>();
            TblDsYeuThiches = new HashSet<TblDsYeuThich>();
            TblNapTiens = new HashSet<TblNapTien>();
            TblThanhToans = new HashSet<TblThanhToan>();
        }

        [Key]
        [StringLength(50)]
        public string MaTk { get; set; }
        [Required]
        [StringLength(50)]
        public string TaiKhoan { get; set; }
        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }
        [StringLength(150)]
        public string HoTen { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }
        [StringLength(250)]
        public string DiaChi { get; set; }
        [StringLength(50)]
        public string Sdt { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(150)]
        public string Viber { get; set; }
        [StringLength(150)]
        public string Telegram { get; set; }
        [StringLength(150)]
        public string Skype { get; set; }
        [StringLength(250)]
        public string Facebook { get; set; }
        [StringLength(250)]
        public string Website { get; set; }
        public int SoDuTk { get; set; }
        //[Required]
        [StringLength(50)]
        public string LoaiTk { get; set; }
        //[Required]
        [StringLength(50)]
        public string TrangThai { get; set; }

        [InverseProperty(nameof(TblBaiDang.MaTkNavigation))]
        public virtual ICollection<TblBaiDang> TblBaiDangs { get; set; }
        [InverseProperty(nameof(TblDsYeuThich.MaTkNavigation))]
        public virtual ICollection<TblDsYeuThich> TblDsYeuThiches { get; set; }
        [InverseProperty(nameof(TblNapTien.MaTkNavigation))]
        public virtual ICollection<TblNapTien> TblNapTiens { get; set; }
        [InverseProperty(nameof(TblThanhToan.MaTkNavigation))]
        public virtual ICollection<TblThanhToan> TblThanhToans { get; set; }
    }
}
