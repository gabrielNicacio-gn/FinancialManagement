using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infrastructure.Data;

public class FinancialContext : DbContext
{
    public FinancialContext(DbContextOptions<FinancialContext> options) : base(options) { }

    public DbSet<Revenue> Revenues { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<FinancialTarget> FinancialTargets { get; set; }
    public DbSet<UserBalance> UserBalances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Revenue>()
        .HasKey(r => r.IdRevenue);
        modelBuilder.Entity<Expense>()
        .HasKey(e => e.IdExpense);
        modelBuilder.Entity<FinancialTarget>()
        .HasKey(ft => ft.IdFinancialTarget);
        modelBuilder.Entity<UserBalance>()
        .HasKey(ub => ub.IdUserBalance);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
        .EnableDetailedErrors()
        .LogTo(Console.WriteLine);
    }
}
