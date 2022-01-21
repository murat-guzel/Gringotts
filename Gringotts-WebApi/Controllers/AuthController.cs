using Gringotts_WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IDBEngine db;
        private readonly ICryptoEngine crypto;

        public AuthController(IConfiguration Configuration, IDBEngine DBEngine, ICryptoEngine CryptoEngine)
        {
            config = Configuration;
            db = DBEngine;
            crypto = CryptoEngine;
        }

        public class LoginBody
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /// <summary>
        /// Login with username and password. No previous authorization required.
        /// </summary>
        [HttpPost]
        public JObject Login([FromBody] LoginBody body)
        {
            string username = body.Username.ToLower();

            string hash = db.Value<string>("SELECT Hash FROM [User] WHERE LOWER(username)=@username", new { username }).Result;

            if (!string.IsNullOrEmpty(hash) && crypto.HashCheck(hash, body.Password))
            {
                JObject session = LoadSession(username);
                session["token"] = GenerateJwtToken(session);

                return session;
            }

            return new JObject("Wrong information");
        }

        public class RegisterBody
        {
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /// <summary>
        /// Register with username, email, name and password. No previous authorization required.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterBody body)
        {
            string username = body.Username.ToLower();
            string email = body.Email.ToLower();

            if (await db.Value<int>("SELECT COUNT(*) FROM [User] WHERE LOWER(username)=@username OR LOWER(email)=@email", new { username, email }) > 0)
                return Unauthorized("error.unavailable");

            await db.Execute("INSERT INTO [User] (Username, Hash, Email) VALUES (@username, @hash, @email)", new { username, hash = crypto.Hash(body.Password), email });

            return Ok();
        }

        /// <summary>
        /// Returns the session data for this user.
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Session()
        {
            return Ok(LoadSession());
        }

        private  JObject LoadSession(string username = "")
        {
            if (username == "")
                username = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return db.Json("SELECT Id, Username, Email FROM [User] WHERE LOWER(username)=@username", new { username = username.ToLower() }).Result;
        }

        private string GenerateJwtToken(JObject session)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, session["Username"].ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, session["Id"].ToString())
            };

            dynamic JwtConfig = new
            {
                Issuer = config.GetSection("JWT:Issuer").Value,
                Key = config.GetSection("JWT:Key").Value,
                ExpireMinutes = config.GetSection("JWT:ExpireMinutes").Value
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(JwtConfig.ExpireMinutes));

            var token = new JwtSecurityToken(
                JwtConfig.Issuer,
                JwtConfig.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
