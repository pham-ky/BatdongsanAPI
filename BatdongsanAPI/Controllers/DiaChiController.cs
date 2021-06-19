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
    public class DiaChiController : ControllerBase
    {
        private readonly CoreDbContext _context;
        public DiaChiController(CoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTinhThanhPho>>> GetTinhTP()
        {
            return await _context.TblTinhThanhPhos.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblQuanHuyen>>> GetQH()
        {
            return await _context.TblQuanHuyens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TblQuanHuyen>>> GetQH(string id)
        {   
            var QH = await _context.TblQuanHuyens.Where(x => x.MaTp==id).ToListAsync();

            if (QH == null)
            {
                return NotFound();
            }

            return QH;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblXaPhuong>>> GetXP()
        {
            return await _context.TblXaPhuongs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TblXaPhuong>>> GetXP(string id)
        {
            var XP = await _context.TblXaPhuongs.Where(x => x.MaQh == id).ToListAsync();

            if (XP == null)
            {
                return NotFound();
            }

            return XP;
        }

        // GET: api/<DiaChiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DiaChiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DiaChiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DiaChiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DiaChiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
