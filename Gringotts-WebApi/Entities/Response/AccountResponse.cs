using Gringotts_WebApi.Entities.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Entities.Response
{
    public class AccountResponse
    {
        public int CustomerId { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public int AccountType { get; set; }

        public string AccountNumber { get; set; }

        public ResponseHeader Header { get; set; }
    }
}
