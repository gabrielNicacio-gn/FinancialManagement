
using FinancialManagement.Application.DTOs.Request.CategoryExpense;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.DTOs.Shared;

namespace FinancialManagement.Application.Interfaces.Services
{
    public interface ICategoryExpenseServices
    {
        Task<BaseResponseDto<CategoryExpenseResponseDto>> CreateNewCategoryExpense(CreateCategoryExpenseDto newCategoryExpense, Guid IdUser);
        Task<BaseResponseDto<CategoryExpenseResponseDto>> GetCategoryExpenseById(Guid idCategoryExpense);
        Task<BaseResponseDto<IEnumerable<CategoryExpenseResponseDto>>> GetAllCategoryExpenses(Guid UserId);
        Task UpdateCategoryExpense(UpdateCategoryExpenseDto updateCategoryExpense, string nameProperty);
        Task RemoveCategoryExpense(Guid idCategoryExpense);
    }
}