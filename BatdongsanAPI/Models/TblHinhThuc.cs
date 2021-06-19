using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblHinhThuc")]
    public partial class TblHinhThuc
    {
        public TblHinhThuc()
        {
            TblBaiDangs = new HashSet<TblBaiDang>();
            TblLoaiBds = new HashSet<TblLoaiBd>();
        }

        [Key]
        [StringLength(50)]
        public string MaHinhThuc { get; set; }
        [StringLength(250)]
        public string TenHinhThuc { get; set; }

        [InverseProperty(nameof(TblBaiDang.MaHinhThucNavigation))]
        public virtual ICollection<TblBaiDang> TblBaiDangs { get; set; }
        [InverseProperty(nameof(TblLoaiBd.MaHinhThucNavigation))]
        public virtual ICollection<TblLoaiBd> TblLoaiBds { get; set; }
    }
}
