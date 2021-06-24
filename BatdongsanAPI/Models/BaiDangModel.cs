using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatdongsanAPI.Models
{
    public class BaiDangModel
    {
        public string MaBaiDang { get; set; }
        public string MaHinhThuc { get; set; }
        public string MaLoaiBds { get; set; }
        public string MaXp { get; set; }
        public string MaTk { get; set; }
        public string DiaChiChiTiet { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public int? DienTich { get; set; }
        public int? MucGia { get; set; }
        public string DonViGia { get; set; }
        public string HuongNha { get; set; }
        public string HuongBanCong { get; set; }
        public int? SoPhongNgu { get; set; }
        public int? SoPhongTam { get; set; }
        public int? SoPhongWc { get; set; }
        public int? SoTang { get; set; }
        public string GiayToPhapLy { get; set; }
        public string MoTaNoiThat { get; set; }
        public string TenLienHe { get; set; }
        public string DiaChiLienHe { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string LoaiBaiDang { get; set; }
        public int ThanhTien { get; set; }
        public string TrangThai { get; set; }
    }
}
