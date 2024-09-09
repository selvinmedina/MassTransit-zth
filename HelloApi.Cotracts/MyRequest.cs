using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloApi.Cotracts
{
    public class MyRequest
    {
        public Guid Id { get; set; }
        public string RequestBody { get; set; }
    }
}
