using BatdongsanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BatdongsanAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NapTienController : ControllerBase
    {
        private readonly CoreDbContext _context;
        public NapTienController(CoreDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ResponseModel ViewNap([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            List<TblNapTien> _post = null;
            int _skip = (page - 1) * 10;
            response.TotalItems = _context.TblNapTiens.Count();

            _post = _context.TblNapTiens.OrderByDescending(x => x.MaNap).Skip(_skip).Take(10).ToList();

            response.Data = _post;
            response.Page = page;
            return response;

        }

        [HttpPost]
        public async Task<int> Nap(NapModel nap)
        {
            TblNapTien _nap = new TblNapTien()
            {
                MaNap = "",
                MaTk = nap.MaTk,
                SoTienNap = nap.SoTienNap,
                HinhThuc = nap.HinhThuc,
                NgayNap = DateTime.Today
            };
            _context.TblNapTiens.Add(_nap);
            int res;
            try
            {
                res = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            try
            {
                TblTaiKhoan _tk = _context.TblTaiKhoans.Where(x => x.MaTk == nap.MaTk).SingleOrDefault();
                if (_tk != null)
                {
                    _tk.SoDuTk = _tk.SoDuTk + nap.SoTienNap;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }
        // GET: api/<NapTienController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NapTienController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NapTienController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NapTienController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NapTienController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
