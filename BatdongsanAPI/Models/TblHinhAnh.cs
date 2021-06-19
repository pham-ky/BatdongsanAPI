using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblHinhAnh")]
    public partial class TblHinhAnh
    {
        [Key]
        [StringLength(50)]
        public string MaHinhAnh { get; set; }
        [Required]
        [StringLength(50)]
        public string MaBaiDang { get; set; }
        [Required]
        [StringLength(250)]
        public string Url { get; set; }

        [ForeignKey(nameof(MaBaiDang))]
        [InverseProperty(nameof(TblBaiDang.TblHinhAnhs))]
        public virtual TblBaiDang MaBaiDangNavigation { get; set; }
    }
}
