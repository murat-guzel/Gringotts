using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Entities
{
    public class Account
    { 
        public int CustomerId { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public int AccountType { get; set; }

        public string AccountNumber { get; set; }
    }
}
