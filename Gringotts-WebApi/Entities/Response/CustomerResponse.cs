using Gringotts_WebApi.Entities.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Entities.Response
{
    public class CustomerResponse
    {
        public List<Customer> customers { get; set; }

        public ResponseHeader Header { get; set; }
    }
}
