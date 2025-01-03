
using FinancialManagement.Application.DTOs.Request.CategoryExpense;
using FinancialManagement.Application.DTOs.Response;

namespace FinancialManagement.Application.Interfaces.Services
{
    public interface ICategoryExpenseServices
    {
        Task<CategoryExpenseResponseDto> CreateNewCategoryExpense(CreateCategoryExpenseDto newCategoryExpense);
        Task<CategoryExpenseResponseDto> GetCategoryExpenseById(Guid idCategoryExpense);
        Task<IEnumerable<CategoryExpenseResponseDto>> GetAllCategoryExpenses();
        Task UpdateCategoryExpense(UpdateCategoryExpenseDto updateCategoryExpense, string nameProperty);
        Task RemoveCategoryExpense(Guid idCategoryExpense);
    }
}