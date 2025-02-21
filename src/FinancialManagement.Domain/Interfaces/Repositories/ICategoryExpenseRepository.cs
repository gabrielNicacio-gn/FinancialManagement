using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Models;

namespace FinancialManagement.Domain.Interfaces.Repositories;
public interface ICategoryExpenseRepository
{
    Task<IEnumerable<CategoryExpense>> GetCategoryExpenses(Guid IdUser);
    Task<CategoryExpense?> GetCategoryExpenseById(Guid id);
    Task<CategoryExpense> AddCategoryExpense(CategoryExpense expense);
    Task UpdateCategoryExpense(CategoryExpense categoryExpense, string nameProperty);
    Task DeleteCategoryExpense(Guid id);
}
