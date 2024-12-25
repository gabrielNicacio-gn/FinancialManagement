
using FinancialManagement.Domain.Models;
using FinancialManagement.Domain.Repositories;
using FinancialManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infrastructure.Repositories;
public class ExpensesRepository : IExpenseRepository
{
        private readonly FinancialContext _context;

        public ExpensesRepository(FinancialContext context)
        {
                _context = context;
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
                var expenses = await _context.Expenses
                .AsNoTracking()
                .OrderByDescending(e => e.DateExpenses)
                .ToListAsync();
                return expenses;
        }

        public async Task<Expense> GetExpensesById(Guid id)
        {
                var expenses = await _context
                .Expenses
                .AsNoTracking()
                .SingleOrDefaultAsync()
                ?? throw new Exception("Expense not found");
                return expenses;
        }

        public async Task<Expense> AddExpenses(Expense expense)
        {
                var expenseCreated = _context.Expenses.Add(expense).Entity;
                await _context.SaveChangesAsync();
                return expenseCreated;
        }

        public async Task UpdateExpenses(Expense expense, string nameProperty)
        {
                var entity = _context.Expenses.Attach(expense);
                entity.State = EntityState.Modified;
                entity.Property(nameProperty).IsModified = true;

                await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenses(Guid id)
        {
                var expense = await _context.Expenses.FindAsync(id)
                ?? throw new Exception("Expense not found");
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
        }

        public bool ExpensesExists(Guid id)
        {
                var exist = _context.Expenses.Any(e => e.IdExpenses == id);
                return exist;
        }
}
