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
    public class TransactionController : ControllerBase
    {
        private readonly IDBEngine db;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IDBEngine DBEngine, ILogger<TransactionController> logger)
        {
            db = DBEngine;
            _logger = logger;
        }

        /// <summary>
        /// Returns all the Transactions.
        /// </summary>
        // GET: api/Transaction
        [HttpGet]
        public IEnumerable<Trnx> Get()
        {
            _logger.LogInformation("Transaction-Get");

            string sql = "SELECT * FROM [Trnx]";
            var results = db.Query<Trnx>(sql).Result;

            return results;

        }

        /// <summary>
        /// Returns all the transactions by accountId.
        /// </summary>
        // GET: api/GetByAccountId
        [HttpGet]
        [Route("GetByAccountId")]
        public IEnumerable<Trnx> Get([FromQuery] int accountId)
        {
            _logger.LogInformation("Transaction-Get");

            string sql = $"SELECT * FROM [Trnx] where AccountId in ({accountId})";
            var results = db.Query<Trnx>(sql).Result;

            return results;

        }

        /// <summary>
        /// Returns all the transactions by between time period.
        /// </summary>
        // GET: api/GetByTimePeriod
        [HttpGet]
        [Route("GetByTimePeriod")]
        public IEnumerable<Trnx> Get([FromQuery] DateTime startDate,DateTime endDate)
        {
            _logger.LogInformation("Transaction-Get");

            string sql = $"SELECT * FROM [Trnx] where RequestDate between '{startDate}' and '{endDate}'";
            var results = db.Query<Trnx>(sql).Result;

            return results;

        }


        /// <summary>
        /// Creates a new Transaction for Adding.
        /// </summary>
        // POST api/AddMoney 
        [HttpPost]
        [Route("AddMoney")]
        public ResponseHeader Post([FromBody] Trnx body)
        {
            _logger.LogInformation("Transaction-Add-Money", body);

            string amount = body.Amount.ToString(CultureInfo.CreateSpecificCulture("en-US"));
            string sql = $"INSERT INTO [Trnx] (Type, AccountId,Amount) VALUES (1,{body.AccountId},'{amount}');";
            string sqlAmount = $"Select Balance From [Account] where Id = {body.AccountId};";

            decimal currentAmount = db.Query<decimal>(sqlAmount).Result.FirstOrDefault();



            string sqlUpdateAmount = $"UPDATE [Account] SET Balance = {currentAmount + body.Amount} where Id = {body.AccountId};";

            //UPDATE amount
            var resultsUpdate = db.Execute(sqlUpdateAmount).Result;

            var results = db.Execute(sql).Result;

            return new ResponseHeader()
            {
                Message = "Success",
                StatusCode = 0
            };


        }
        /// <summary>
        /// Creates a new Transaction for WithDrawing.
        /// </summary>
        // POST api/WithDraw
        [HttpPost]
        [Route("WithDraw")]
        public ResponseHeader WithDraw([FromBody] Trnx body)
        {
            _logger.LogInformation("Customer-Post", body);

            string amount = body.Amount.ToString(CultureInfo.CreateSpecificCulture("en-US"));
            string sql = $"INSERT INTO [Trnx] (Type, AccountId,Amount) VALUES (2,{body.AccountId},'{amount}');";
            var results = db.Execute(sql).Result;

            string sqlAmount = $"Select Balance From [Account] where Id = {body.AccountId};";

            decimal currentAmount = db.Query<decimal>(sqlAmount).Result.FirstOrDefault();

            if (currentAmount < body.Amount)
            {
                return new ResponseHeader()
                {
                    Message = "Insufficient Balance",
                    StatusCode = 1001
                };
            }
            else
            {

                string sqlUpdateAmount = $"UPDATE [Account] SET Balance = {currentAmount - body.Amount} where Id = {body.AccountId};";

                //UPDATE amount
                var resultsUpdate = db.Execute(sqlUpdateAmount).Result;


                return new ResponseHeader()
                {
                    Message = "Success",
                    StatusCode = 0
                };
            }




        }
    }
}
