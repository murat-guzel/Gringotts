using Gringotts_WebApi.Entities;
using Gringotts_WebApi.Entities.Headers;
using Gringotts_WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IDBEngine db;

        public AccountController(IDBEngine DBEngine)
        {
            db = DBEngine;
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
        /// Creates a new Account.
        /// </summary>
        // POST api/Account
        [HttpPost]
        public ResponseHeader Post([FromBody] Account body)
        {

            string sql = $"INSERT INTO Account (CustomerId, Currency,AccountNumber,AccountType,Balance) VALUES ({body.CustomerId},'{body.Currency}','{body.AccountNumber}',{body.AccountType},{body.Balance});";
            var results = db.Execute(sql).Result;

            return new ResponseHeader()
            {
                Message = "Success",
                Status = 0
            };


        }
    }
}
