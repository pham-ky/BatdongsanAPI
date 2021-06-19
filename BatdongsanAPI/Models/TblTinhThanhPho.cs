using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblTinhThanhPho")]
    public partial class TblTinhThanhPho
    {
        public TblTinhThanhPho()
        {
            TblQuanHuyens = new HashSet<TblQuanHuyen>();
        }

        [Key]
        [Column("MaTP")]
        [StringLength(5)]
        public string MaTp { get; set; }
        [Required]
        [StringLength(100)]
        public string Ten { get; set; }
        [Required]
        [StringLength(30)]
        public string Kieu { get; set; }

        [InverseProperty(nameof(TblQuanHuyen.MaTpNavigation))]
        public virtual ICollection<TblQuanHuyen> TblQuanHuyens { get; set; }
    }
}
