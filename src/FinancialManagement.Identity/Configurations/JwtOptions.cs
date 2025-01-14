using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace FinancialManagement.Identity.Configurations
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public SigningCredentials SigningCredentials { get; set; }
        public int Expirations { get; set; }
    }
}