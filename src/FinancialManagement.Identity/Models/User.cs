using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FinancialManagement.Identity.Models
{
    public class User : IdentityUser<Guid> { }
}