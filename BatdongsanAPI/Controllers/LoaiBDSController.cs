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
    public class LoaiBDSController : ControllerBase
    {
        private readonly CoreDbContext _context;
        public LoaiBDSController(CoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TblLoaiBd>>> GetLoaiBDS(string id)
        {
            var Loai = await _context.TblLoaiBds.Where(x => x.MaHinhThuc == id).ToListAsync();

            if (Loai == null)
            {
                return NotFound();
            }

            return Loai;
        }
        // GET: api/<LoaiBDSController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoaiBDSController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoaiBDSController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LoaiBDSController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoaiBDSController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
