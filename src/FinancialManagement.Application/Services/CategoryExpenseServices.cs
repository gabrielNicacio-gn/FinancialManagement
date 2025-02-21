using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.CategoryExpense;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.DTOs.Shared;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagement.Application.Services
{
    public class CategoryExpenseServices : ICategoryExpenseServices
    {
        private readonly ICategoryExpenseRepository _categoryExpenseRepository;
        private readonly ILogger<CategoryExpenseServices> _logger;
        public CategoryExpenseServices(ICategoryExpenseRepository categoryExpenseRepository, ILogger<CategoryExpenseServices> logger)
        {
            _categoryExpenseRepository = categoryExpenseRepository;
            _logger = logger;
        }
        public async Task<BaseResponseDto<CategoryExpenseResponseDto>> CreateNewCategoryExpense(CreateCategoryExpenseDto categoryExpenseDto, Guid IdUser)
        {
            var newCategoryExpense = new CategoryExpense
            {
                Name = categoryExpenseDto.Name,
                UserId = IdUser
            };
            var categoryExpenseCreated = await _categoryExpenseRepository.AddCategoryExpense(newCategoryExpense);
            _logger.LogInformation($"CategoryExpense created with id: {categoryExpenseCreated.IdCategory}");
            var newCategoryExpenseResponse = new CategoryExpenseResponseDto(categoryExpenseCreated.IdCategory,
            categoryExpenseCreated.Name);
            return new BaseResponseDto<CategoryExpenseResponseDto>(newCategoryExpenseResponse);

        }

        public async Task<BaseResponseDto<IEnumerable<CategoryExpenseResponseDto>>> GetAllCategoryExpenses(Guid UserId)
        {
            var categoryExpenses = await _categoryExpenseRepository.GetCategoryExpenses(UserId);
            _logger.LogInformation($"CategoryExpenses found: {categoryExpenses.Count()}");
            var listCategoryExpensesResponse = categoryExpenses.Select(categoryExpense =>
             new CategoryExpenseResponseDto(categoryExpense.IdCategory, categoryExpense.Name));
            return new BaseResponseDto<IEnumerable<CategoryExpenseResponseDto>>(listCategoryExpensesResponse);
        }

        public async Task<BaseResponseDto<CategoryExpenseResponseDto>> GetCategoryExpenseById(Guid idCategoryExpense)
        {
            var categoryExpense = await _categoryExpenseRepository.GetCategoryExpenseById(idCategoryExpense);

            if (categoryExpense is null)
            {
                _logger.LogInformation($"Expense not found");
                return new BaseResponseDto<CategoryExpenseResponseDto>(false);
            }
            _logger.LogInformation($"CategoryExpense found with id: {categoryExpense.IdCategory}");
            var categoryExpenseResponse = new CategoryExpenseResponseDto(categoryExpense.IdCategory, categoryExpense.Name);
            return new BaseResponseDto<CategoryExpenseResponseDto>(categoryExpenseResponse);
        }

        public async Task RemoveCategoryExpense(Guid idCategoryExpense)
        {
            await _categoryExpenseRepository.DeleteCategoryExpense(idCategoryExpense);
            _logger.LogInformation($"CategoryExpense with id: {idCategoryExpense} removed");
        }

        public async Task UpdateCategoryExpense(UpdateCategoryExpenseDto updateCategoryExpense, string nameProperty)
        {
            var categoryExpenseUpdated = new CategoryExpense
            {
                Name = updateCategoryExpense.Name,
            };
            await _categoryExpenseRepository.UpdateCategoryExpense(categoryExpenseUpdated, nameProperty);
            _logger.LogInformation($"CategoryExpense with id: {categoryExpenseUpdated.IdCategory} updated");
        }
    }
}