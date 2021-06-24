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
    public class UserController : ControllerBase
    {

        private readonly CoreDbContext _context;
        public UserController(CoreDbContext context)
        {
            _context = context;
        }

        //login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] TblTaiKhoan user)
        {
            var us = user = await _context.TblTaiKhoans.FirstOrDefaultAsync(u => u.TaiKhoan == user.TaiKhoan && u.MatKhau == user.MatKhau && u.LoaiTk == "user");
            return Ok(us);
        }
        [HttpPost]
        public async Task<IActionResult> LoginAdmin([FromBody] TblTaiKhoan user)
        {
            var us = user = await _context.TblTaiKhoans.FirstOrDefaultAsync(u => u.TaiKhoan == user.TaiKhoan && u.MatKhau == user.MatKhau && u.LoaiTk == "admin");
            return Ok(us);
        }
        // GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTaiKhoan>>> GetAll()
        {
            return await _context.TblTaiKhoans.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTaiKhoan>>> GetAllUser()
        {
            return await _context.TblTaiKhoans.Where(x=>x.LoaiTk=="user").ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TblTaiKhoan>>> getLoai(string id)
        {
            return await _context.TblTaiKhoans.Where(x => x.LoaiTk == id.ToLower()).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblTaiKhoan>> getUser(string id)
        {
            var user = await _context.TblTaiKhoans.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<int> addAdmin(User _user)
        {
            TblTaiKhoan _tk = new TblTaiKhoan()
            {
                MaTk = "",
                TaiKhoan = _user.TaiKhoan,
                MatKhau = _user.MatKhau,
                SoDuTk = 0,
                LoaiTk = "admin",
                TrangThai = "1"
            };
            _context.TblTaiKhoans.Add(_tk);
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

        [HttpGet("{id}")]
        public async Task<int> TrangThai(string id)
        {
            TblTaiKhoan _tk = await _context.TblTaiKhoans.FindAsync(id);
            if(_tk.TrangThai == "1")
            {
                _tk.TrangThai = "0";
            }
            else
            if (_tk.TrangThai == "0")
            {
                _tk.TrangThai = "1";
            }
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
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
