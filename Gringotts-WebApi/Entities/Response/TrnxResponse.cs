using Gringotts_WebApi.Entities.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Entities.Response
{
    public class TrnxResponse
    {
        public int AccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime RequestDate { get; set; }

        public ResponseHeader Header { get; set; }
    }
}
