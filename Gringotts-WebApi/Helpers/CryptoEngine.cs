using CryptoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Helpers
{
    public class CryptoEngine : ICryptoEngine
    {
        public string Hash(string text)
        {
            return Crypto.HashPassword(text);
        }

        public bool HashCheck(string hash, string text)
        {
            return Crypto.VerifyHashedPassword(hash, text);
        }
    }
}
