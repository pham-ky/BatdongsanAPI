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
    public class HinhThucController : ControllerBase
    {

        private readonly CoreDbContext _context;
        public HinhThucController(CoreDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblHinhThuc>>> GetAll()
        {
            return await _context.TblHinhThucs.Take(2).ToListAsync();
        }

        // GET: api/<HinhThucController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HinhThucController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HinhThucController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HinhThucController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HinhThucController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
