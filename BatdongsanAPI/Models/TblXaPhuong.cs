using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblXaPhuong")]
    public partial class TblXaPhuong
    {
        public TblXaPhuong()
        {
            TblBaiDangs = new HashSet<TblBaiDang>();
        }

        [Key]
        [Column("MaXP")]
        [StringLength(5)]
        public string MaXp { get; set; }
        [Required]
        [StringLength(100)]
        public string Ten { get; set; }
        [Required]
        [StringLength(30)]
        public string Kieu { get; set; }
        [Required]
        [Column("MaQH")]
        [StringLength(5)]
        public string MaQh { get; set; }

        [ForeignKey(nameof(MaQh))]
        [InverseProperty(nameof(TblQuanHuyen.TblXaPhuongs))]
        public virtual TblQuanHuyen MaQhNavigation { get; set; }
        [InverseProperty(nameof(TblBaiDang.MaXpNavigation))]
        public virtual ICollection<TblBaiDang> TblBaiDangs { get; set; }
    }
}
