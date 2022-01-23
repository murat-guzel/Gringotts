using Gringotts_WebApi.Entities;
using Gringotts_WebApi.Entities.Headers;
using Gringotts_WebApi.Entities.Response;
using Gringotts_WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IDBEngine db;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IDBEngine DBEngine, ILogger<CustomerController> logger)
        {
            db = DBEngine;
            _logger = logger;
        }
        /// <summary>
        /// Returns all the Customers.
        /// </summary>
        // GET: api/Customers
        [HttpGet]
        public CustomerResponse Get()
        {
            _logger.LogInformation("Customer-Get");

            string sql = "SELECT * FROM Customer";
            var results = db.Query<Customer>(sql).Result;

            CustomerResponse response = new CustomerResponse();

            if (results.Count>0)
            {
                List<Customer> customers = new List<Customer>();
                foreach (var customer in results)
                {
                    customers.Add(new Customer()
                    {

                        Name = customer.Name,
                        Surname = customer.Surname

                    });
                }

                response.customers = customers;
                response.Header = new ResponseHeader()
                {
                    Message = "Success",
                    StatusCode = 0

                };

            }
            else
            {
                response.Header = new ResponseHeader()
                {
                    Message = "No customers found matching your criteria.",
                    StatusCode = 0

                };
            }


            return response;



        }

        /// <summary>
        /// Creates a new Customer.
        /// </summary>
        // POST api/Customer
        [HttpPost]
        public ResponseHeader Post([FromBody] Customer body)
        {
            _logger.LogInformation("Customer-Post", body);
            

            string sql = $"INSERT INTO Customer (Name, Surname) VALUES ('{body.Name}','{body.Surname}');";
            var results = db.Execute(sql).Result;

            return new ResponseHeader()
            {
                Message = "Success",
                StatusCode = 0
            };

             
        }
    }
}
