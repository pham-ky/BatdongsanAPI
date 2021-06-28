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

        public async Task<ActionResult<IEnumerable<TblBaiDang>>> getTuongTu()
        {
            return await _context.TblBaiDangs.OrderByDescending(x => x.MaBaiDang).Take(8).ToListAsync();
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
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            var loai = formData["LoaiBds"].ToString();
            var hinhthuc = formData["HinhThuc"].ToString();
            var tinh = formData["Tinh"].ToString();
            var huyen = formData["Huyen"].ToString();
            var xa = formData["Xa"].ToString();
            var huong = formData["Huong"].ToString();
            var min = int.Parse(formData["min"].ToString());
            var max = int.Parse(formData["max"].ToString());
            List<TblBaiDang> _post = null;
            int _skip = (page - 1) * 6;
            int count = 0;
            if (loai == "" && huong == "" && tinh == "" && huyen == "" && xa == "")
            {
                if (hinhthuc == "all")
                {
                    response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2").Count();

                    _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2").OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (hinhthuc == "ban")
                {
                    response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1").Count();

                    _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1").OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (hinhthuc == "chothue")
                {
                    response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "2").Count();

                    _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "2").OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (hinhthuc != "all" && hinhthuc != "ban" && hinhthuc != "chothue")
                {
                    response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == hinhthuc).Count();

                    _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == hinhthuc).OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
            }
            if (hinhthuc == "" || loai == "" || huong == "")
            {
                if (xa != "")
                {
                    response.TotalItems = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Count();
                    _post = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (huyen != "")
                {
                    var xaphuong = from h in _context.TblQuanHuyens
                                   where h.MaQh == huyen
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post.OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (tinh != "")
                {
                    var xaphuong = from t in _context.TblTinhThanhPhos
                                   where t.MaTp == tinh
                                   join h in _context.TblQuanHuyens on t.MaTp equals h.MaTp
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }

            }
            if (loai != "" && huong == "")
            {
                if (xa != "")
                {
                    response.TotalItems = _context.TblBaiDangs
                        .Where(x => x.MaLoaiBds == loai)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Count();
                    _post = _context.TblBaiDangs
                        .Where(x => x.MaLoaiBds == loai)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (huyen != "")
                {
                    var xaphuong = from h in _context.TblQuanHuyens
                                   where h.MaQh == huyen
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (tinh != "")
                {
                    var xaphuong = from t in _context.TblTinhThanhPhos
                                   where t.MaTp == tinh
                                   join h in _context.TblQuanHuyens on t.MaTp equals h.MaTp
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
            }
            if (loai != "" && huong != "")
            {
                if (xa != "")
                {
                    response.TotalItems = _context.TblBaiDangs
                        .Where(x => x.MaLoaiBds == loai)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Where(x => x.HuongNha == huong)
                        .Count();
                    _post = _context.TblBaiDangs
                        .Where(x => x.MaLoaiBds == loai)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Where(x => x.HuongNha == huong)
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (huyen != "")
                {
                    var xaphuong = from h in _context.TblQuanHuyens
                                   where h.MaQh == huyen
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (tinh != "")
                {
                    var xaphuong = from t in _context.TblTinhThanhPhos
                                   where t.MaTp == tinh
                                   join h in _context.TblQuanHuyens on t.MaTp equals h.MaTp
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaLoaiBds == loai)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
            }
            if (loai == "" && huong != "")
            {
                if (xa != "")
                {
                    response.TotalItems = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Where(x => x.HuongNha == huong)
                        .Count();
                    _post = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Where(x => x.HuongNha == huong)
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (huyen != "")
                {
                    var xaphuong = from h in _context.TblQuanHuyens
                                   where h.MaQh == huyen
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (tinh != "")
                {
                    var xaphuong = from t in _context.TblTinhThanhPhos
                                   where t.MaTp == tinh
                                   join h in _context.TblQuanHuyens on t.MaTp equals h.MaTp
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2")
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
            }
            if (hinhthuc != "" && huong == "")
            {
                if (xa != "")
                {
                    response.TotalItems = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == hinhthuc)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Count();
                    _post = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == hinhthuc)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (huyen != "")
                {
                    var xaphuong = from h in _context.TblQuanHuyens
                                   where h.MaQh == huyen
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (tinh != "")
                {
                    var xaphuong = from t in _context.TblTinhThanhPhos
                                   where t.MaTp == tinh
                                   join h in _context.TblQuanHuyens on t.MaTp equals h.MaTp
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
            }
            if (hinhthuc != "" && huong != "")
            {
                if (xa != "")
                {
                    response.TotalItems = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == hinhthuc)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Where(x => x.HuongNha == huong)
                        .Count();
                    _post = _context.TblBaiDangs
                        .Where(x => x.MaHinhThuc == hinhthuc)
                        .Where(x => x.MaXp == xa)
                        .Where(x => x.DienTich <= max && x.DienTich >= min)
                        .Where(x => x.HuongNha == huong)
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (huyen != "")
                {
                    var xaphuong = from h in _context.TblQuanHuyens
                                   where h.MaQh == huyen
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post
                        .OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6)
                        .ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
                if (tinh != "")
                {
                    var xaphuong = from t in _context.TblTinhThanhPhos
                                   where t.MaTp == tinh
                                   join h in _context.TblQuanHuyens on t.MaTp equals h.MaTp
                                   join x in _context.TblXaPhuongs on h.MaQh equals x.MaQh
                                   select x.MaXp;
                    foreach (var xp in xaphuong)
                    {
                        count += _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .Count();
                        var p = _context.TblBaiDangs
                            .Where(x => x.MaHinhThuc == hinhthuc)
                            .Where(x => x.MaXp == xp)
                            .Where(x => x.DienTich <= max && x.DienTich >= min)
                            .Where(x => x.HuongNha == huong)
                            .ToList();

                        if (_post == null)
                        {
                            _post = p;
                        }
                        else
                            _post.AddRange(p);
                    }
                    response.TotalItems = count;
                    _post = _post.
                        OrderByDescending(x => x.LoaiBaiDang)
                        .Skip(_skip)
                        .Take(6).
                        ToList();
                    response.Data = _post;
                    response.Page = page;
                    return response;
                }
            }
            response.TotalItems = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2").Count();

            _post = _context.TblBaiDangs.Where(x => x.MaHinhThuc == "1" || x.MaHinhThuc == "2").OrderByDescending(x => x.LoaiBaiDang).Skip(_skip).Take(6).ToList();

            response.Data = _post;
            response.Page = page;
            return response;
            //return null;
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
