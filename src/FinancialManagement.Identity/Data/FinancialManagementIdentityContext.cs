using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Identity.Data;
public class FinancialManagementIdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public FinancialManagementIdentityContext(DbContextOptions<FinancialManagementIdentityContext> options)
    : base(options) { }
}
