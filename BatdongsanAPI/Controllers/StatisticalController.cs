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
    public class StatisticalController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public StatisticalController(CoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Object Post()
        {
            int x = 0;
            int[] quantity = new int[5];
            dynamic[] payment = new dynamic[5];
            var totalpay = 0;
            

            var totalPost = _context.TblBaiDangs.Where(x => x.NgayBatDau.Month == DateTime.Now.Month).Count();
            var _pay = _context.TblThanhToans.Where(x => x.NgayThanhToan.Month == DateTime.Now.Month);
            foreach(var p in _pay)
            {
                totalpay += p.ThanhTien;
            }


            for (int i = 4; i >= 0; i--)
            {
                int S = 0;
                var previousDate = DateTime.Now.AddMonths(-i);
                quantity[x] = _context.TblBaiDangs.Where(x => x.NgayBatDau.Month == previousDate.Month).Count();
                var _pay1 = _context.TblThanhToans.Where(x => x.NgayThanhToan.Month == previousDate.Month);
                foreach (var p in _pay1)
                {
                    S += p.ThanhTien;
                }
                payment[x] = S;
                x++;
            }
            return new { quantity, totalPost, payment, totalpay};
        }


        [HttpGet("{x}")]
        public async Task<ActionResult<IEnumerable<TblBaiDang>>> View(int x)
        {
            return await _context.TblBaiDangs.OrderByDescending(x => x.LuotXem).Take(x).ToListAsync();
        }



        // GET: api/<StatisticalController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StatisticalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StatisticalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatisticalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatisticalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
