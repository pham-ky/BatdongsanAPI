using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblNapTien")]
    public partial class TblNapTien
    {
        [Key]
        [StringLength(50)]
        public string MaNap { get; set; }
        [Required]
        [StringLength(50)]
        public string MaTk { get; set; }
        public int SoTienNap { get; set; }
        [Required]
        [StringLength(150)]
        public string HinhThuc { get; set; }
        [Column(TypeName = "date")]
        public DateTime NgayNap { get; set; }

        [ForeignKey(nameof(MaTk))]
        [InverseProperty(nameof(TblTaiKhoan.TblNapTiens))]
        public virtual TblTaiKhoan MaTkNavigation { get; set; }
    }
}
