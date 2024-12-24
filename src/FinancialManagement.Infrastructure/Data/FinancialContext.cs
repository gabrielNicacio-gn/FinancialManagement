using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infrastructure.Data
{
    public class FinancialContext : DbContext
    {
        public FinancialContext(DbContextOptions<FinancialContext> options) : base(options) { }

        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
    }
}