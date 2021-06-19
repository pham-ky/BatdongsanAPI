using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblQuanHuyen")]
    public partial class TblQuanHuyen
    {
        public TblQuanHuyen()
        {
            TblXaPhuongs = new HashSet<TblXaPhuong>();
        }

        [Key]
        [Column("MaQH")]
        [StringLength(5)]
        public string MaQh { get; set; }
        [Required]
        [StringLength(100)]
        public string Ten { get; set; }
        [Required]
        [StringLength(30)]
        public string Kieu { get; set; }
        [Required]
        [Column("MaTP")]
        [StringLength(5)]
        public string MaTp { get; set; }

        [ForeignKey(nameof(MaTp))]
        [InverseProperty(nameof(TblTinhThanhPho.TblQuanHuyens))]
        public virtual TblTinhThanhPho MaTpNavigation { get; set; }
        [InverseProperty(nameof(TblXaPhuong.MaQhNavigation))]
        public virtual ICollection<TblXaPhuong> TblXaPhuongs { get; set; }
    }
}
