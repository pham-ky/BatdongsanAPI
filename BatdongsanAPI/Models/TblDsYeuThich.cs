using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblDsYeuThich")]
    public partial class TblDsYeuThich
    {
        [Key]
        [StringLength(50)]
        public string MaYeuThich { get; set; }
        [Required]
        [StringLength(50)]
        public string MaBaiDang { get; set; }
        [Required]
        [StringLength(50)]
        public string MaTk { get; set; }

        [ForeignKey(nameof(MaBaiDang))]
        [InverseProperty(nameof(TblBaiDang.TblDsYeuThiches))]
        public virtual TblBaiDang MaBaiDangNavigation { get; set; }
        [ForeignKey(nameof(MaTk))]
        [InverseProperty(nameof(TblTaiKhoan.TblDsYeuThiches))]
        public virtual TblTaiKhoan MaTkNavigation { get; set; }
    }
}
