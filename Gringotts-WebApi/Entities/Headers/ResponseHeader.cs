using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Entities.Headers
{
    public class ResponseHeader
    {
        public int Status { get; set; }

        public string Message { get; set; }
    }
}
