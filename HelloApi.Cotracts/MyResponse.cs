using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloApi.Cotracts
{
    public class MyResponse
    {
        public string ResponseContent { get; set; }
        public Guid InitialRequestId { get; set; }
    }
}
