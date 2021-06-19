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
