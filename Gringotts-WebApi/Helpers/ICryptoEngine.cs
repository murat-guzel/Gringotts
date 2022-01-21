using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gringotts_WebApi.Helpers
{
    public interface ICryptoEngine
    {
        
        string Hash(string text);

         
        bool HashCheck(string hash, string text);
    }
}
