using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblThanhToan")]
    public partial class TblThanhToan
    {
        [Key]
        [StringLength(50)]
        public string MaThanhToan { get; set; }
        [Required]
        [StringLength(50)]
        public string MaBaiDang { get; set; }
        [Required]
        [StringLength(50)]
        public string MaTk { get; set; }
        public int ThanhTien { get; set; }
        [Column(TypeName = "date")]
        public DateTime NgayThanhToan { get; set; }

        [ForeignKey(nameof(MaBaiDang))]
        [InverseProperty(nameof(TblBaiDang.TblThanhToans))]
        public virtual TblBaiDang MaBaiDangNavigation { get; set; }
        [ForeignKey(nameof(MaTk))]
        [InverseProperty(nameof(TblTaiKhoan.TblThanhToans))]
        public virtual TblTaiKhoan MaTkNavigation { get; set; }
    }
}
