using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatdongsanAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BatdongsanAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaiDangController : ControllerBase
    {
        private readonly CoreDbContext _context;
        public BaiDangController(CoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblBaiDang>>> getNew()
        {
            return await _context.TblBaiDangs.OrderByDescending(x => x.MaBaiDang).Take(6).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblBaiDang>>> getVip()
        {
            return await _context.TblBaiDangs.Where(x => x.LoaiBaiDang == "1").OrderByDescending(x => x.MaBaiDang).Take(6).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblBaiDang>> getDetail(string id)
        {
            var product = await _context.TblBaiDangs.FindAsync(id);
            product.LuotXem += 1;
            _context.SaveChanges();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<int> editTT(BaiDangModel post)
        {
            TblBaiDang _post = await _context.TblBaiDangs.FindAsync(post.MaBaiDang);
            _post.TrangThai = post.TrangThai;
            int res;
            try
            {
                res = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        [HttpPost]
        public async Task<int> addPost(BaiDangModel post)
        {
            TblBaiDang _post = new TblBaiDang()
            {
                MaBaiDang = "",
                MaHinhThuc = post.MaHinhThuc,
                MaLoaiBds = post.MaLoaiBds,
                MaXp = post.MaXp,
                MaTk = post.MaTk,
                DiaChiChiTiet = post.DiaChiChiTiet,
                TieuDe = post.TieuDe,
                MoTa = post.MoTa,
                DienTich = post.DienTich,
                MucGia = post.MucGia,
                DonViGia = post.DonViGia,
                HuongNha = post.HuongNha,
                HuongBanCong = post.HuongBanCong,
                SoPhongNgu = post.SoPhongNgu,
                SoPhongTam = post.SoPhongTam,
                SoPhongWc = post.SoPhongWc,
                SoTang = post.SoTang,
                GiayToPhapLy = post.GiayToPhapLy,
                MoTaNoiThat = post.MoTaNoiThat,
                TenLienHe = post.TenLienHe,
                DiaChiLienHe = post.DiaChiLienHe,
                Sdt = post.Sdt,
                Email = post.Email,
                NgayBatDau = post.NgayBatDau,
                NgayKetThuc = post.NgayKetThuc,
                TrangThai = "1",
                LoaiBaiDang = post.LoaiBaiDang,
                LuotXem = 0,
            };
            _context.TblBaiDangs.Add(_post);
            int res;
            try
            {
                res = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            TblBaiDang _baidang = _context.TblBaiDangs.OrderByDescending(x => x.MaBaiDang).FirstOrDefault();
            TblThanhToan _thanhtoan = new TblThanhToan()
            {
                MaThanhToan = "",
                MaBaiDang = _baidang.MaBaiDang,
                MaTk = post.MaTk,
                ThanhTien = post.ThanhTien,
                NgayThanhToan = DateTime.Today,
            };
            try
            {
                _context.TblThanhToans.Add(_thanhtoan);
                TblTaiKhoan _tk = _context.TblTaiKhoans.Where(x => x.MaTk == post.MaTk).SingleOrDefault();
                if (_tk != null)
                {
                    _tk.SoDuTk = _tk.SoDuTk - post.ThanhTien;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        [HttpPost]
        public ResponseModel GetPosts([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            var result = formData["item_group_id"].ToString();
            List<TblBaiDang> _post = null;
            if (result == "all")
            {
                int _skip = (page - 1) * 6;
                response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2").Count();

                _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2").OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                response.Data = _post;
                response.Page = page;
                return response;
            }
            if (result == "ban")
            {
                int _skip = (page - 1) * 6;
                response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1").Count();

                _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1").OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                response.Data = _post;
                response.Page = page;
                return response;
            }
            if (result == "chothue")
            {
                int _skip = (page - 1) * 6;
                response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "2").Count();

                _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "2").OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                response.Data = _post;
                response.Page = page;
                return response;
            }
            if (result != "all" || result != "ban" || result != "chothue")
            {
                int _skip = (page - 1) * 6;
                response.TotalItems = _context.TblBaiDangs.Where(x => x.MaLoaiBds == result).Count();

                _post = _context.TblBaiDangs.Where(x => x.MaLoaiBds == result).OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                response.Data = _post;
                response.Page = page;
                return response;
            }
            return null;
        }

        [HttpPost]
        public ResponseModel GetListPosts([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            var result = formData["id_user"].ToString();
            List<TblBaiDang> _post = null;
            int _skip = (page - 1) * 6;
            response.TotalItems = _context.TblBaiDangs.Where(x => x.MaTk == result).Count();

            _post = _context.TblBaiDangs.Where(x => x.MaTk == result).OrderByDescending(x => x.MaBaiDang).Skip(_skip).Take(6).ToList();

            response.Data = _post;
            response.Page = page;
            return response;

            //return null;
        }

        [HttpPost]
        public ResponseModel GetHistory([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            var result = formData["id_user"].ToString();
            List<TblThanhToan> _post = null;
            int _skip = (page - 1) * 6;
            response.TotalItems = _context.TblThanhToans.Where(x => x.MaTk == result).Count();

            _post = _context.TblThanhToans.Where(x => x.MaTk == result).OrderByDescending(x => x.MaThanhToan).Skip(_skip).Take(6).ToList();

            response.Data = _post;
            response.Page = page;
            return response;

            //return null;
        }

        [HttpPost]
        public ResponseModel getTT([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            var result = formData["trangthai"].ToString();
            List<TblBaiDang> _post = null;
            int _skip = (page - 1) * 10;
            response.TotalItems = _context.TblBaiDangs.Where(x => x.TrangThai == result).Count();

            _post = _context.TblBaiDangs.Where(x => x.TrangThai == result).OrderByDescending(x => x.MaBaiDang).Skip(_skip).Take(10).ToList();

            response.Data = _post;
            response.Page = page;
            return response;

            //return null;
        }


        // GET: api/<BaiDangController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BaiDangController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BaiDangController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BaiDangController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BaiDangController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
