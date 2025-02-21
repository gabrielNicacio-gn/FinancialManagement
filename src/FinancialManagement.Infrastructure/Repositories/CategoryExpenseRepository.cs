using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Models;
using FinancialManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infrastructure.Repositories;
public class CategoryExpenseRepository : ICategoryExpenseRepository
{
    private readonly FinancialContext _context;
    public CategoryExpenseRepository(FinancialContext context)
    {
        _context = context;
    }
    public async Task<CategoryExpense> AddCategoryExpense(CategoryExpense categoryExpense)
    {
        var categoryExpenseCreated = _context.CategoryExpenses.Add(categoryExpense).Entity;
        await _context.SaveChangesAsync();
        return categoryExpenseCreated;
    }

    public bool CategoryExpenseExists(Guid id)
    {
        var exist = _context.CategoryExpenses.Any(x => x.IdCategory == id);
        return exist;
    }

    public async Task DeleteCategoryExpense(Guid id)
    {
        await _context.CategoryExpenses
        .Where(ce => ce.IdCategory == id)
        .ExecuteDeleteAsync();
    }

    public async Task<CategoryExpense?> GetCategoryExpenseById(Guid id)
    {
        var categoryExpense = await _context
        .CategoryExpenses
        .AsNoTracking()
        .SingleOrDefaultAsync(ce => ce.IdCategory == id);
        return categoryExpense;
    }

    public async Task<IEnumerable<CategoryExpense>> GetCategoryExpenses(Guid IdUser)
    {
        var categoryExpenses = await _context.CategoryExpenses
        .Where(ce => ce.UserId == IdUser)
        .AsNoTracking()
        .ToListAsync();
        return categoryExpenses;
    }

    public async Task UpdateCategoryExpense(CategoryExpense categoryExpense, string nameProperty)
    {
        var entity = _context.CategoryExpenses.Attach(categoryExpense);
        entity.State = EntityState.Modified;
        entity.Property(nameProperty).IsModified = true;

        await _context.SaveChangesAsync();
    }
}
