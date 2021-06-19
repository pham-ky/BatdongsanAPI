using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblLoaiBDS")]
    public partial class TblLoaiBd
    {
        public TblLoaiBd()
        {
            TblBaiDangs = new HashSet<TblBaiDang>();
        }

        [Key]
        [Column("MaLoaiBDS")]
        [StringLength(50)]
        public string MaLoaiBds { get; set; }
        [Column("TenLoaiBDS")]
        [StringLength(250)]
        public string TenLoaiBds { get; set; }
        [StringLength(50)]
        public string MaHinhThuc { get; set; }

        [ForeignKey(nameof(MaHinhThuc))]
        [InverseProperty(nameof(TblHinhThuc.TblLoaiBds))]
        public virtual TblHinhThuc MaHinhThucNavigation { get; set; }
        [InverseProperty(nameof(TblBaiDang.MaLoaiBdsNavigation))]
        public virtual ICollection<TblBaiDang> TblBaiDangs { get; set; }
    }
}
