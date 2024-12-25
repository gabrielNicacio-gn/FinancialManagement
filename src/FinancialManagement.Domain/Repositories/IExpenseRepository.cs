using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Models;

namespace FinancialManagement.Domain.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetExpenses();
        Task<Expense> GetExpensesById(Guid id);
        Task<Expense> AddExpenses(Expense expense);
        Task UpdateExpenses(Expense expense, string nameProperty);
        Task DeleteExpenses(Guid id);
        bool ExpensesExists(Guid id);
    }
}