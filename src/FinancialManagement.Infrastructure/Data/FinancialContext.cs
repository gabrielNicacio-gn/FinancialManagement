using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Enums;
using FinancialManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infrastructure.Data;

public class FinancialContext : DbContext
{
    public FinancialContext(DbContextOptions<FinancialContext> options) : base(options) { }

    public DbSet<Revenue> Revenues { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<FinancialTarget> FinancialTargets { get; set; }
    public DbSet<CategoryExpense> CategoryExpenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Revenue>()
        .HasKey(r => r.IdRevenue);

        modelBuilder.Entity<Expense>()
        .HasKey(e => e.IdExpense);

        modelBuilder.Entity<Expense>()
        .HasOne(e => e.CategoryeExpense)
        .WithOne(ce => ce.Expense)
        .HasForeignKey<CategoryExpense>(ce => ce.IdCategory);

        modelBuilder.Entity<FinancialTarget>()
        .HasKey(ft => ft.IdFinancialTarget);

        modelBuilder.Entity<CategoryExpense>()
        .HasKey(ce => ce.IdCategory);

        modelBuilder.Entity<FinancialTarget>()
        .Property(ft => ft.Status)
        .HasConversion(
            s => s.ToString(),
            s => (StatusFinancialTarget)Enum.Parse(typeof(StatusFinancialTarget), s));

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
        .EnableDetailedErrors()
        .LogTo(Console.WriteLine);
    }
}
