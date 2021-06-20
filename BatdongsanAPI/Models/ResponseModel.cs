using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatdongsanAPI.Models
{
    public class ResponseModel
    {
        public long TotalItems { get; set; }
        public int Page { get; set; }
        public dynamic Data { get; set; }
    }
}
