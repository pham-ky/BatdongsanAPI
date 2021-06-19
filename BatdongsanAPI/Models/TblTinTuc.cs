using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BatdongsanAPI.Models
{
    [Table("tblTinTuc")]
    public partial class TblTinTuc
    {
        [Key]
        [StringLength(50)]
        public string MaTinTuc { get; set; }
        [StringLength(250)]
        public string TieuDe { get; set; }
        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }
    }
}
