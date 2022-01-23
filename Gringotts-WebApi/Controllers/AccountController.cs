using Gringotts_WebApi.Entities;
using Gringotts_WebApi.Entities.Headers;
using Gringotts_WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IDBEngine db;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IDBEngine DBEngine, ILogger<AccountController> logger)
        {
            db = DBEngine;
            _logger = logger;
        }
        /// <summary>
        /// Returns all the Accounts.
        /// </summary>
        // GET: api/Account
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            string sql = "SELECT * FROM Account";
            var results = db.Query<Account>(sql).Result;

            return results;

        }

        /// <summary>
        /// Returns all the Accounts by customerId.
        /// </summary>
        // GET: api/GetByCustomerId
        [HttpGet]
        [Route("GetByCustomerId")]
        public IEnumerable<Account> Get([FromQuery] int customerId)
        {
            _logger.LogInformation("Account-Get-CustomerId");

            string sql = $"SELECT * FROM [Account] where CustomerId in ({customerId})";
            var results = db.Query<Account>(sql).Result;

            return results;

        }

        /// <summary>
        /// Creates a new Account.
        /// </summary>
        // POST api/Account
        [HttpPost]
        public ResponseHeader Post([FromBody] Account body)
        {
            string balace = body.Balance.ToString(CultureInfo.CreateSpecificCulture("en-US"));
            string sql = $"INSERT INTO Account (CustomerId, Currency,AccountNumber,AccountType,Balance) VALUES ({body.CustomerId},'{body.Currency}','{body.AccountNumber}',{body.AccountType},'{balace}');";
            var results = db.Execute(sql).Result;

            return new ResponseHeader()
            {
                Message = "Success",
                StatusCode = 0
            };


        }
    }
}
