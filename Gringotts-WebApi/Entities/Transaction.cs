﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Entities
{
    public class Transaction
    {
        public string Type { get; set; }

        public int AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
